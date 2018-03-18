using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib.Utility;
using XMarkDown;

namespace XbeLib.XbeStructure
{
    public class LibraryVersion
    {

        public string LibraryName;      // 0x00 [0x08 bytes]

        private byte[] _MajorVersion;   // 0x08 [0x02 bytes]
        public int MajorVersion;

        private byte[] _MinorVersion;   // 0x0A [0x02 bytes]
        public int MinorVersion;

        private byte[] _BuildVersion;   // 0x0C [0x02 bytes]
        public int BuildVersion;

        private string _FullVersion;

        private byte[] _LibraryFlags;   // 0x0E [0x02 bytes]
        public Int16 LibraryFlags;
        public int QFEVersion;
        public int Approved;
        public bool DebugBuild;

        public LibraryVersion(byte[] library)
        {
            LibraryName = Encoding.ASCII.GetString(Util.SubArray(library, 0, 8)).TrimEnd('\0');

            _MajorVersion = Util.SubArray(library, 0x08, 0x02);
            MajorVersion = BitConverter.ToInt16(_MajorVersion, 0);

            _MinorVersion = Util.SubArray(library, 0x0A, 0x02);
            MinorVersion = BitConverter.ToInt16(_MinorVersion, 0);

            _BuildVersion = Util.SubArray(library, 0x0C, 0x02);
            BuildVersion = BitConverter.ToInt16(_BuildVersion, 0);

            _FullVersion = String.Format("{0}.{1}.{2}", MajorVersion, MinorVersion, BuildVersion);

            _LibraryFlags = Util.SubArray(library, 0x0E, 0x02);
            LibraryFlags = BitConverter.ToInt16(_LibraryFlags, 0);
            QFEVersion = (ushort)(((ushort)Enum.LibraryFlags.QFEVersion) & LibraryFlags);
            Approved = (((ushort)Enum.LibraryFlags.Approved) & LibraryFlags) >> 13;
            int debug = (ushort)(((ushort)Enum.LibraryFlags.DebugBuild) & LibraryFlags) >> 15;
            if (debug == 1) { DebugBuild = true; }
            else { DebugBuild = false; }
        }

        public string GenerateMD()
        {
            string md = "# XBE Library Version - " + LibraryName + " - " + _FullVersion + "\n\n";

            md += MDUtil.MDTableHeader("Field Name", "Description");
            md += MDUtil.MDTableRow("Library Name", LibraryName);
            md += MDUtil.MDTableRow("Major Version", MajorVersion.ToString());
            md += MDUtil.MDTableRow("Minor Version", MinorVersion.ToString());
            md += MDUtil.MDTableRow("Build Version", BuildVersion.ToString());
            md += MDUtil.MDTableRow("Library Flags", LibraryFlags.ToString("X"));
            md += MDUtil.MDTableRow("QFE Version", QFEVersion.ToString());
            md += MDUtil.MDTableRow("Approved", ((Enum.Approved)Approved).ToString());
            md += MDUtil.MDTableRow("Debug Build", DebugBuild.ToString());

            return md;
        }
    }
}
