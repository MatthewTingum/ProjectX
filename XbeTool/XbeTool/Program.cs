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
            Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\");
            Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\");
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\ImageHeader.MD", mdImageHeader);
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\Certificate.MD", mdCertificate);

            foreach (SectionHeader section in xbe.SectionHeaders)
            {
                string mdSectionHeader = section.GenerateMD();
                File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\" + section.SectionName + ".MD", mdSectionHeader);
            }

            foreach (LibraryVersion version in xbe.LibraryVersions)
            {
                string mdLibraryVersion = version.GenerateMD();
                File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\" + version.LibraryName + ".MD", mdLibraryVersion);
            }

            string mdTLS = xbe.TLS.GenerateMD();
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\TLS.MD", mdTLS);

            string mdMain = "";
            mdMain += "# " + titleName + "\n\n";
            mdMain += "[Xbe Structure](./wiki/xbe/README.MD)\n\n";
            mdMain += "Assets\n\n";
            mdMain += "Archives\n\n";
            mdMain += "Debug Content\n\n";

            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\README.MD", mdMain);

            string mdStructMain = xbe.GenerateStructMainMD();
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\README.MD", mdStructMain);

            string mdLibsMain = xbe.GenerateLibrariesMainMD();
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\README.MD", mdLibsMain);

            string mdSectionsMain = xbe.GenerateSectionsMainMD();
            File.WriteAllText(@"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\README.MD", mdSectionsMain);

        }
    }
}
