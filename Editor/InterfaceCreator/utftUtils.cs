using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace InterfaceCreator
{
    public static class utftUtils
    {
        public static Color GetUTFTColor(VGA_COLOR index)
        {
            Color res = Color.FromArgb(0, 0, 0);
            switch (index)
            {
                case VGA_COLOR.VGA_BLACK: res = Color.FromArgb(0, 0, 0); break;
                case VGA_COLOR.VGA_WHITE: res = Color.FromArgb(248, 252, 248); break;
                case VGA_COLOR.VGA_RED: res = Color.FromArgb(248, 0, 0); break;
                case VGA_COLOR.VGA_GREEN: res = Color.FromArgb(0, 128, 0); break;
                case VGA_COLOR.VGA_BLUE: res = Color.FromArgb(0, 0, 248); break;
                case VGA_COLOR.VGA_SILVER: res = Color.FromArgb(192, 192, 192); break;
                case VGA_COLOR.VGA_GRAY: res = Color.FromArgb(128, 128, 128); break;
                case VGA_COLOR.VGA_MAROON: res = Color.FromArgb(128, 0, 0); break;
                case VGA_COLOR.VGA_YELLOW: res = Color.FromArgb(248, 252, 0); break;
                case VGA_COLOR.VGA_OLIVE: res = Color.FromArgb(128, 128, 0); break;
                case VGA_COLOR.VGA_LIME: res = Color.FromArgb(0, 252, 0); break;
                case VGA_COLOR.VGA_AQUA: res = Color.FromArgb(0, 252, 248); break;
                case VGA_COLOR.VGA_TEAL: res = Color.FromArgb(0, 128, 128); break;
                case VGA_COLOR.VGA_NAVY: res = Color.FromArgb(0, 0, 128); break;
                case VGA_COLOR.VGA_FUCHSIA: res = Color.FromArgb(248, 0, 248); break;
                case VGA_COLOR.VGA_PURPLE: res = Color.FromArgb(128, 0, 128); break;
            }
            return res;
        }

        public static UInt16 GetUTFTColorBytes(VGA_COLOR index)
        {
            UInt16 res = 0;
            /*
#define VGA_BLACK		0x0000
#define VGA_WHITE		0xFFFF
#define VGA_RED			0xF800
#define VGA_GREEN		0x0400
#define VGA_BLUE		0x001F
#define VGA_SILVER		0xC618
#define VGA_GRAY		0x8410
#define VGA_MAROON		0x8000
#define VGA_YELLOW		0xFFE0
#define VGA_OLIVE		0x8400
#define VGA_LIME		0x07E0
#define VGA_AQUA		0x07FF
#define VGA_TEAL		0x0410
#define VGA_NAVY		0x0010
#define VGA_FUCHSIA		0xF81F
#define VGA_PURPLE		0x8010
             */
            switch (index)
            {
                case VGA_COLOR.VGA_BLACK: res = 0x0000; break;
                case VGA_COLOR.VGA_WHITE: res = 0xFFFF; break;
                case VGA_COLOR.VGA_RED: res = 0xF800; break;
                case VGA_COLOR.VGA_GREEN: res = 0x0400; break;
                case VGA_COLOR.VGA_BLUE: res = 0x001F; break;
                case VGA_COLOR.VGA_SILVER: res = 0xC618; break;
                case VGA_COLOR.VGA_GRAY: res = 0x8410; break;
                case VGA_COLOR.VGA_MAROON: res = 0x8000; break;
                case VGA_COLOR.VGA_YELLOW: res = 0xFFE0; break;
                case VGA_COLOR.VGA_OLIVE: res = 0x8400; break;
                case VGA_COLOR.VGA_LIME: res = 0x07E0; break;
                case VGA_COLOR.VGA_AQUA: res = 0x07FF; break;
                case VGA_COLOR.VGA_TEAL: res = 0x0410; break;
                case VGA_COLOR.VGA_NAVY: res = 0x0010; break;
                case VGA_COLOR.VGA_FUCHSIA: res = 0xF81F; break;
                case VGA_COLOR.VGA_PURPLE: res = 0x8010; break;
            }
            return res;
        }

        public static void Save2Bytes(System.IO.FileStream fs, UInt16 data)
        {
            byte bb = (byte)((data >> 8) & 0xff);
            fs.WriteByte(bb);
            bb = (byte)(data & 0xff);
            fs.WriteByte(bb);
        }

        public static UInt16 Read2Bytes(System.IO.FileStream fs)
        {
            byte b = (byte)fs.ReadByte();
            UInt16 res = (UInt16)(b << 8);
            b = (byte)fs.ReadByte();
            res = (UInt16)(res | b);

            return res;
        }

        public static VGA_COLOR GetUTFTColor(UInt16 clr)
        {
            VGA_COLOR res = VGA_COLOR.VGA_BLACK;
            switch (clr)
            {
                case 0x0000: res = VGA_COLOR.VGA_BLACK; break;
                case 0xFFFF: res = VGA_COLOR.VGA_WHITE; break;
                case 0xF800: res = VGA_COLOR.VGA_RED; break;
                case 0x0400: res = VGA_COLOR.VGA_GREEN; break;
                case 0x001F: res = VGA_COLOR.VGA_BLUE; break;
                case 0xC618: res = VGA_COLOR.VGA_SILVER; break;
                case 0x8410: res = VGA_COLOR.VGA_GRAY; break;
                case 0x8000: res = VGA_COLOR.VGA_MAROON; break;
                case 0xFFE0: res = VGA_COLOR.VGA_YELLOW; break;
                case 0x8400: res = VGA_COLOR.VGA_OLIVE; break;
                case 0x07E0: res = VGA_COLOR.VGA_LIME; break;
                case 0x07FF: res = VGA_COLOR.VGA_AQUA; break;
                case 0x0410: res = VGA_COLOR.VGA_TEAL; break;
                case 0x0010: res = VGA_COLOR.VGA_NAVY; break;
                case 0xF81F: res = VGA_COLOR.VGA_FUCHSIA; break;
                case 0x8010: res = VGA_COLOR.VGA_PURPLE; break;
            }
            return res;
        }
    }
}
