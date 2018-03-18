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

        private byte[] _File;

        private byte[] _MagicNumber;                        // 0x00 [0x04 bytes]
        public string MagicNumber;

        public byte[] DigitalSignature;                     // 0x04 [0x100 bytes]

        private byte[] _BaseAddress;                        // 0x104 [0x04 bytes]
        public long BaseAddress;

        private byte[] _SizeOfHeaders;                      // 0x108 [0x04 bytes]
        public long SizeOfHeaders;

        private byte[] _SizeOfImage;                        // 0x10C [0x04 bytes]
        public long SizeOfImage;

        private byte[] _SizeOfImageHeader;                  // 0x110 [0x04 bytes]
        public long SizeOfImageHeader;

        private byte[] _TimeDate;                           // 0x114 [0x04 bytes]
        public long TimeDate;

        private byte[] _CertificateAddress;                 // 0x118 [0x04 bytes]
        public long CertificateAddress;

        private byte[] _NumberOfSections;                   // 0x11C [0x04 bytes]
        public long NumberOfSections;

        private byte[] _SectionHeadersAddress;              // 0x120 [0x04 bytes]
        public long SectionHeaderAddress;

        // TODO: Enum me plz!
        private byte[] _InitializationFlags;                // 0x124 [0x04 bytes]
        public long Initializationflags;

        private byte[] _EntryPoint;                         // 0x128 [0x04 bytes]
        public long EntryPoint;

        private byte[] _TLSAddress;                         // 0x12C [0x04 bytes]
        public long TLSAddress;

        private byte[] _PEStackCommit;                      // 0x130 [0x04 bytes]
        public long PEStackCommit;

        private byte[] _PEHeapReserve;                      // 0x134 [0x04 bytes]
        public long PEHeapReserve;

        private byte[] _PEHeapCommit;                       // 0x138 [0x04 bytes]
        public long PEHeapCommit;

        private byte[] _PEBaseAddress;                      // 0x13C [0x04 bytes]
        public long PEBaseAddress;

        private byte[] _PESizeOfImage;                      // 0x140 [0x04 bytes]
        public long PESizeOfImage;

        public byte[] PEChecksum;                           // 0x144 [0x04 bytes]

        private byte[] _PETimeDate;                         // 0x148 [0x04 bytes]
        public long PETimeDate;

        private byte[] _DebugPathNameAddress;               // 0x14C [0x04 bytes]
        public long DebugPathNameAddress;
        public string DebugPathName;

        private byte[] _DebugFileNameAddress;               // 0x150 [0x04 bytes]
        public long DebugFileNameAddress;
        public string DebugFileName;

        private byte[] _DebugUnicodeFileNameAddress;        // 0x154 [0x04 bytes]
        public long DebugUnicodeFileNameAddress;
        public string DebugUnicodeFileName;

        private byte[] _KernelImageThunkAddress;            // 0x158 [0x04 bytes]
        public long KernelImageThunkAddress;

        private byte[] _NonKernelImportDirectoryAddress;    // 0x15C [0x04 bytes]
        public long NonKernelImportDirectoryAddress;

        private byte[] _NumberOfLibraryVersions;            // 0x160 [0x04 bytes]
        public long NumberOfLibraryVersions;

        private byte[] _LibraryVersionsAddress;             // 0x164 [0x04 bytes]
        public long LibraryVersionsAddress;

        private byte[] _KernelLibraryVersionAddress;        // 0x168 [0x04 bytes]
        public long KernelLibraryAddress;

        private byte[] _XAPILibraryVersionAddress;          // 0x16C [0x04 bytes]
        public long XAPILibraryVersionAddress;

        private byte[] _LogoBitmapAddress;                  // 0x170 [0x04 bytes]
        public long LogoBitmapAddress;

        private byte[] _LogoBitmapSize;                     // 0x174 [0x04 bytes]
        public long LogoBitmapSize;


        public XbeFile(byte[] file)
        {

            _File = file;

            _MagicNumber = SubArray(file, 0x00, 0x04);
            MagicNumber = Encoding.ASCII.GetString(_MagicNumber);

            DigitalSignature = SubArray(file, 0x04, 0x100);

            _BaseAddress = SubArray(file, 0x104, 0x04);
            BaseAddress = BitConverter.ToUInt32(_BaseAddress, 0);

            _SizeOfHeaders = SubArray(file, 0x108, 0x04);
            SizeOfHeaders = BitConverter.ToUInt32(_SizeOfHeaders, 0);

            _SizeOfImage = SubArray(file, 0x10C, 0x04);
            SizeOfImage = BitConverter.ToUInt32(_SizeOfImage, 0);

            _SizeOfImageHeader = SubArray(file, 0x110, 0x04);
            SizeOfImageHeader = BitConverter.ToUInt32(_SizeOfImageHeader, 0);

            _TimeDate = SubArray(file, 0x114, 0x04);
            TimeDate = BitConverter.ToUInt32(_TimeDate, 0);

            _CertificateAddress = SubArray(file, 0x118, 0x04);
            CertificateAddress = BitConverter.ToUInt32(_CertificateAddress, 0);

            _NumberOfSections = SubArray(file, 0x11C, 0x04);
            NumberOfSections = BitConverter.ToUInt32(_NumberOfSections, 0);

            _SectionHeadersAddress = SubArray(file, 0x120, 0x04);
            SectionHeaderAddress = BitConverter.ToUInt32(_SectionHeadersAddress, 0);

            _InitializationFlags = SubArray(file, 0x124, 0x04);
            // TODO: Enum!!!

            // TODO: Detect correct XOR key
            // Debug: 0x94859D4B
            // Retail: 0xA8FC57AB
            _EntryPoint = SubArray(file, 0x128, 0x04);
            EntryPoint = BitConverter.ToUInt32(_EntryPoint, 0) ^ 0xA8FC57AB;

            _TLSAddress = SubArray(file, 0x12C, 0x04);
            TLSAddress = BitConverter.ToUInt32(_TLSAddress, 0);

            _PEStackCommit = SubArray(file, 0x130, 0x04);
            PEStackCommit = BitConverter.ToUInt32(_PEStackCommit, 0);

            _PEHeapReserve = SubArray(file, 0x134, 0x04);
            PEHeapReserve = BitConverter.ToUInt32(_PEHeapReserve, 0);

            _PEHeapCommit = SubArray(file, 0x138, 0x04);
            PEHeapCommit = BitConverter.ToUInt32(_PEHeapCommit, 0);

            _PEBaseAddress = SubArray(file, 0x13C, 0x04);
            PEBaseAddress = BitConverter.ToUInt32(_PEBaseAddress, 0);

            _PESizeOfImage = SubArray(file, 0x140, 0x04);
            PESizeOfImage = BitConverter.ToUInt32(_PESizeOfImage, 0);

            PEChecksum = SubArray(file, 0x144, 0x04);

            _PETimeDate = SubArray(file, 0x148, 0x04);
            PETimeDate = BitConverter.ToUInt32(_PETimeDate, 0);

            _DebugPathNameAddress = SubArray(file, 0x14C, 0x04);
            DebugPathNameAddress = BitConverter.ToUInt32(_DebugPathNameAddress, 0);
            DebugPathName = GetNullTerminatedString(DebugPathNameAddress - BaseAddress);

            _DebugFileNameAddress = SubArray(file, 0x150, 0x04);
            DebugFileNameAddress = BitConverter.ToUInt32(_DebugFileNameAddress, 0);
            DebugFileName = GetNullTerminatedString(DebugFileNameAddress - BaseAddress);

            _DebugUnicodeFileNameAddress = SubArray(file, 0x154, 0x04);
            DebugUnicodeFileNameAddress = BitConverter.ToUInt32(_DebugUnicodeFileNameAddress, 0);
            DebugUnicodeFileName = GetUnicodeString(DebugUnicodeFileNameAddress - BaseAddress);
            // TODO

            // TODO: Detect correct XOR key
            // Debug: 0xEFB1F152
            // Retail: 0x5B6D40B6
            _KernelImageThunkAddress = SubArray(file, 0x158, 0x04);
            KernelImageThunkAddress = BitConverter.ToUInt32(_KernelImageThunkAddress, 0) ^ 0x5B6D40B6;

            _NonKernelImportDirectoryAddress = SubArray(file, 0x15C, 0x04);
            NonKernelImportDirectoryAddress = BitConverter.ToUInt32(_NonKernelImportDirectoryAddress, 0);

            _NumberOfLibraryVersions = SubArray(file, 0x160, 0x04);
            NumberOfLibraryVersions = BitConverter.ToUInt32(_NumberOfLibraryVersions, 0);

            _LibraryVersionsAddress = SubArray(file, 0x164, 0x04);
            LibraryVersionsAddress = BitConverter.ToUInt32(_LibraryVersionsAddress, 0);

            _KernelLibraryVersionAddress = SubArray(file, 0x168, 0x04);
            KernelLibraryAddress = BitConverter.ToUInt32(_KernelLibraryVersionAddress, 0);

            _XAPILibraryVersionAddress = SubArray(file, 0x16C, 0x04);
            XAPILibraryVersionAddress = BitConverter.ToUInt32(_XAPILibraryVersionAddress, 0);

            _LogoBitmapAddress = SubArray(file, 0x170, 0x04);
            LogoBitmapAddress = BitConverter.ToUInt32(_LogoBitmapAddress, 0);

            _LogoBitmapSize = SubArray(file, 0x174, 0x04);
            LogoBitmapSize = BitConverter.ToUInt32(_LogoBitmapSize, 0);

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
            md += MDUtil.MDTableRow("Base Address", BaseAddress.ToString("X"));
            md += MDUtil.MDTableRow("Size of Headers", SizeOfHeaders.ToString("X"));
            md += MDUtil.MDTableRow("Size of Image", SizeOfImage.ToString("X"));
            md += MDUtil.MDTableRow("Size of Header", SizeOfImageHeader.ToString("X"));
            DateTime dtTimeDate = DateTimeOffset.FromUnixTimeSeconds(TimeDate).DateTime;
            md += MDUtil.MDTableRow("Time / Date", TimeDate.ToString("X") + " (" + dtTimeDate + ")");
            md += MDUtil.MDTableRow("Certificate Address", CertificateAddress.ToString("X"));
            md += MDUtil.MDTableRow("Number of Sections", NumberOfSections.ToString("X"));
            md += MDUtil.MDTableRow("Section Header Address", SectionHeaderAddress.ToString("X"));
            md += MDUtil.MDTableRow("Initialization Flags", Initializationflags.ToString("X"));
            md += MDUtil.MDTableRow("Entry Point", EntryPoint.ToString("X"));
            md += MDUtil.MDTableRow("TLS Address", TLSAddress.ToString("X"));
            md += MDUtil.MDTableRow("PE Stack Commit", PEStackCommit.ToString("X"));
            md += MDUtil.MDTableRow("PE Heap Reserve", PEHeapReserve.ToString("X"));
            md += MDUtil.MDTableRow("PE Heap Commit", PEHeapCommit.ToString("X"));
            md += MDUtil.MDTableRow("PE Base Address", PEBaseAddress.ToString("X"));
            md += MDUtil.MDTableRow("PE Size of Image", PESizeOfImage.ToString("X"));
            md += MDUtil.MDTableRow("PE Checksum", BitConverter.ToString(PEChecksum).Replace("-", " "));
            DateTime dtPETimeDate = DateTimeOffset.FromUnixTimeSeconds(PETimeDate).DateTime;
            md += MDUtil.MDTableRow("PE Time / Date", PETimeDate.ToString("X") + " (" + dtPETimeDate + ")");
            md += MDUtil.MDTableRow("Debug Path Name Address", DebugPathNameAddress.ToString("X"));
            md += MDUtil.MDTableRow("Debug Path Name", DebugPathName);
            md += MDUtil.MDTableRow("Debug File Name Address", DebugFileNameAddress.ToString("X"));
            md += MDUtil.MDTableRow("Debug File Name", DebugFileName);
            md += MDUtil.MDTableRow("Debug Unicode File Name Address", DebugUnicodeFileNameAddress.ToString("X"));
            md += MDUtil.MDTableRow("Debug Unicode File Name", DebugUnicodeFileName);
            md += MDUtil.MDTableRow("Kernel Image Thunk Address", KernelImageThunkAddress.ToString("X"));
            md += MDUtil.MDTableRow("Non-Kernel Import Directory Address", NonKernelImportDirectoryAddress.ToString("X"));
            md += MDUtil.MDTableRow("Number of Library Versions", NumberOfLibraryVersions.ToString("X"));
            md += MDUtil.MDTableRow("Library Versions Address", LibraryVersionsAddress.ToString("X"));
            md += MDUtil.MDTableRow("Kernel Library Address", KernelLibraryAddress.ToString("X"));
            md += MDUtil.MDTableRow("XAPI Library Version Address", XAPILibraryVersionAddress.ToString("X"));
            md += MDUtil.MDTableRow("Logo Bitmap Address", LogoBitmapAddress.ToString("X"));
            md += MDUtil.MDTableRow("Logo Bitmap Size", LogoBitmapSize.ToString("X"));

            return md;
        }

        // TODO: belongs in a common library
        private static byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private string GetNullTerminatedString(long address)
        {
            string value = "";

            while (_File[address] != 0x00)
            {
                value += (char)_File[address];
                address++;
            }

            return value;
        }

        private string GetUnicodeString(long address)
        {
            string value = "";

            while (_File[address] != 0x00 || _File[address + 1] != 0x00)
            {
                value += (char)_File[address];
                address += 2;
            }

            return value;
        }
    }
}
