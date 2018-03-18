using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeLib.Enum
{
    [Flags]
    public enum LibraryFlags
    {
        QFEVersion = 0x1FFF,    // 13-Bit Mask
        Approved = 0x6000,      // 02-Bit Mask    // Approved? (0:no, 1:possibly, 2:yes)
        DebugBuild = 0x8000     // 01-Bit Mask
    }

    public enum Approved
    {
        No = 0,
        Possibly = 1,
        Yes = 2
    }
}
