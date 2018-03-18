using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib.Utility;
using XMarkDown;

namespace XbeLib.XbeStructure
{
    public class SectionHeader
    {

        private byte[] _SectionHeader;

        private byte[] _SectionFlags;            // 0x00 [0x04 bytes]
        public long SectionFlags;
        public bool SectionFlagWritable;
        public bool SectionFlagPreload;
        public bool SectionFlagExecutable;
        public bool SectionFlagInsertedFile;
        public bool SectionFlagHeadPageRO;
        public bool SectionFlagTailPageRO;

        private byte[] _VirtualAddress;          // 0x04 [0x04 bytes]
        public long VirtualAddress;

        private byte[] _VirtualSize;             // 0x08 [0x04 bytes]
        public long VirtualSize;

        private byte[] _RawAddress;              // 0x0C [0x04 bytes]
        public long RawAddress;

        private byte[] _RawSize;                 // 0x10 [0x04 bytes]
        public long RawSize;

        private byte[] _SectionNameAddress;      // 0x14 [0x04 bytes]
        public long SectionNameAddress;
        public string SectionName;

        private byte[] _SectionNameRefCount;     // 0x18 [0x04 bytes]
        public long SectionNameRefCount;

        private byte[] _HeadSharedRefAddr;       // 0x1C [0x04 bytes]
        public long HeadSharedRefAddr;

        private byte[] _TailSharedRefAddr;       // 0x20 [0x04 bytes]
        public long TailSharedRefAddr;

        public byte[] SectionDigest;            // 0x24 [0x14 bytes]



        public SectionHeader(byte[] sectionHeader, byte[] xbe, long baseAddress)
        {
            _SectionHeader = sectionHeader;

            _SectionFlags = Util.SubArray(sectionHeader, 0x00, 0x04);
            SectionFlags = BitConverter.ToUInt32(_SectionFlags, 0);
            var sectionMask = (Enum.SectionFlags)SectionFlags;
            SectionFlagWritable = sectionMask.HasFlag(Enum.SectionFlags.Writable);
            SectionFlagPreload = sectionMask.HasFlag(Enum.SectionFlags.Preload);
            SectionFlagExecutable = sectionMask.HasFlag(Enum.SectionFlags.Executable);
            SectionFlagInsertedFile = sectionMask.HasFlag(Enum.SectionFlags.InsertedFile);
            SectionFlagHeadPageRO = sectionMask.HasFlag(Enum.SectionFlags.HeadPageReadOnly);
            SectionFlagTailPageRO = sectionMask.HasFlag(Enum.SectionFlags.TailPageReadOnly);

            _VirtualAddress = Util.SubArray(sectionHeader, 0x04, 0x04);
            VirtualAddress = BitConverter.ToUInt32(_VirtualAddress, 0);

            _VirtualSize = Util.SubArray(sectionHeader, 0x08, 0x04);
            VirtualSize = BitConverter.ToUInt32(_VirtualSize, 0);

            _RawAddress = Util.SubArray(sectionHeader, 0x0C, 0x04);
            RawAddress = BitConverter.ToUInt32(_RawAddress, 0);

            _RawSize = Util.SubArray(sectionHeader, 0x10, 0x04);
            RawSize = BitConverter.ToUInt32(_RawSize, 0);

            _SectionNameAddress = Util.SubArray(sectionHeader, 0x14, 0x04);
            SectionNameAddress = BitConverter.ToUInt32(_SectionNameAddress, 0);
            SectionName = Util.GetNullTerminatedString(xbe, SectionNameAddress - baseAddress);

            _SectionNameRefCount = Util.SubArray(sectionHeader, 0x18, 0x04);
            SectionNameRefCount = BitConverter.ToUInt32(_SectionNameRefCount, 0);

            _HeadSharedRefAddr = Util.SubArray(sectionHeader, 0x1C, 0x04);
            HeadSharedRefAddr = BitConverter.ToUInt32(_HeadSharedRefAddr, 0);

            _TailSharedRefAddr = Util.SubArray(sectionHeader, 0x20, 0x04);
            TailSharedRefAddr = BitConverter.ToUInt32(_TailSharedRefAddr, 0);

            SectionDigest = Util.SubArray(sectionHeader, 0x24, 0x14);
        }

        public string GenerateMD()
        {
            string md = "# XBE Section Header - " + SectionName + "\n\n";

            md += MDUtil.MDTableHeader("Field Name", "Description");
            md += MDUtil.MDTableRow("Section Flags", SectionFlags.ToString("X"));
            md += MDUtil.MDTableRow("Section Flag Writable", SectionFlagWritable.ToString());
            md += MDUtil.MDTableRow("Section Flag Preload", SectionFlagPreload.ToString());
            md += MDUtil.MDTableRow("Section Flag Executable", SectionFlagExecutable.ToString());
            md += MDUtil.MDTableRow("Section Flag Inserted File", SectionFlagInsertedFile.ToString());
            md += MDUtil.MDTableRow("Section Flag Head Page Read Only", SectionFlagHeadPageRO.ToString());
            md += MDUtil.MDTableRow("Section Flag Tail Page Read Only", SectionFlagTailPageRO.ToString());
            md += MDUtil.MDTableRow("Virtual Address", VirtualAddress.ToString("X"));
            md += MDUtil.MDTableRow("Virtual Size", VirtualSize.ToString("X"));
            md += MDUtil.MDTableRow("Raw Address", RawAddress.ToString("X"));
            md += MDUtil.MDTableRow("Raw Size", RawSize.ToString("X"));
            md += MDUtil.MDTableRow("Section Name Address", SectionNameAddress.ToString("X"));
            md += MDUtil.MDTableRow("Section Name", SectionName);
            md += MDUtil.MDTableRow("Section Name Reference Count", SectionNameRefCount.ToString("X"));
            md += MDUtil.MDTableRow("Head Shared Page Reference Count Address", HeadSharedRefAddr.ToString("X"));
            md += MDUtil.MDTableRow("Tail Shared Page Reference Count Address", TailSharedRefAddr.ToString("X"));
            md += MDUtil.MDTableRow("Section Digest", BitConverter.ToString(SectionDigest).Replace("-", " "));

            return md;
        }
    }
}
