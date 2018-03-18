using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbeLib.Enum
{
    [Flags]
    public enum GameRegions : uint
    {
        XBEIMAGE_GAME_REGION_NA = 0x00000001,
        XBEIMAGE_GAME_REGION_JAPAN = 0x00000002,
        XBEIMAGE_GAME_REGION_RESTOFWORLD = 0x00000004,
        XBEIMAGE_GAME_REGION_MANUFACTURING = 0x80000000
    }
}
