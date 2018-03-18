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

        private byte[] _File;
        public ImageHeader ImageHeader;
        public Certificate Certificate;


        public XbeFile(byte[] file)
        {

            _File = file;
            ImageHeader = new ImageHeader(file);
            Certificate = new Certificate(Util.SubArray(file, ImageHeader.CertificateAddress - ImageHeader.BaseAddress, 0x1D0));
            
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
