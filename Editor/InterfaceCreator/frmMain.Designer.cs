namespace InterfaceCreator
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadScreen = new System.Windows.Forms.Button();
            this.btnSaveScreen = new System.Windows.Forms.Button();
            this.btnCreateLabel = new System.Windows.Forms.Button();
            this.btnCreateElement = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.cbElements = new System.Windows.Forms.ComboBox();
            this.pmElement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCreatePanel = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.FaceBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pmElement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FaceBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSwitch);
            this.panel1.Controls.Add(this.btnCreatePanel);
            this.panel1.Controls.Add(this.btnLoadScreen);
            this.panel1.Controls.Add(this.btnSaveScreen);
            this.panel1.Controls.Add(this.btnCreateLabel);
            this.panel1.Controls.Add(this.btnCreateElement);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(774, 94);
            this.panel1.TabIndex = 0;
            // 
            // btnLoadScreen
            // 
            this.btnLoadScreen.Location = new System.Drawing.Point(431, 12);
            this.btnLoadScreen.Name = "btnLoadScreen";
            this.btnLoadScreen.Size = new System.Drawing.Size(75, 69);
            this.btnLoadScreen.TabIndex = 3;
            this.btnLoadScreen.Tag = "";
            this.btnLoadScreen.Text = "Load Screen";
            this.btnLoadScreen.UseVisualStyleBackColor = true;
            this.btnLoadScreen.Click += new System.EventHandler(this.btnLoadScreen_Click);
            // 
            // btnSaveScreen
            // 
            this.btnSaveScreen.Location = new System.Drawing.Point(350, 13);
            this.btnSaveScreen.Name = "btnSaveScreen";
            this.btnSaveScreen.Size = new System.Drawing.Size(75, 69);
            this.btnSaveScreen.TabIndex = 2;
            this.btnSaveScreen.Tag = "";
            this.btnSaveScreen.Text = "Save Screen";
            this.btnSaveScreen.UseVisualStyleBackColor = true;
            this.btnSaveScreen.Click += new System.EventHandler(this.btnSaveScreen_Click);
            // 
            // btnCreateLabel
            // 
            this.btnCreateLabel.Location = new System.Drawing.Point(250, 12);
            this.btnCreateLabel.Name = "btnCreateLabel";
            this.btnCreateLabel.Size = new System.Drawing.Size(75, 69);
            this.btnCreateLabel.TabIndex = 1;
            this.btnCreateLabel.Tag = "";
            this.btnCreateLabel.Text = "Create\r\nLabel";
            this.btnCreateLabel.UseVisualStyleBackColor = true;
            this.btnCreateLabel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCreateElement
            // 
            this.btnCreateElement.Location = new System.Drawing.Point(7, 12);
            this.btnCreateElement.Name = "btnCreateElement";
            this.btnCreateElement.Size = new System.Drawing.Size(75, 69);
            this.btnCreateElement.TabIndex = 0;
            this.btnCreateElement.Tag = "";
            this.btnCreateElement.Text = "Create Button";
            this.btnCreateElement.UseVisualStyleBackColor = true;
            this.btnCreateElement.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(774, 312);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.FaceBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(275, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(499, 312);
            this.panel4.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(272, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 312);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(272, 312);
            this.panel3.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(272, 312);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyGrid2);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(264, 286);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Screen";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid2.Location = new System.Drawing.Point(3, 24);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(258, 259);
            this.propertyGrid2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(258, 21);
            this.panel5.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propertyGrid1);
            this.tabPage2.Controls.Add(this.cbElements);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(264, 286);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Elements";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 24);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(258, 259);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // cbElements
            // 
            this.cbElements.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbElements.FormattingEnabled = true;
            this.cbElements.Location = new System.Drawing.Point(3, 3);
            this.cbElements.Name = "cbElements";
            this.cbElements.Size = new System.Drawing.Size(258, 21);
            this.cbElements.TabIndex = 0;
            this.cbElements.SelectedIndexChanged += new System.EventHandler(this.cbElements_SelectedIndexChanged);
            // 
            // pmElement
            // 
            this.pmElement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.pmElement.Name = "pmElement";
            this.pmElement.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "scr";
            this.saveFileDialog1.Filter = "Screens|*.scr";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "scr";
            this.openFileDialog1.Filter = "Screens|*.scr";
            // 
            // btnCreatePanel
            // 
            this.btnCreatePanel.Location = new System.Drawing.Point(88, 12);
            this.btnCreatePanel.Name = "btnCreatePanel";
            this.btnCreatePanel.Size = new System.Drawing.Size(75, 69);
            this.btnCreatePanel.TabIndex = 4;
            this.btnCreatePanel.Tag = "";
            this.btnCreatePanel.Text = "Create Panel";
            this.btnCreatePanel.UseVisualStyleBackColor = true;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(169, 12);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(75, 69);
            this.btnSwitch.TabIndex = 5;
            this.btnSwitch.Tag = "";
            this.btnSwitch.Text = "Create Switch";
            this.btnSwitch.UseVisualStyleBackColor = true;
            // 
            // FaceBox
            // 
            this.FaceBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FaceBox.Location = new System.Drawing.Point(6, 6);
            this.FaceBox.Name = "FaceBox";
            this.FaceBox.Size = new System.Drawing.Size(187, 105);
            this.FaceBox.TabIndex = 1;
            this.FaceBox.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 406);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.pmElement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FaceBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCreateElement;
        private System.Windows.Forms.ComboBox cbElements;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnCreateLabel;
        private System.Windows.Forms.Button btnSaveScreen;
        private System.Windows.Forms.ContextMenuStrip pmElement;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnLoadScreen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnCreatePanel;
        private System.Windows.Forms.PictureBox FaceBox;
    }
}

