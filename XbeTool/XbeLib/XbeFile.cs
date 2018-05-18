using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XbeLib.Crypto;
using XbeLib.Utility;
using XbeLib.XbeStructure;
using XMarkDown;

namespace XbeLib
{

    // Thanks a ton to caustik for documenting the format!
    // http://www.caustik.com/cxbx/download/xbe.htm

    public class XbeFile
    {

        public byte[] File;
        public ImageHeader ImageHeader;
        public Certificate Certificate;
        public List<SectionHeader> SectionHeaders;
        public List<LibraryVersion> LibraryVersions;
        public TLS TLS;
        public byte[] MetaHash; // This (+ some padding) is what gets encrypted with the private key

        public XbeFile(byte[] file)
        {

            File = file;
            ImageHeader = new ImageHeader(file);
            Certificate = new Certificate(Util.SubArray(file, ImageHeader.CertificateAddress - ImageHeader.BaseAddress, 0x1D0));
            SectionHeaders = new List<SectionHeader>();
            LibraryVersions = new List<LibraryVersion>();

            for (int i = 0; i < ImageHeader.NumberOfSections; i++)
            {
                SectionHeaders.Add(new SectionHeader(Util.SubArray(file, ImageHeader.SectionHeaderAddress - ImageHeader.BaseAddress + (i * 0x38), 0x38), File, ImageHeader.BaseAddress));
            }

            for (int i = 0; i < ImageHeader.NumberOfLibraryVersions; i++)
            {
                LibraryVersions.Add(new LibraryVersion(Util.SubArray(file, ImageHeader.LibraryVersionsAddress - ImageHeader.BaseAddress + (i * 0x10), 0x10)));
            }

            TLS = new TLS(Util.SubArray(file, ImageHeader.TLSAddress - ImageHeader.BaseAddress, 0x18));

            MetaHash = Util.SubArray(File, 0x104, ImageHeader.SizeOfHeaders - 0x104);

            List<byte> list = new List<byte>(MetaHash);
            list.InsertRange(0, BitConverter.GetBytes(MetaHash.Length));
            MetaHash = list.ToArray();

            byte[] calcHash;

            using (SHA1 sha1 = SHA1.Create())
            {
                calcHash = sha1.ComputeHash(MetaHash);
            }

            MetaHash = calcHash.Reverse().ToArray();

        }

        public bool VerifyIntegrity(bool verbose, bool green)
        {

            bool valid = true;

            byte[] decryptedSignature;

            if (green)
            {
                decryptedSignature = PublicKey.Decrypt(ImageHeader.DigitalSignature);
            }
            else
            {
                decryptedSignature = PublicKey.DecryptRed(ImageHeader.DigitalSignature);
            }

            if (verbose)
            {
                Console.WriteLine("Checking digital signature...");
            }

            // Check the digital signature
            // TODO: We must also check the padding. The Xbox checks this too!
            if (decryptedSignature.Take(20).ToArray().SequenceEqual(MetaHash))
            {
                if (verbose)
                {
                    Console.WriteLine("\n\tValid\n");
                }
            }
            else
            {
                valid = false;

                if (verbose)
                {
                    Console.WriteLine("\n\tInvalid\n");
                }
            }

            // Verify that the section hashes in the section header table match the data in the xbe
            // The section header table hashes can not be modified because they have already been validated with the digital signature
            // As far as I can tell, this is the order in which the xbox validates the xbe, contrary to what is found here: http://xboxdevwiki.net/Kernel/XboxSignatureKey

            if (verbose)
            {
                Console.WriteLine("Checking section hashes...");
            }

            foreach (SectionHeader section in SectionHeaders)
            {

                if (section.VerifyDigest())
                {
                    if (verbose)
                    {
                        Console.WriteLine(section.SectionName + ":\n\tValid\n");
                    }
                }
                else
                {

                    valid = false;

                    if (verbose)
                    {
                        Console.WriteLine(section.SectionName + ":\n\tInvalid\n");
                    }
                }
            }

            if (verbose)
            {
                if (valid)
                {
                    Console.WriteLine("\nXBE is NOT valid");
                }
                else
                {
                    Console.WriteLine("\nXBE is valid");
                }
            }

            return valid;

        }


        // Retail
        public void SignGreen()
        {
            
        }

        // Debug
        public void SignRed()
        {
            
        }

        public void ExtractBitmap()
        {

        }

        private void ExtractSection()
        {

        }

        public string GenerateStructMainMD()
        {
            string md = "# XBE Structure\n\n";
            md += "[Image Header](./ImageHeader.MD)\n\n";
            md += "[Certificate](./Certificate.MD)\n\n";
            md += "[Section Headers](./sections/README.MD)\n\n";
            md += "[Library Versions](./libraries/README.MD)\n\n";
            md += "[TLS](./TLS.MD)\n\n";

            return md;
        }

        public string GenerateSectionsMainMD()
        {
            string md = "# XBE Sections\n\n";
            foreach (SectionHeader section in SectionHeaders)
            {
                md += String.Format("[{0}](./{0}.MD)\n\n", section.SectionName);
            }

            return md;
        }

        public string GenerateLibrariesMainMD()
        {
            string md = "# XBE Libraries\n\n";
            foreach (LibraryVersion version in LibraryVersions)
            {
                md += String.Format("[{0}](./{1}.MD)\n\n", version.LibraryName + " - " + version.FullVersion, version.LibraryName);
            }

            return md;
        }

    }
}
