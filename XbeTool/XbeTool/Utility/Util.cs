using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeTool.Utility
{
    public static class Util
    {
        public static List<Tuple<string, byte[]>> DirSearch(string directory)
        {

            List<Tuple<string, byte[]>> files = new List<Tuple<string, byte[]>>();

            try
            {
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        files.Add(new Tuple<string, byte[]>(file, File.ReadAllBytes(file)));
                    }
                    files.AddRange(new List<Tuple<string, byte[]>>(DirSearch(dir)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return files;
        }

        public static string DirTree(string directory, string basePath)
        {

            string files = "";

            try
            {

                string[] getDirectories = Directory.GetDirectories(directory);

                for (int j = 0; j < getDirectories.Length; j++)
                {

                    string dirFormatted = getDirectories[j].Substring(basePath.Length);
                    string[] folders = dirFormatted.Split('\\');
                    dirFormatted = folders[folders.Length - 1];
                    string tabs = "";
                    string tabsDir = "";

                    for (int i = 0; i < folders.Length - 1; i++)
                    {

                        // directory formatting
                        if (i == folders.Length - 2)
                        {
                            tabsDir += "|---";
                            
                        }
                        else
                        {
                            tabsDir += "|\t";
                            
                        }
                        
                        // file formatting
                        if (i <= folders.Length - 2)
                        {
                            tabs += "|\t";
                        }
                        else
                        {
                            tabs += "\t";
                        }
                    }

                    dirFormatted = tabsDir + "\\" + dirFormatted;
                    tabs += "\t";

                    files += String.Format("{0}\n", dirFormatted);

                    foreach (string file in Directory.GetFiles(getDirectories[j]))
                    {
                        string[] split = file.Split('\\');
                        files += String.Format("{0}{1}\n", tabs, split[split.Length - 1]);
                    }

                    files += DirTree(getDirectories[j], basePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return files;
        }

        public static string GenerateAssetMD(string directory)
        {
            string md = "# Assets\n\n";

            md += "## Directory Tree\n\n";
            md += String.Format("```\n{0}```\n\n", DirTree(directory, directory));

            return md;
        }
    }
}
