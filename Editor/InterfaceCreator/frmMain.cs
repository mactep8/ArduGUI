using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InterfaceCreator
{
    public partial class frmMain : Form
    {
        public TScreen Screen = new TScreen();

        private void OnAddItem(TInterfaceElement item)
        {
            cbElements.Items.Add(item);
            cbElements.SelectedItem = item;
        }

        public frmMain()
        {
            InitializeComponent();
            Screen.pnl = FaceBox;
            Screen.BackColor = VGA_COLOR.VGA_BLACK;
            Screen.FontColor = VGA_COLOR.VGA_WHITE;
            Screen.Width = 400;
            Screen.Height = 240;
            propertyGrid2.SelectedObject = Screen;
            Screen.AddItem = OnAddItem;
            Screen.MouseDown = button1_MouseDown;
            Screen.MouseMove = button1_MouseMove;
            Screen.pmElement = pmElement;
        }
                
        int x_pos, y_pos;
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            x_pos = e.X;
            y_pos = e.Y;
            cbElements.SelectedItem = Screen.listitem[((Control)sender).Tag.ToString()];
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ((Control)sender).Left += (e.X - x_pos);

                ((Control)sender).Top += (e.Y - y_pos);

                ((Control)sender).Refresh();
                propertyGrid1.Refresh();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Screen.CreateElement();
        }

        private void cbElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = cbElements.SelectedItem;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "ItemName")
            {
                TInterfaceElement ie = Screen.listitem[e.OldValue.ToString()];
                Screen.listitem.Remove(e.OldValue.ToString());
                Screen.listitem.Add(ie.ItemName, ie);

                int indx = cbElements.SelectedIndex;
                cbElements.Items.RemoveAt(indx);
                cbElements.Items.Insert(indx, ie);
                cbElements.SelectedIndex = indx;
                cbElements.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Screen.CreateLabel();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TInterfaceElement item = (TInterfaceElement)cbElements.SelectedItem;
            Screen.listitem.Remove(item.ItemName);
            cbElements.Items.Remove(item);
            item.Dispose();
            if (cbElements.Items.Count > 0) cbElements.SelectedIndex = 0;
            else propertyGrid1.SelectedObject = null;
        }

        private void btnSaveScreen_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string MainFileName = saveFileDialog1.FileName;// +".scr";
                if (MainFileName.Substring(MainFileName.Length - 4) != ".scr") MainFileName = MainFileName + ".scr";
                string IDsFileName = MainFileName.Substring(0,MainFileName.Length-4) + ".ids";
                string DefsFileName = IDsFileName.Substring(0, IDsFileName.Length - 4) + ".h";

                System.IO.FileStream fm = new System.IO.FileStream(MainFileName, System.IO.FileMode.Create);
                

                System.Xml.XmlWriter fi = System.Xml.XmlWriter.Create(IDsFileName);
                System.IO.FileStream fd = new System.IO.FileStream(DefsFileName, System.IO.FileMode.Create);

                fi.WriteStartDocument();
                fi.WriteStartElement("Elements");

                Screen.Save(fm, fi, fd);
                /*foreach (KeyValuePair<string, TInterfaceElement> itm in Screen.listitem)
                    itm.Value.Save(fm, fi, fd);*/
                fm.Close();
                fi.WriteEndElement();
                fi.WriteEndDocument();
                fi.Close();
                fd.Close();

                fm.Dispose();
                fd.Dispose();
            }
        }

        private void btnLoadScreen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string MainFileName = openFileDialog1.FileName;// +".scr";
                string IDsFileName = MainFileName.Substring(0, MainFileName.Length - 4) + ".ids";
                string DefsFileName = IDsFileName.Substring(0, IDsFileName.Length - 4) + ".h";

                // сначала открываем основной файл и читаем настройки экрана
                System.IO.FileStream fm = new System.IO.FileStream(MainFileName, System.IO.FileMode.Open);

                byte cstr = 0, cbtn = 0;
                Screen.LoadScreen(fm, ref cstr, ref cbtn);
                // далее читаем xml и создаем структуру элементов
                System.Xml.XmlReader ids = System.Xml.XmlReader.Create(IDsFileName);
                Screen.LoadElements(cstr, cbtn, fm, ids);
                // потом из основного файла читаем сами элементы
                int i=0;
                while (cstr + cbtn > i)
                {
                    Screen.LoadProperties(fm);
                    i++;
                }
                // закрываем все
                fm.Close();
                fm.Dispose();
                ids.Close();
            }
        }

    }
}
