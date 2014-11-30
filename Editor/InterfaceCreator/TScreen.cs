using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfaceCreator
{
            public delegate void cbElementsAddItem(TInterfaceElement item);

    public class TScreen
    {
        public Dictionary<string, TInterfaceElement> listitem = new Dictionary<string, TInterfaceElement>();
        
        private VGA_COLOR _BackColor;
        private VGA_COLOR _fontcolor;

        public System.Windows.Forms.Panel pnl;

        public int Width
        {
            get
            {
                return pnl.Width;
            }
            set
            {
                pnl.Width = value;
            }
        }
        public int Height
        {
            get
            {
                return pnl.Height;
            }
            set
            {
                pnl.Height = value;
            }
        }
        public VGA_COLOR BackColor
        {
            get
            {
                return _BackColor;
            }
            set
            {
                _BackColor = value;
                pnl.BackColor = utftUtils.GetUTFTColor(_BackColor);
                pnl.Refresh();
            }
        }
        public VGA_COLOR FontColor
        {
            get
            {
                return _fontcolor;
            }
            set
            {
                _fontcolor = value;
                pnl.ForeColor = utftUtils.GetUTFTColor(_fontcolor);
                pnl.Refresh();
            }
        }

        public MouseEventHandler MouseDown;
        public MouseEventHandler MouseMove;
        public ContextMenuStrip pmElement;
        public cbElementsAddItem AddItem;

        public void CreateElement()
        {
            Button btn = new Button();

            btn.Parent = pnl;
            btn.MouseDown += new MouseEventHandler(MouseDown);
            btn.MouseMove += new MouseEventHandler(MouseMove);

            btn.ContextMenuStrip = pmElement;
            btn.Text = "Border";
            btn.Refresh();

            TInterfaceElement item = new TInterfaceElement();
            item.border = btn;
            item.ItemName = "Border";
            item.BackColor = VGA_COLOR.VGA_BLACK;
            item.FontColor = VGA_COLOR.VGA_WHITE;
            Boolean err = true;
            int i = 0;
            while (err)
                try
                {
                    listitem.Add(item.ItemName, item);
                    btn.Tag = item.ItemName;
                    err = false;
                    AddItem(item);
                }
                catch (Exception ee)
                {
                    i++;
                    item.ItemName = "Border" + i.ToString();
                }
        }
        public void CreateLabel()
        {
            Label lbl = new Label();

            lbl.Parent = pnl;
            lbl.MouseDown += new MouseEventHandler(MouseDown);
            lbl.MouseMove += new MouseEventHandler(MouseMove);

            lbl.Text = "Label";
            lbl.ContextMenuStrip = pmElement;
            lbl.Refresh();

            TInterfaceElement item = new TInterfaceElement();
            item.border = lbl;
            item.ItemName = "Label";
            item.BackColor = VGA_COLOR.VGA_BLACK;
            item.FontColor = VGA_COLOR.VGA_WHITE;
            Boolean err = true;
            int i = 0;
            while (err)
                try
                {
                    listitem.Add(item.ItemName, item);
                    lbl.Tag = item.ItemName;
                    err = false;
                    AddItem(item);
                }
                catch (Exception ee)
                {
                    i++;
                    item.ItemName = "Label" + i.ToString();
                }
        }

        public void Save(System.IO.FileStream fs, System.Xml.XmlWriter fi, System.IO.FileStream fd)
        {
            byte lbl = 0;
            byte btn = 0;
            foreach (KeyValuePair<string, TInterfaceElement> e in listitem)
            {
                if (e.Value.GetItemTypeNumber() == 3)
                e.Value.ID = lbl++;
                else e.Value.ID = btn++;
            }
            fs.WriteByte(10);// размер данных экрана
            utftUtils.Save2Bytes(fs, (UInt16)Width);
            utftUtils.Save2Bytes(fs, (UInt16)Height);

            UInt16 clr = utftUtils.GetUTFTColorBytes(BackColor);
            utftUtils.Save2Bytes(fs, clr);
            clr = utftUtils.GetUTFTColorBytes(FontColor);
            utftUtils.Save2Bytes(fs, clr);

            fs.WriteByte((byte)lbl);
            fs.WriteByte((byte)btn);

            foreach (KeyValuePair<string, TInterfaceElement> e in listitem)
            {
                e.Value.Save(fs, fi);
            }
        }

        public void LoadScreen(System.IO.FileStream fs, ref byte cstr, ref byte cbtn)
        {
            byte eSize = (byte)fs.ReadByte();
            Width = utftUtils.Read2Bytes(fs);
            Height = utftUtils.Read2Bytes(fs);
            BackColor = utftUtils.GetUTFTColor(utftUtils.Read2Bytes(fs));
            FontColor = utftUtils.GetUTFTColor(utftUtils.Read2Bytes(fs));

            cstr = (byte)fs.ReadByte();
            cbtn = (byte)fs.ReadByte();
        }

        public void LoadElements(int cstr, int cbtn, System.IO.FileStream fs, System.Xml.XmlReader ids)
        {
            TInterfaceElement ie = new TInterfaceElement();
            while (ids.Read())
                if (ids.IsStartElement())
                {
                    switch (ids.Name)
                    {
                        case "Element":
                            ie = new TInterfaceElement();
                            break;
                        case "ItemType":
                            {
                                ids.Read();
                                switch (ids.Value)
                                {
                                    case "Border":
                                        {
                                            Button btn = new Button();

                                            btn.Parent = pnl;
                                            btn.MouseDown += new MouseEventHandler(MouseDown);
                                            btn.MouseMove += new MouseEventHandler(MouseMove);

                                            btn.ContextMenuStrip = pmElement;
                                            btn.Text = "Border";
                                            btn.Refresh();
                                            ie.border = btn;
                                        } break; // button
                                    case "2": break; // edit
                                    case "Label":
                                        {
                                            Label lbl = new Label();

                                            lbl.Parent = pnl;
                                            lbl.MouseDown += new MouseEventHandler(MouseDown);
                                            lbl.MouseMove += new MouseEventHandler(MouseMove);

                                            lbl.Text = "Label";
                                            lbl.ContextMenuStrip = pmElement;
                                            lbl.Refresh();
                                            ie.border = lbl;
                                        } break; // label
                                    case "4": break; // Checker
                                }
                            }
                            break;
                        case "ID": ids.Read(); ie.ID = Convert.ToByte(ids.Value); break;
                        case "ItemName": ids.Read(); ie.ItemName = ids.Value; listitem.Add(ie.ItemName, ie); break;
                    }// ids.Name
                }//while
        }

        public void LoadProperties(System.IO.FileStream fs)
        {
            UInt16 eSize = utftUtils.Read2Bytes(fs);
            byte[] buff = new byte[eSize];
            fs.Read(buff, 0, eSize);
            byte itemtype = buff[0];
            byte itemid = buff[1];
            foreach (KeyValuePair<string, TInterfaceElement> ie in listitem)
            if (ie.Value.GetItemTypeNumber()==itemtype && ie.Value.ID==itemid) 
            {
                TInterfaceElement ii = ie.Value;
                ii.X = (buff[2] << 8) | buff[3];
                ii.Y = (buff[4] << 8) | buff[5];
                ii.width = buff[6];
                ii.heigth = buff[7];
                ii.BackColor = utftUtils.GetUTFTColor((UInt16)((buff[8] << 8) | buff[9]));
                ii.FontColor = utftUtils.GetUTFTColor((UInt16)((buff[10] << 8) | buff[11]));
                byte strl = buff[12];
                string ss="";
                for (int i = 0; i < strl; i++)
                    ss = ss + Convert.ToChar(buff[13 + i]);
                ii.ItemName = ss;
                ii.Text = ss;
                AddItem(ii);
            }
        }
    }
}
