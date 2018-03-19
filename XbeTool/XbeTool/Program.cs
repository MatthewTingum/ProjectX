using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib;
using XbeLib.XbeStructure;
using XbeTool.Utility;

namespace XbeTool
{
    class Program
    {
        static void Main(string[] args)
        {

            string path;
            string directory;

            if (args.Length > 0 && File.Exists(args[0]))
            {
                path = args[0];
                directory = Path.GetDirectoryName(path);
            }
            else
            {
                path = @"C:\Users\matth\Documents\Xbox\ISO\BurgerKing\default.xbe";
                directory = Path.GetDirectoryName(path);
                //Console.WriteLine("File error: File does not exist. Try dragging and dropping an xbe onto the program.");
                //Console.ReadLine();
                //return;
            }


            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            XbeFile xbe = new XbeFile(File.ReadAllBytes(path));

            string mdImageHeader = xbe.ImageHeader.GenerateMD();
            string mdCertificate = xbe.Certificate.GenerateMD();

            string titleName = xbe.Certificate.TitleName;

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                titleName = titleName.Replace(c.ToString(), "");
            }
            //Directory.CreateDirectory(@"..\..\..\..\Games\" + titleName + @"\wiki\");
            Directory.CreateDirectory(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\");
            Directory.CreateDirectory(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\");
            Directory.CreateDirectory(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\assets\");
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\ImageHeader.MD", mdImageHeader);
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\Certificate.MD", mdCertificate);

            foreach (SectionHeader section in xbe.SectionHeaders)
            {
                string mdSectionHeader = section.GenerateMD();
                File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\" + section.SectionName + ".MD", mdSectionHeader);
            }

            foreach (LibraryVersion version in xbe.LibraryVersions)
            {
                string mdLibraryVersion = version.GenerateMD();
                File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\" + version.LibraryName + ".MD", mdLibraryVersion);
            }

            string mdTLS = xbe.TLS.GenerateMD();
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\TLS.MD", mdTLS);

            string mdMain = "";
            mdMain += "# " + titleName + "\n\n";
            mdMain += "[Xbe Structure](./wiki/xbe/README.MD)\n\n";
            mdMain += "[Assets](./wiki/assets/README.MD)\n\n";
            mdMain += "Archives\n\n";
            mdMain += "Debug Content\n\n";

            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\README.MD", mdMain);

            string mdStructMain = xbe.GenerateStructMainMD();
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\README.MD", mdStructMain);

            string mdLibsMain = xbe.GenerateLibrariesMainMD();
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\libraries\README.MD", mdLibsMain);

            string mdSectionsMain = xbe.GenerateSectionsMainMD();
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\xbe\sections\README.MD", mdSectionsMain);

            // Assets README
            string mdAssets = "";
            mdAssets += "# Assets\n\n";
            mdAssets += "[Directory Tree](./DirectoryTree.MD)\n\n";
            mdAssets += "[Asset Details](./AssetDetails.MD)\n\n";
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\assets\README.MD", mdAssets);

            // Directory Structure
            Directory.CreateDirectory(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\assets\");
            string dirTree = Util.GenerateAssetMD(directory);
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\assets\DirectoryTree.MD", dirTree);

            // Asset Details
            string mdAssetDetails = Util.GenerateAssetDetailMD(directory);
            File.WriteAllText(exeDirectory + @"..\..\..\..\Games\" + titleName + @"\wiki\assets\AssetDetails.MD", mdAssetDetails);

        }
    }
}
