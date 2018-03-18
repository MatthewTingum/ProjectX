using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib.Utility;
using XMarkDown;

namespace XbeLib.XbeStructure
{
    public class TLS
    {

        private byte[] _DataStartAddress;       // 0x00 [0x04 bytes]
        public long DataStartAddress;

        private byte[] _DataEndAddress;         // 0x04 [0x04 bytes]
        public long DataEndAddress;

        private byte[] _TLSIndexAddress;        // 0x08 [0x04 bytes]
        public long TLSIndexAddress;

        private byte[] _TLSCallbackAddress;     // 0x0C [0x04 bytes]
        public long TLSCallbackAddress;

        private byte[] _SizeOfZeroFill;         // 0x10 [0x04 bytes]
        public long SizeOfZeroFill;

        private byte[] _Characteristics;        // 0x14 [0x04 bytes]
        public long Characteristics;

        public TLS(byte[] tls)
        {
            _DataStartAddress = Util.SubArray(tls, 0x00, 0x04);
            DataStartAddress = BitConverter.ToUInt32(_DataStartAddress, 0);

            _DataEndAddress = Util.SubArray(tls, 0x04, 0x04);
            DataEndAddress = BitConverter.ToUInt32(_DataEndAddress, 0);

            _TLSIndexAddress = Util.SubArray(tls, 0x08, 0x04);
            TLSIndexAddress = BitConverter.ToUInt32(_TLSIndexAddress, 0);

            _TLSCallbackAddress = Util.SubArray(tls, 0x0C, 0x04);
            TLSCallbackAddress = BitConverter.ToUInt32(_TLSCallbackAddress, 0);

            _SizeOfZeroFill = Util.SubArray(tls, 0x10, 0x04);
            SizeOfZeroFill = BitConverter.ToUInt32(_SizeOfZeroFill, 0);

            _Characteristics = Util.SubArray(tls, 0x14, 0x04);
            Characteristics = BitConverter.ToUInt32(_Characteristics, 0);
        }

        public string GenerateMD()
        {
            string md = "# XBE TLS\n\n";

            md += MDUtil.MDTableHeader("Field Name", "Description");
            md += MDUtil.MDTableRow("Data Start Address", DataStartAddress.ToString("X"));
            md += MDUtil.MDTableRow("Data End Address", DataEndAddress.ToString("X"));
            md += MDUtil.MDTableRow("TLS Index Address", TLSIndexAddress.ToString("X"));
            md += MDUtil.MDTableRow("TLS Callback Address", TLSCallbackAddress.ToString("X"));
            md += MDUtil.MDTableRow("Size of Zero Fill", SizeOfZeroFill.ToString("X"));
            md += MDUtil.MDTableRow("Characteristics", Characteristics.ToString("X"));

            return md;
        }
    }
}
