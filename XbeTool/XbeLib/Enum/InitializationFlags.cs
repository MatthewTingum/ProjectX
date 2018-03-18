using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeLib.Enum
{
    [Flags]
    public enum InitializationFlags
    {
        MountUtilityDrive = 0x00000001,
        FormatUtilityDrive = 0x00000002,
        Limit64Megabytes = 0x00000004,
        DontSetupHarddisk = 0x00000008
    }
}
