using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMarkDown;

namespace XbeLib
{

    // Thanks a ton to caustik for documenting the format!
    // http://www.caustik.com/cxbx/download/xbe.htm

    public class XbeFile
    {

        private byte[] _MagicNumber;                        // 0x00 [0x04 bytes]
        public string MagicNumber;

        public byte[] DigitalSignature;                     // 0x04 [0x100 bytes]

        private byte[] _BaseAddress;                        // 0x104 [0x04 bytes]
        public int BaseAddress;

        private byte[] _SizeOfHeaders;                      // 0x108 [0x04 bytes]
        public int SizeOfHeaders;

        private byte[] _SizeOfImage;                        // 0x10C [0x04 bytes]
        public int SizeOfImage;

        private byte[] _SizeOfImageHeader;                  // 0x110 [0x04 bytes]
        public int SizeOfImageHeader;

        private byte[] _TimeDate;                           // 0x114 [0x04 bytes]
        public DateTime TimeDate;

        private byte[] _CertificateAddress;                 // 0x118 [0x04 bytes]
        public int CertificateAddress;

        private byte[] _NumberOfSections;                   // 0x11C [0x04 bytes]
        public int NumberOfSections;

        private byte[] _SectionHeadersAddress;              // 0x120 [0x04 bytes]
        public int SectionHeaderAddress;

        // TODO: Enum me plz!
        private byte[] _InitializationFlags;                // 0x124 [0x04 bytes]
        public int Initializationflags;

        private byte[] _EntryPoint;                         // 0x128 [0x04 bytes]
        public int EntryPoint;

        private byte[] _TLSAddress;                         // 0x12C [0x04 bytes]
        public int TLSAddress;

        private byte[] _PEStackCommit;                      // 0x130 [0x04 bytes]
        public int PEStackCommit;

        private byte[] _PEHeapReserve;                      // 0x134 [0x04 bytes]
        public int PEHeapReserve;

        private byte[] _PEHeapCommit;                       // 0x138 [0x04 bytes]
        public int PEHeapCommit;

        private byte[] _PEBaseAddress;                      // 0x13C [0x04 bytes]
        public int PEBaseAddress;

        private byte[] _PESizeOfImage;                      // 0x140 [0x04 bytes]
        public int PESizeOfImage;

        public byte[] PEChecksum;                           // 0x144 [0x04 bytes]

        private byte[] _PETimeDate;                         // 0x148 [0x04 bytes]
        public DateTime PETimeDate;

        private byte[] _DebugPathNameAddress;               // 0x14C [0x04 bytes]
        public int DebugPathNameAddress;
        public int PathNameAddress;

        private byte[] _DebugFileNameAddress;               // 0x150 [0x04 bytes]
        public int DebugFileNameAddress;
        public string DebugFileName;

        private byte[] _DebugUnicodeFileNameAddress;        // 0x154 [0x04 bytes]
        public int DebugUnicodeFileNameAddress;
        public string UnicodeFileName;

        private byte[] _KernelImageThunkAddress;            // 0x158 [0x04 bytes]
        public int KernelImageThunkAddress;

        private byte[] _NonKernelImportDirectoryAddress;    // 0x15C [0x04 bytes]
        public int NonKernelImportDirectoryAddress;

        private byte[] _NumberOfLibraryVersions;            // 0x160 [0x04 bytes]
        public int NumberOfLibraryVersions;

        private byte[] _LibraryVersionsAddress;             // 0x164 [0x04 bytes]
        public int LibraryVersionsAddress;

        private byte[] _KernelLibraryVersionAddress;        // 0x168 [0x04 bytes]
        public int KernelLibraryAddress;

        private byte[] _XAPILibraryVersionAddress;          // 0x16C [0x04 bytes]
        public int XAPILibraryVersionAddress;

        private byte[] _LogoBitmapAddress;                  // 0x170 [0x04 bytes]
        public int LogoBitmapAddress;

        private byte[] _LogoBitmapSize;                     // 0x174 [0x04 bytes]
        public int LogoBitmapSize;


        public XbeFile(byte[] file)
        {
            _MagicNumber = SubArray(file, 0x00, 0x04);
            MagicNumber = Encoding.ASCII.GetString(_MagicNumber);

            DigitalSignature = SubArray(file, 0x04, 0x100);

            _BaseAddress = SubArray(file, 0x104, 0x04);
            BaseAddress = BitConverter.ToInt32(_BaseAddress, 0);

            _SizeOfHeaders = SubArray(file, 0x10C, 0x04);
            SizeOfHeaders = BitConverter.ToInt32(_SizeOfHeaders, 0);

            _SizeOfImage = SubArray(file, 0x110, 0x04);
            SizeOfImage = BitConverter.ToInt32(_SizeOfImage, 0);

            _SizeOfImageHeader = SubArray(file, 0x104, 0x04);
            SizeOfImageHeader = BitConverter.ToInt32(_SizeOfImageHeader, 0);

            _TimeDate = SubArray(file, 0x108, 0x04);
            // Todo: this

            _CertificateAddress = SubArray(file, 0x10C, 0x04);
            CertificateAddress = BitConverter.ToInt32(_CertificateAddress, 0);

            _NumberOfSections = SubArray(file, 0x110, 0x04);
            NumberOfSections = BitConverter.ToInt32(_NumberOfSections, 0);

            _SectionHeadersAddress = SubArray(file, 0x114, 0x04);
            SectionHeaderAddress = BitConverter.ToInt32(_SectionHeadersAddress, 0);

            _InitializationFlags = SubArray(file, 0x118, 0x04);
            // TODO: Enum!!!

            _EntryPoint = SubArray(file, 0x11C, 0x04);
            EntryPoint = BitConverter.ToInt32(_EntryPoint, 0);

            _TLSAddress = SubArray(file, 0x120, 0x04);
            TLSAddress = BitConverter.ToInt32(_TLSAddress, 0);

            _PEStackCommit = SubArray(file, 0x124, 0x04);
            PEStackCommit = BitConverter.ToInt32(_PEStackCommit, 0);

            _PEHeapReserve = SubArray(file, 0x128, 0x04);
            PEHeapReserve = BitConverter.ToInt32(_PEHeapReserve, 0);

            _PEHeapCommit = SubArray(file, 0x12C, 0x04);
            PEHeapCommit = BitConverter.ToInt32(_PEHeapCommit, 0);

            _PEBaseAddress = SubArray(file, 0x130, 0x04);
            PEBaseAddress = BitConverter.ToInt32(_PEBaseAddress, 0);

            _PESizeOfImage = SubArray(file, 0x134, 0x04);
            PESizeOfImage = BitConverter.ToInt32(_PESizeOfImage, 0);

            PEChecksum = SubArray(file, 0x138, 0x04);

            _PETimeDate = SubArray(file, 0x13C, 0x04);
            // TODO

            _DebugPathNameAddress = SubArray(file, 0x140, 0x04);
            DebugPathNameAddress = BitConverter.ToInt32(_DebugPathNameAddress, 0);
            // TODO

            _DebugFileNameAddress = SubArray(file, 0x144, 0x04);
            DebugFileNameAddress = BitConverter.ToInt32(_DebugFileNameAddress, 0);
            // TODO

            _DebugUnicodeFileNameAddress = SubArray(file, 0x148, 0x04);
            DebugUnicodeFileNameAddress = BitConverter.ToInt32(_DebugUnicodeFileNameAddress, 0);
            // TODO

            _KernelImageThunkAddress = SubArray(file, 0x14C, 0x04);
            KernelImageThunkAddress = BitConverter.ToInt32(_KernelImageThunkAddress, 0);

            _NonKernelImportDirectoryAddress = SubArray(file, 0x150, 0x04);
            NonKernelImportDirectoryAddress = BitConverter.ToInt32(_NonKernelImportDirectoryAddress, 0);

            _NumberOfLibraryVersions = SubArray(file, 0x15C, 0x04);
            NumberOfLibraryVersions = BitConverter.ToInt32(_NumberOfLibraryVersions, 0);

            _LibraryVersionsAddress = SubArray(file, 0x160, 0x04);
            LibraryVersionsAddress = BitConverter.ToInt32(_LibraryVersionsAddress, 0);

            _KernelLibraryVersionAddress = SubArray(file, 0x164, 0x04);
            KernelLibraryAddress = BitConverter.ToInt32(_KernelLibraryVersionAddress, 0);

            _XAPILibraryVersionAddress = SubArray(file, 0x168, 0x04);
            XAPILibraryVersionAddress = BitConverter.ToInt32(_XAPILibraryVersionAddress, 0);

            _LogoBitmapAddress = SubArray(file, 0x16C, 0x04);
            LogoBitmapAddress = BitConverter.ToInt32(_LogoBitmapAddress, 0);

            _LogoBitmapSize = SubArray(file, 0x174, 0x04);
            LogoBitmapSize = BitConverter.ToInt32(_LogoBitmapSize, 0);

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

        public string GenerateMD()
        {
            string md = "";

            md += MDUtil.MDTableHeader("Field Name", "Description");
            md += MDUtil.MDTableRow("Magic Number", MagicNumber);
            md += MDUtil.MDTableRow("Digital Signature", BitConverter.ToString(DigitalSignature).Replace("-", " "));
            md += MDUtil.MDTableRow("Base Address", BaseAddress.ToString());
            md += MDUtil.MDTableRow("Size of Headers", SizeOfHeaders.ToString());
            md += MDUtil.MDTableRow("Size of Image", SizeOfImage.ToString());
            md += MDUtil.MDTableRow("Size of Header", SizeOfImageHeader.ToString());
            //md += MDUtil.MDTableRow("Time / Date", TimeDate.ToString());
            md += MDUtil.MDTableRow("Certificate Address", CertificateAddress.ToString());
            md += MDUtil.MDTableRow("Number of Sections", NumberOfSections.ToString());
            md += MDUtil.MDTableRow("Section Header Address", SectionHeaderAddress.ToString());
            md += MDUtil.MDTableRow("Initialization Flags", Initializationflags.ToString());
            md += MDUtil.MDTableRow("Entry Point", EntryPoint.ToString());
            md += MDUtil.MDTableRow("TLS Address", TLSAddress.ToString());
            md += MDUtil.MDTableRow("PE Stack Commit", PEStackCommit.ToString());
            md += MDUtil.MDTableRow("PE Heap Reserve", PEHeapReserve.ToString());
            md += MDUtil.MDTableRow("PE Heap Commit", PEHeapCommit.ToString());
            md += MDUtil.MDTableRow("PE Base Address", PEBaseAddress.ToString());
            md += MDUtil.MDTableRow("PE Size of Image", PESizeOfImage.ToString());
            md += MDUtil.MDTableRow("PE Checksum", PEChecksum.ToString());
            //md += MDUtil.MDTableRow("PE Time / Date", PETimeDate.ToString());
            md += MDUtil.MDTableRow("Debug Path Name Address", DebugPathNameAddress.ToString());
            //md += MDUtil.MDTableRow("Debug Path Name", DebugPathName.ToString());
            md += MDUtil.MDTableRow("Debug File Name Address", DebugFileNameAddress.ToString());
            //md += MDUtil.MDTableRow("Debug File Name", DebugFileName);
            md += MDUtil.MDTableRow("Debug Unicode File Name Address", DebugUnicodeFileNameAddress.ToString());
            //md += MDUtil.MDTableRow("Debug Unicode File Name", DebugUnicodeFileName);
            md += MDUtil.MDTableRow("Kernel Image Thunk Address", KernelImageThunkAddress.ToString());
            md += MDUtil.MDTableRow("Non-Kernel Import Directory Address", NonKernelImportDirectoryAddress.ToString());
            md += MDUtil.MDTableRow("Number of Library Versions", NumberOfLibraryVersions.ToString());
            md += MDUtil.MDTableRow("Library Versions Address", LibraryVersionsAddress.ToString());
            md += MDUtil.MDTableRow("Kernel Library Address", KernelLibraryAddress.ToString());
            md += MDUtil.MDTableRow("XAPI Library Version Address", XAPILibraryVersionAddress.ToString());
            md += MDUtil.MDTableRow("Logo Bitmap Address", LogoBitmapAddress.ToString());
            md += MDUtil.MDTableRow("Logo Bitmap Size", LogoBitmapSize.ToString());

            return md;
        }

        // TODO: belongs in a common library
        private static byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
