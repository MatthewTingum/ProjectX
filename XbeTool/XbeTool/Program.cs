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
            string markDown = xbe.GenerateMD();
            File.WriteAllText(@".\README.MD", markDown);
        }
    }
}
