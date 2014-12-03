using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace InterfaceCreator
{
    public enum VGA_COLOR
    {
        VGA_BLACK,
        VGA_WHITE,
        VGA_RED,
        VGA_GREEN,
        VGA_BLUE,
        VGA_SILVER,
        VGA_GRAY,
        VGA_MAROON,
        VGA_YELLOW,
        VGA_OLIVE,
        VGA_LIME,
        VGA_AQUA,
        VGA_TEAL,
        VGA_NAVY,
        VGA_FUCHSIA,
        VGA_PURPLE
    };
    public class TInterfaceElement
    {
        
        public object border;
        public byte ID;

        public int X
        {
            get
            {
                return ((Control)border).Left;
            }
            set
            {
                ((Control)border).Left = value;
            }
        }
        public int Y
        {
            get
            {
                return ((Control)border).Top;
            }
            set
            {
                ((Control)border).Top = value;
            }
        }
        public int width
        {
            get
            {
                return ((Control)border).Width;
            }
            set
            {
                ((Control)border).Width = value;
            }
        }
        public int heigth
        {
            get
            {
                return ((Control)border).Height;
            }
            set
            {
                ((Control)border).Height = value;
            }
        }

        private string _itemname;
        public string ItemName
        {
            get
            {
                return _itemname;
            }
            set
            {
                if (Text == _itemname) Text = value;
                _itemname = value;
                ((Control)border).Tag = value;
            }
        }
        public override string ToString()
        {
            return ItemName;
        }
        private Boolean _canSelect;
        public Boolean CanSelect
        {
            get
            {
                return _canSelect;
            }
            set
            {
                _canSelect = value;
            }
        }

        private VGA_COLOR _backcolor;
        public VGA_COLOR BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {
                _backcolor = value;
                ((Control)border).BackColor = utftUtils.GetUTFTColor(_backcolor);
                ((Control)border).Refresh();
            }
        }

        private VGA_COLOR _fontcolor;
        public VGA_COLOR FontColor
        {
            get
            {
                return _fontcolor;
            }
            set
            {
                _fontcolor = value;
                ((Control)border).ForeColor = utftUtils.GetUTFTColor(_fontcolor);
                ((Control)border).Refresh();
            }
        }

        public string Text
        {
            get
            {
                return ((Control)border).Text;
            }
            set
            {
                ((Control)border).Text = value;
            }
        }
        public string ItemType
        {
            get
            {
                if (border.GetType() == typeof(Button)) return "Border";
                else
                    if (border.GetType() == typeof(Label)) return "Label";
                    else
                        return "NoName";
            }
        }
        public byte GetItemTypeNumber()
        {
            if (border.GetType() == typeof(Button)) return 1;
            else
                if (border.GetType() == typeof(Label)) return 3;
                else
                    return 0;
        }
        public void Dispose()
        {
            if (border.GetType() == typeof(Button)) ((Button)border).Dispose();
            else
                if (border.GetType() == typeof(Label)) ((Label)border).Dispose();
        }

        public void Save(System.IO.FileStream fs, System.Xml.XmlWriter fi)
        {
            UInt16 eSize = (UInt16)(Text.Length + 14);
            utftUtils.Save2Bytes(fs, eSize);
            fs.WriteByte((byte)GetItemTypeNumber());
            fs.WriteByte(ID);
            utftUtils.Save2Bytes(fs, (UInt16)X);
            utftUtils.Save2Bytes(fs, (UInt16)Y);
            fs.WriteByte((byte)width);
            fs.WriteByte((byte)heigth);
            UInt16 clr = utftUtils.GetUTFTColorBytes(BackColor);
            utftUtils.Save2Bytes(fs, clr);
            clr = utftUtils.GetUTFTColorBytes(FontColor);
            utftUtils.Save2Bytes(fs, clr);
            fs.WriteByte(Convert.ToByte(_canSelect));
            fs.WriteByte((byte)Text.Length);
            char[] arr = Text.ToCharArray();
            for (int i = 0; i < Text.Length; i++)
                fs.WriteByte((byte)arr[i]);

            fi.WriteStartElement("Element");
            fi.WriteElementString("ItemType", ItemType);
            fi.WriteElementString("ID", ID.ToString());
            fi.WriteElementString("X", X.ToString());
            fi.WriteElementString("Y", Y.ToString());
            fi.WriteElementString("Width", width.ToString());
            fi.WriteElementString("Height", heigth.ToString());
            fi.WriteElementString("BackColor", BackColor.ToString());
            fi.WriteElementString("FontColor", FontColor.ToString());
            fi.WriteElementString("CanSelect", CanSelect.ToString());
            fi.WriteElementString("Text", Text);
            fi.WriteElementString("ItemName", ItemName);
            fi.WriteEndElement();
        }
    }
}
