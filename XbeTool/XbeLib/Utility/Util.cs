using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeLib.Utility
{

    // TODO: Is passing in the whole file really the best use of resources?
    public static class Util
    {
        public static byte[] SubArray(byte[] data, long index, long length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string GetNullTerminatedString(byte[] data, long address)
        {
            string value = "";

            while (data[address] != 0x00)
            {
                value += (char)data[address];
                address++;
            }

            return value;
        }

        public static string GetUnicodeString(byte[] data, long address)
        {
            string value = "";

            while (data[address] != 0x00 || data[address + 1] != 0x00)
            {
                value += (char)data[address];
                address += 2;
            }

            return value;
        }
    }
}
