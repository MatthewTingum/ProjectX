using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeLib.Enum
{
    [Flags]
    public enum SectionFlags : uint
    {
        Writable = 0x00000001,
        Preload = 0x00000002,
        Executable = 0x00000004,
        InsertedFile = 0x00000008,
        HeadPageReadOnly = 0x00000010,
        TailPageReadOnly = 0x00000020
    }
}
