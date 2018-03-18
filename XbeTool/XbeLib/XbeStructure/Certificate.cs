using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XbeLib.Utility;
using XMarkDown;

namespace XbeLib.XbeStructure
{
    public class Certificate
    {

        private byte[] _Certificate;

        private byte[] _SizeOfCertificate;           // 0x00 [0x04 bytes]
        public long SizeOfCertificate;

        private byte[] _TimeDate;                    // 0x04 [0x04 bytes]
        public long TimeDate;

        private byte[] _TitleID;                     // 0x08 [0x04 bytes]
        public long TitleID;

        private byte[] _TitleName;                   // 0x0C [0x50 bytes]
        public string TitleName;

        private byte[] _AlternateTitleIDs;           // 0x5C [0x40 bytes]
        public long[] AlternateTitleIDs;

        private byte[] _AllowedMedia;                // 0x9C [0x04 bytes]
        public long AllowedMedia;
        public bool MEDIA_TYPE_HARD_DISK;
        public bool MEDIA_TYPE_DVD_X2;
        public bool MEDIA_TYPE_DVD_CD;
        public bool MEDIA_TYPE_CD;
        public bool MEDIA_TYPE_DVD_5_RO;
        public bool MEDIA_TYPE_DVD_9_RO;
        public bool MEDIA_TYPE_DVD_5_RW;
        public bool MEDIA_TYPE_DVD_9_RW;
        public bool MEDIA_TYPE_DONGLE;
        public bool MEDIA_TYPE_MEDIA_BOARD;
        public bool MEDIA_TYPE_NONSECURE_HARD_DISK;
        public bool MEDIA_TYPE_NONSECURE_MODE;
        public bool MEDIA_TYPE_MEDIA_MASK;

        private byte[] _GameRegion;                  // 0xA0 [0x04 bytes]
        public long GameRegion;
        public bool GAME_REGION_NA;
        public bool GAME_REGION_JAPAN;
        public bool GAME_REGION_RESTOFWORLD;
        public bool GAME_REGION_MANUFACTURING;

        private byte[] _GameRatings;                 // 0xA4 [0x04 bytes]
        public long GameRatings;

        private byte[] _DiskNumber;                  // 0xA8 [0x04 bytes]
        public long DiskNumber;

        private byte[] _Version;                     // 0xAC [0x04 bytes]
        public long Version;

        public byte[] LANKey;                        // 0xB0 [0x10 bytes]

        public byte[] SignatureKey;                  // 0xC0 [0x10 bytes]

        private byte[] _AlternateSignatureKeys;      // 0xD0 [0x100 bytes]
        public byte[][] AlternateSignatureKeys;    

        public Certificate(byte[] certificate)
        {
            _Certificate = certificate;

            _SizeOfCertificate = Util.SubArray(certificate, 0x00, 0x04);
            SizeOfCertificate = BitConverter.ToUInt32(_SizeOfCertificate, 0);

            _TimeDate = Util.SubArray(certificate, 0x04, 0x04);
            TimeDate = BitConverter.ToUInt32(_TimeDate, 0);

            _TitleID = Util.SubArray(certificate, 0x08, 0x04);
            TitleID = BitConverter.ToUInt32(_TitleID, 0);

            _TitleName = Util.SubArray(certificate, 0x0C, 0x50);
            TitleName = Encoding.Unicode.GetString(_TitleName).TrimEnd('\0');

            _AlternateTitleIDs = Util.SubArray(certificate, 0x5C, 0x40);
            AlternateTitleIDs = new long[4];
            AlternateTitleIDs[0] = BitConverter.ToUInt32(Util.SubArray(_AlternateTitleIDs, 0, 4), 0);
            AlternateTitleIDs[1] = BitConverter.ToUInt32(Util.SubArray(_AlternateTitleIDs, 4, 4), 0);
            AlternateTitleIDs[2] = BitConverter.ToUInt32(Util.SubArray(_AlternateTitleIDs, 8, 4), 0);
            AlternateTitleIDs[3] = BitConverter.ToUInt32(Util.SubArray(_AlternateTitleIDs, 12, 4), 0);

            _AllowedMedia = Util.SubArray(certificate, 0x9C, 0x04);
            AllowedMedia = BitConverter.ToUInt32(_AllowedMedia, 0);
            var mediaMask = (Enum.AllowedMedia)AllowedMedia;
            MEDIA_TYPE_HARD_DISK = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_HARD_DISK);
            MEDIA_TYPE_DVD_X2 = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_X2);
            MEDIA_TYPE_DVD_CD = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_CD);
            MEDIA_TYPE_CD = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_CD);
            MEDIA_TYPE_DVD_5_RO = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_5_RO);
            MEDIA_TYPE_DVD_9_RO = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_9_RO);
            MEDIA_TYPE_DVD_5_RW = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_5_RW);
            MEDIA_TYPE_DVD_9_RW = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DVD_9_RW);
            MEDIA_TYPE_DONGLE = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_DONGLE);
            MEDIA_TYPE_MEDIA_BOARD = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_MEDIA_BOARD);
            MEDIA_TYPE_NONSECURE_HARD_DISK = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_NONSECURE_HARD_DISK);
            MEDIA_TYPE_NONSECURE_MODE = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_NONSECURE_MODE);
            MEDIA_TYPE_MEDIA_MASK = mediaMask.HasFlag(Enum.AllowedMedia.XBEIMAGE_MEDIA_TYPE_MEDIA_MASK);

            _GameRegion = Util.SubArray(certificate, 0xA0, 0x04);
            GameRegion = BitConverter.ToUInt32(_GameRegion, 0);
            var regionMask = (Enum.GameRegions)GameRegion;
            GAME_REGION_NA = regionMask.HasFlag(Enum.GameRegions.XBEIMAGE_GAME_REGION_NA);
            GAME_REGION_JAPAN = regionMask.HasFlag(Enum.GameRegions.XBEIMAGE_GAME_REGION_JAPAN);
            GAME_REGION_RESTOFWORLD = regionMask.HasFlag(Enum.GameRegions.XBEIMAGE_GAME_REGION_RESTOFWORLD);
            GAME_REGION_MANUFACTURING = regionMask.HasFlag(Enum.GameRegions.XBEIMAGE_GAME_REGION_MANUFACTURING);

            _GameRatings = Util.SubArray(certificate, 0xA4, 0x04);
            GameRatings = BitConverter.ToUInt32(_GameRatings, 0);

            _DiskNumber = Util.SubArray(certificate, 0xA8, 0x04);
            DiskNumber = BitConverter.ToUInt32(_DiskNumber, 0);

            _Version = Util.SubArray(certificate, 0xAC, 0x04);
            Version = BitConverter.ToUInt32(_Version, 0);

            LANKey = Util.SubArray(certificate, 0xB0, 0x10);

            SignatureKey = Util.SubArray(certificate, 0xC0, 0x10);

            _AlternateSignatureKeys = Util.SubArray(certificate, 0xD0, 0x100);
            AlternateSignatureKeys = new byte[16][];

            for (int i = 0; i < 16; i++)
            {
                AlternateSignatureKeys[i] = Util.SubArray(_AlternateSignatureKeys, i * 16, 16);
            }
        }


        public string GenerateMD()
        {
            string md = "# XBE Certificate\n\n";

            md += MDUtil.MDTableHeader("Field Name", "Description");
            md += MDUtil.MDTableRow("Size of Certificate", SizeOfCertificate.ToString("X"));
            DateTime dtTimeDate = DateTimeOffset.FromUnixTimeSeconds(TimeDate).DateTime;
            md += MDUtil.MDTableRow("Time / Date", TimeDate.ToString("X") + " (" + dtTimeDate + ")");
            md += MDUtil.MDTableRow("Title ID", TitleID.ToString("X8"));
            md += MDUtil.MDTableRow("Title Name", TitleName);
            md += MDUtil.MDTableRow("Alternate Title ID", AlternateTitleIDs[0].ToString("X8"));
            md += MDUtil.MDTableRow("Alternate Title ID", AlternateTitleIDs[1].ToString("X8"));
            md += MDUtil.MDTableRow("Alternate Title ID", AlternateTitleIDs[2].ToString("X8"));
            md += MDUtil.MDTableRow("Alternate Title ID", AlternateTitleIDs[3].ToString("X8"));
            md += MDUtil.MDTableRow("Allowed Media", AllowedMedia.ToString("X"));

            md += MDUtil.MDTableRow("Media Type Hard Disk", MEDIA_TYPE_HARD_DISK.ToString());
            md += MDUtil.MDTableRow("Media Type DVD X2", MEDIA_TYPE_DVD_X2.ToString());
            md += MDUtil.MDTableRow("Media Type DVD / CD", MEDIA_TYPE_DVD_CD.ToString());
            md += MDUtil.MDTableRow("Media Type CD", MEDIA_TYPE_CD.ToString());
            md += MDUtil.MDTableRow("Media Type DVD 5 RO", MEDIA_TYPE_DVD_5_RO.ToString());
            md += MDUtil.MDTableRow("Media Type DVD 9 RO", MEDIA_TYPE_DVD_9_RO.ToString());
            md += MDUtil.MDTableRow("Media Type DVD 5 RW", MEDIA_TYPE_DVD_5_RW.ToString());
            md += MDUtil.MDTableRow("Media Type DVD 9 RW", MEDIA_TYPE_DVD_9_RW.ToString());
            md += MDUtil.MDTableRow("Media Type Dongle", MEDIA_TYPE_DONGLE.ToString());
            md += MDUtil.MDTableRow("Media Type Media Board", MEDIA_TYPE_MEDIA_BOARD.ToString());
            md += MDUtil.MDTableRow("Media Type Nonsecure Hard Disk", MEDIA_TYPE_NONSECURE_HARD_DISK.ToString());
            md += MDUtil.MDTableRow("Media Type Nonsecure Mode", MEDIA_TYPE_NONSECURE_MODE.ToString());
            md += MDUtil.MDTableRow("Media Type Media Mask", MEDIA_TYPE_MEDIA_MASK.ToString());

            md += MDUtil.MDTableRow("Game Region", GameRegion.ToString("X"));
            md += MDUtil.MDTableRow("Game Region North America", GAME_REGION_NA.ToString());
            md += MDUtil.MDTableRow("Game Region Japan", GAME_REGION_JAPAN.ToString());
            md += MDUtil.MDTableRow("Game Region Rest of World", GAME_REGION_RESTOFWORLD.ToString());
            md += MDUtil.MDTableRow("Game Region Manufacturing", GAME_REGION_MANUFACTURING.ToString());

            if (GameRatings > -1 && GameRatings < 7)
            {
                md += MDUtil.MDTableRow("Game Ratings", GameRatings.ToString("X") + " (" + ((Enum.GameRatings)GameRatings).ToString() + ")");
            }
            else
            {
                md += MDUtil.MDTableRow("Game Ratings", GameRatings.ToString("X"));
            }

            md += MDUtil.MDTableRow("Disk Number", DiskNumber.ToString("X"));
            md += MDUtil.MDTableRow("Version", Version.ToString("X"));
            md += MDUtil.MDTableRow("LAN Key", BitConverter.ToString(LANKey).Replace("-", " "));
            md += MDUtil.MDTableRow("Signature Key", BitConverter.ToString(SignatureKey).Replace("-", " "));

            for (int i = 0; i < 16; i++)
            {
                md += MDUtil.MDTableRow("Alternate Signature Key", BitConverter.ToString(AlternateSignatureKeys[i]).Replace("-", " "));
            }
            

            return md;
        }
    }
}
