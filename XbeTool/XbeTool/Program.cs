using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib;

namespace XbeTool
{
    class Program
    {
        static void Main(string[] args)
        {
            XbeFile xbe = new XbeFile(File.ReadAllBytes(@".\default.xbe"));

            string mdImageHeader = xbe.ImageHeader.GenerateMD();
            string mdCertificate = xbe.Certificate.GenerateMD();

            string titleName = xbe.Certificate.TitleName;
            Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\");
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\ImageHeader.MD", mdImageHeader);
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\Certificate.MD", mdCertificate);
        }
    }
}
