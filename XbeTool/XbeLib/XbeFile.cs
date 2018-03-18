using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    }
}
