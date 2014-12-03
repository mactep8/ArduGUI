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

        public void Save(System.IO.FileStream fs, System.Xml.XmlWriter fi, System.IO.StreamWriter fd)
        {
            fi.WriteStartDocument();
            fi.WriteStartElement("Document");
            fi.WriteStartElement("Screen");
            fi.WriteElementString("SCREEN_Width", Width.ToString());
            fi.WriteElementString("SCREEN_Height", Height.ToString());
            fi.WriteElementString("SCREEN_BackColor", BackColor.ToString());
            fi.WriteElementString("SCREEN_FontColor", FontColor.ToString());
            fi.WriteEndElement();

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
            fi.WriteStartElement("Elements");

            foreach (KeyValuePair<string, TInterfaceElement> e in listitem)
            {
                e.Value.Save(fs, fi);
                if (e.Value.ItemType!="Label")
                    fd.WriteLine(String.Format("#define {0} {1}", e.Value.ItemName, e.Value.ID));
            }

            fi.WriteEndElement();

            fi.WriteEndElement();
            fi.WriteEndDocument();
        }

        public void LoadElements(System.Xml.XmlReader ids)
        {
            TInterfaceElement ie = new TInterfaceElement();
            while (ids.Read())
                if (ids.IsStartElement())
                {
                    switch (ids.Name)
                    {
                        case "SCREEN_Width":
                            {
                                ids.Read();
                                Width = Convert.ToInt16(ids.Value);
                            }; break;
                        case "SCREEN_Height":
                            {
                                ids.Read();
                                Height = Convert.ToInt16(ids.Value);
                            }; break;
                        case "SCREEN_BackColor":
                            {
                                ids.Read();
                                BackColor = utftUtils.GetUTFTColor(ids.Value);
                            }; break;
                        case "SCREEN_FontColor":
                            {
                                ids.Read();
                                FontColor = utftUtils.GetUTFTColor(ids.Value);
                            }; break;
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
                        case "ItemName": ids.Read(); ie.ItemName = ids.Value; listitem.Add(ie.ItemName, ie); AddItem(ie); break;
                        case "X": ids.Read(); ie.X = Convert.ToInt16(ids.Value); break;
                        case "Y": ids.Read(); ie.Y = Convert.ToInt16(ids.Value); break;
                        case "Width": ids.Read(); ie.width = Convert.ToInt16(ids.Value); break;
                        case "Height": ids.Read(); ie.heigth = Convert.ToInt16(ids.Value); break;
                        case "BackColor": ids.Read(); ie.BackColor = utftUtils.GetUTFTColor(ids.Value); break;
                        case "FontColor": ids.Read(); ie.FontColor = utftUtils.GetUTFTColor(ids.Value); break;
                        case "CanSelect": ids.Read(); ie.CanSelect = Convert.ToBoolean(ids.Value); break;
                        case "Text": ids.Read(); ie.Text = ids.Value; break;
                    }// ids.Name
                }//while
        }

    }
}
