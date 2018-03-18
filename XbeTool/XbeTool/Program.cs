using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib;
using XbeLib.XbeStructure;

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
            //Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\");
            Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\sections\");
            Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\libraries\");
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\ImageHeader.MD", mdImageHeader);
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\Certificate.MD", mdCertificate);

            foreach (SectionHeader section in xbe.SectionHeaders)
            {
                string mdSectionHeader = section.GenerateMD();
                File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\sections\" + section.SectionName + ".MD", mdSectionHeader);
            }

            foreach (LibraryVersion version in xbe.LibraryVersions)
            {
                string mdLibraryVersion = version.GenerateMD();
                File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\libraries\" + version.LibraryName + ".MD", mdLibraryVersion);
            }

            string mdTLS = xbe.TLS.GenerateMD();
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\TLS.MD", mdTLS);
        }
    }
}
