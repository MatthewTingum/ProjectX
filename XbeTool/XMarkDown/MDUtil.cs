using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMarkDown
{
    public static class MDUtil
    {

        public static string MDTableHeader(string column1, string column2)
        {
            return "| " + column1 + " | " + column2 + " |\n|---|---|\n";
        }

        public static string MDTableRow(string column1, string column2)
        {
            return "| " + column1 + " | " + column2 + " |\n";
        }

        public static string MDTableHeader(string column1, string column2, string column3, string column4, string column5)
        {
            return "| " + column1 + " | " + column2 + " | " + column3 + " | " + column4 + " | " + column5 + " |\n|---|---|\n";
        }

        public static string MDTableRow(string column1, string column2, string column3, string column4, string column5)
        {
            return "| " + column1 + " | " + column2 + " | " + column3 + " | " + column4 + " | " + column5 + " |\n";
        }
    }
}
