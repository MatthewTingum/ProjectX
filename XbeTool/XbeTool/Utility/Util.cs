using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public static string DirTree(string directory, string basePath, bool root)
        {

            string files = "";

            try
            {

                string[] getDirectories;

                if (root)
                {
                    List<string> tempDirectories = new List<string>();
                    tempDirectories.AddRange(Directory.GetDirectories(directory));
                    tempDirectories.Add(directory);
                    getDirectories = tempDirectories.ToArray();
                }
                else
                {
                    getDirectories = Directory.GetDirectories(directory);
                }

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

                    files += DirTree(getDirectories[j], basePath, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return files;
        }

        public static string DirTree(string directory)
        {
            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            string[] directories = Directory.GetDirectories(directory, "*", SearchOption.AllDirectories);


            List<string> directoriesList = directories.ToList();
            directoriesList.Sort();
            directories = directoriesList.ToArray();

            string tree = "";

            // List root directory files first
            foreach (string file in files)
            {
                string[] folders = file.Substring(directory.Length).Split('\\');
                if (folders.Length == 2)
                {
                    tree += folders[folders.Length - 1] + "\n";
                }
            }

            foreach (string dir in directories)
            {
                string originalDir = dir;

                // Find depth of directory
                string[] folders = dir.Substring(directory.Length).Split('\\');

                string tabs = "";
                for (int i = 0; i < folders.Length - 3; i++)
                {
                    tabs += "|\t";
                }

                // if not a root directory, link to the parent directory
                if (folders.Length != 2)
                {
                    tabs += "|-------";
                }

                tree += tabs + "\\" + folders[folders.Length - 1] + "\n";

                tabs = tabs.Replace("|-------", "|   ");

                if (folders.Length != 2)
                {
                    tabs += "\t+-------";
                }
                else
                {
                    tabs += "+-------";
                }

                // Look for files belonging to directory
                foreach (string file in files)
                {
                    DirectoryInfo di1 = new DirectoryInfo(originalDir);
                    DirectoryInfo di2 = new DirectoryInfo(file);
                    if (di2.Parent.FullName == di1.FullName)
                    {
                        string[] fileName = file.Split('\\');
                        
                        
                        tree += tabs + " " + fileName[fileName.Length - 1] + "\n";
                    }
                }
            }

            return tree;
        }

        public static string GenerateAssetMD(string directory)
        {
            string md = "# Assets\n\n";

            md += "## Directory Tree\n\n";
            //md += String.Format("```\n{0}```\n\n", DirTree(directory, directory, true));
            md += String.Format("```\n{0}```\n\n", DirTree(directory));

            return md;
        }

        public static string GenerateAssetDetailMD(string directory)
        {

            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            List<string> filesList = new List<string>(files);
            filesList.Sort();

            string md = "```\n# Asset Details\n\n";
            md += XMarkDown.MDUtil.MDTableHeader("File Name", "Size", "MD5", "File Type", "Comments");

            foreach (string file in filesList)
            {
                // TODO: best guess file type detection
                byte[] hash;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(file))
                    {
                        hash = md5.ComputeHash(stream);
                    }
                }

                FileInfo fileInfo = new FileInfo(file);
                md += XMarkDown.MDUtil.MDTableRow(fileInfo.Name, fileInfo.Length.ToString(), BitConverter.ToString(hash).Replace("-", ""), "", "");
            }

            md += "```";
            return md;
        }
    }
}
