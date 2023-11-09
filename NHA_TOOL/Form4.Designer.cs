namespace NHA_TOOL
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxState = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvVillage = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvDistrict = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvBlock = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvFlag = new System.Windows.Forms.DataGridView();
            this.btnStartProcessing = new System.Windows.Forms.Button();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbgender = new System.Windows.Forms.CheckBox();
            this.cbblock = new System.Windows.Forms.CheckBox();
            this.cbpmj = new System.Windows.Forms.CheckBox();
            this.cbabhaid = new System.Windows.Forms.CheckBox();
            this.cbpic = new System.Windows.Forms.CheckBox();
            this.cbvillage = new System.Windows.Forms.CheckBox();
            this.cbstate = new System.Windows.Forms.CheckBox();
            this.cbdistrict = new System.Windows.Forms.CheckBox();
            this.cbname = new System.Windows.Forms.CheckBox();
            this.cbdob = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbslash = new System.Windows.Forms.RadioButton();
            this.cbpipe = new System.Windows.Forms.RadioButton();
            this.cbtilt = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbtxt = new System.Windows.Forms.RadioButton();
            this.cbcsv = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVillage)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistrict)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlock)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlag)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pbClose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 39);
            this.panel1.TabIndex = 55;
            // 
            // pbClose
            // 
            this.pbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbClose.Image = global::NHA_TOOL.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(831, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(43, 39);
            this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbClose.TabIndex = 1;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(360, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "NHA Dashboard";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbxState);
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Controls.Add(this.btnStartProcessing);
            this.groupBox3.Controls.Add(this.btnExportReport);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbgender);
            this.groupBox3.Controls.Add(this.cbblock);
            this.groupBox3.Controls.Add(this.cbpmj);
            this.groupBox3.Controls.Add(this.cbabhaid);
            this.groupBox3.Controls.Add(this.cbpic);
            this.groupBox3.Controls.Add(this.cbvillage);
            this.groupBox3.Controls.Add(this.cbstate);
            this.groupBox3.Controls.Add(this.cbdistrict);
            this.groupBox3.Controls.Add(this.cbname);
            this.groupBox3.Controls.Add(this.cbdob);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtFilePath);
            this.groupBox3.Controls.Add(this.btnBrowseFile);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Location = new System.Drawing.Point(31, 62);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(807, 531);
            this.groupBox3.TabIndex = 122;
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 17);
            this.label7.TabIndex = 150;
            this.label7.Text = "Summary:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 149;
            this.label6.Text = "State";
            // 
            // cbxState
            // 
            this.cbxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxState.FormattingEnabled = true;
            this.cbxState.Location = new System.Drawing.Point(167, 22);
            this.cbxState.Name = "cbxState";
            this.cbxState.Size = new System.Drawing.Size(200, 24);
            this.cbxState.TabIndex = 148;
            this.cbxState.SelectedIndexChanged += new System.EventHandler(this.cbxState_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(14, 278);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 200);
            this.tabControl1.TabIndex = 147;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvVillage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 174);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Village Wise Summary";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvVillage
            // 
            this.dgvVillage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVillage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVillage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVillage.Location = new System.Drawing.Point(3, 3);
            this.dgvVillage.Name = "dgvVillage";
            this.dgvVillage.ReadOnly = true;
            this.dgvVillage.RowHeadersVisible = false;
            this.dgvVillage.Size = new System.Drawing.Size(754, 168);
            this.dgvVillage.TabIndex = 143;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvDistrict);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(760, 174);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "District Wise Summary";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvDistrict
            // 
            this.dgvDistrict.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDistrict.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistrict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDistrict.Location = new System.Drawing.Point(3, 3);
            this.dgvDistrict.Name = "dgvDistrict";
            this.dgvDistrict.ReadOnly = true;
            this.dgvDistrict.RowHeadersVisible = false;
            this.dgvDistrict.Size = new System.Drawing.Size(754, 168);
            this.dgvDistrict.TabIndex = 141;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvBlock);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(760, 174);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Block Wise Summary";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvBlock
            // 
            this.dgvBlock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBlock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBlock.Location = new System.Drawing.Point(3, 3);
            this.dgvBlock.Name = "dgvBlock";
            this.dgvBlock.ReadOnly = true;
            this.dgvBlock.RowHeadersVisible = false;
            this.dgvBlock.Size = new System.Drawing.Size(754, 168);
            this.dgvBlock.TabIndex = 142;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvFlag);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 174);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Flag Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvFlag
            // 
            this.dgvFlag.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFlag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFlag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFlag.Location = new System.Drawing.Point(3, 3);
            this.dgvFlag.Name = "dgvFlag";
            this.dgvFlag.ReadOnly = true;
            this.dgvFlag.RowHeadersVisible = false;
            this.dgvFlag.Size = new System.Drawing.Size(754, 168);
            this.dgvFlag.TabIndex = 144;
            // 
            // btnStartProcessing
            // 
            this.btnStartProcessing.AccessibleName = "Batch_insert_spliter";
            this.btnStartProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartProcessing.Location = new System.Drawing.Point(606, 484);
            this.btnStartProcessing.Name = "btnStartProcessing";
            this.btnStartProcessing.Size = new System.Drawing.Size(176, 34);
            this.btnStartProcessing.TabIndex = 146;
            this.btnStartProcessing.Text = "Go to Data Processing >";
            this.btnStartProcessing.UseVisualStyleBackColor = true;
            this.btnStartProcessing.Click += new System.EventHandler(this.btnStartProcessing_Click);
            // 
            // btnExportReport
            // 
            this.btnExportReport.AccessibleName = "Batch_insert_spliter";
            this.btnExportReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportReport.Location = new System.Drawing.Point(663, 245);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(112, 27);
            this.btnExportReport.TabIndex = 145;
            this.btnExportReport.Text = "Export Report";
            this.btnExportReport.UseVisualStyleBackColor = true;
            this.btnExportReport.Visible = false;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 17);
            this.label5.TabIndex = 140;
            this.label5.Text = "Import File :";
            // 
            // cbgender
            // 
            this.cbgender.AutoSize = true;
            this.cbgender.Location = new System.Drawing.Point(727, 170);
            this.cbgender.Name = "cbgender";
            this.cbgender.Size = new System.Drawing.Size(61, 17);
            this.cbgender.TabIndex = 139;
            this.cbgender.Text = "Gender";
            this.cbgender.UseVisualStyleBackColor = true;
            // 
            // cbblock
            // 
            this.cbblock.AutoSize = true;
            this.cbblock.Location = new System.Drawing.Point(547, 170);
            this.cbblock.Name = "cbblock";
            this.cbblock.Size = new System.Drawing.Size(53, 17);
            this.cbblock.TabIndex = 138;
            this.cbblock.Text = "Block";
            this.cbblock.UseVisualStyleBackColor = true;
            // 
            // cbpmj
            // 
            this.cbpmj.AutoSize = true;
            this.cbpmj.Location = new System.Drawing.Point(282, 170);
            this.cbpmj.Name = "cbpmj";
            this.cbpmj.Size = new System.Drawing.Size(78, 17);
            this.cbpmj.TabIndex = 137;
            this.cbpmj.Text = "PMJAY_ID";
            this.cbpmj.UseVisualStyleBackColor = true;
            // 
            // cbabhaid
            // 
            this.cbabhaid.AutoSize = true;
            this.cbabhaid.Location = new System.Drawing.Point(362, 170);
            this.cbabhaid.Name = "cbabhaid";
            this.cbabhaid.Size = new System.Drawing.Size(72, 17);
            this.cbabhaid.TabIndex = 136;
            this.cbabhaid.Text = "ABHA_ID";
            this.cbabhaid.UseVisualStyleBackColor = true;
            // 
            // cbpic
            // 
            this.cbpic.AutoSize = true;
            this.cbpic.Location = new System.Drawing.Point(435, 170);
            this.cbpic.Name = "cbpic";
            this.cbpic.Size = new System.Drawing.Size(43, 17);
            this.cbpic.TabIndex = 135;
            this.cbpic.Text = "PIC";
            this.cbpic.UseVisualStyleBackColor = true;
            // 
            // cbvillage
            // 
            this.cbvillage.AutoSize = true;
            this.cbvillage.Location = new System.Drawing.Point(484, 170);
            this.cbvillage.Name = "cbvillage";
            this.cbvillage.Size = new System.Drawing.Size(57, 17);
            this.cbvillage.TabIndex = 134;
            this.cbvillage.Text = "Village";
            this.cbvillage.UseVisualStyleBackColor = true;
            // 
            // cbstate
            // 
            this.cbstate.AutoSize = true;
            this.cbstate.Location = new System.Drawing.Point(670, 170);
            this.cbstate.Name = "cbstate";
            this.cbstate.Size = new System.Drawing.Size(51, 17);
            this.cbstate.TabIndex = 133;
            this.cbstate.Text = "State";
            this.cbstate.UseVisualStyleBackColor = true;
            // 
            // cbdistrict
            // 
            this.cbdistrict.AutoSize = true;
            this.cbdistrict.Location = new System.Drawing.Point(606, 170);
            this.cbdistrict.Name = "cbdistrict";
            this.cbdistrict.Size = new System.Drawing.Size(58, 17);
            this.cbdistrict.TabIndex = 132;
            this.cbdistrict.Text = "District";
            this.cbdistrict.UseVisualStyleBackColor = true;
            // 
            // cbname
            // 
            this.cbname.AutoSize = true;
            this.cbname.Location = new System.Drawing.Point(167, 170);
            this.cbname.Name = "cbname";
            this.cbname.Size = new System.Drawing.Size(54, 17);
            this.cbname.TabIndex = 131;
            this.cbname.Text = "Name";
            this.cbname.UseVisualStyleBackColor = true;
            // 
            // cbdob
            // 
            this.cbdob.AutoSize = true;
            this.cbdob.Location = new System.Drawing.Point(227, 170);
            this.cbdob.Name = "cbdob";
            this.cbdob.Size = new System.Drawing.Size(49, 17);
            this.cbdob.TabIndex = 130;
            this.cbdob.Text = "DOB";
            this.cbdob.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 129;
            this.label2.Text = "Validation";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbslash);
            this.groupBox2.Controls.Add(this.cbpipe);
            this.groupBox2.Controls.Add(this.cbtilt);
            this.groupBox2.Location = new System.Drawing.Point(167, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 44);
            this.groupBox2.TabIndex = 128;
            this.groupBox2.TabStop = false;
            // 
            // cbslash
            // 
            this.cbslash.AutoSize = true;
            this.cbslash.Location = new System.Drawing.Point(152, 16);
            this.cbslash.Name = "cbslash";
            this.cbslash.Size = new System.Drawing.Size(30, 17);
            this.cbslash.TabIndex = 37;
            this.cbslash.Text = "/";
            this.cbslash.UseVisualStyleBackColor = true;
            // 
            // cbpipe
            // 
            this.cbpipe.AutoSize = true;
            this.cbpipe.Checked = true;
            this.cbpipe.Location = new System.Drawing.Point(19, 18);
            this.cbpipe.Name = "cbpipe";
            this.cbpipe.Size = new System.Drawing.Size(27, 17);
            this.cbpipe.TabIndex = 35;
            this.cbpipe.TabStop = true;
            this.cbpipe.Text = "|";
            this.cbpipe.UseVisualStyleBackColor = true;
            // 
            // cbtilt
            // 
            this.cbtilt.AutoSize = true;
            this.cbtilt.Location = new System.Drawing.Point(79, 18);
            this.cbtilt.Name = "cbtilt";
            this.cbtilt.Size = new System.Drawing.Size(32, 17);
            this.cbtilt.TabIndex = 36;
            this.cbtilt.Text = "~";
            this.cbtilt.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbtxt);
            this.groupBox1.Controls.Add(this.cbcsv);
            this.groupBox1.Location = new System.Drawing.Point(167, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 44);
            this.groupBox1.TabIndex = 127;
            this.groupBox1.TabStop = false;
            // 
            // cbtxt
            // 
            this.cbtxt.AutoSize = true;
            this.cbtxt.Location = new System.Drawing.Point(17, 17);
            this.cbtxt.Name = "cbtxt";
            this.cbtxt.Size = new System.Drawing.Size(46, 17);
            this.cbtxt.TabIndex = 38;
            this.cbtxt.Text = "TXT";
            this.cbtxt.UseVisualStyleBackColor = true;
            // 
            // cbcsv
            // 
            this.cbcsv.AutoSize = true;
            this.cbcsv.Checked = true;
            this.cbcsv.Location = new System.Drawing.Point(134, 17);
            this.cbcsv.Name = "cbcsv";
            this.cbcsv.Size = new System.Drawing.Size(46, 17);
            this.cbcsv.TabIndex = 37;
            this.cbcsv.TabStop = true;
            this.cbcsv.Text = "CSV";
            this.cbcsv.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 126;
            this.label3.Text = "Input File Extension";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 125;
            this.label4.Text = "Input File Seperator";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(14, 222);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(275, 23);
            this.txtFilePath.TabIndex = 124;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.AccessibleName = "Batch_insert_spliter";
            this.btnBrowseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseFile.Location = new System.Drawing.Point(294, 220);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(99, 27);
            this.btnBrowseFile.TabIndex = 123;
            this.btnBrowseFile.Text = "Browse File";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(435, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(347, 136);
            this.richTextBox1.TabIndex = 122;
            this.richTextBox1.Text = "";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 605);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVillage)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistrict)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBlock)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlag)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbgender;
        private System.Windows.Forms.CheckBox cbblock;
        private System.Windows.Forms.CheckBox cbpmj;
        private System.Windows.Forms.CheckBox cbabhaid;
        private System.Windows.Forms.CheckBox cbpic;
        private System.Windows.Forms.CheckBox cbvillage;
        private System.Windows.Forms.CheckBox cbstate;
        private System.Windows.Forms.CheckBox cbdistrict;
        private System.Windows.Forms.CheckBox cbname;
        private System.Windows.Forms.CheckBox cbdob;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton cbslash;
        private System.Windows.Forms.RadioButton cbpipe;
        private System.Windows.Forms.RadioButton cbtilt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton cbtxt;
        private System.Windows.Forms.RadioButton cbcsv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvFlag;
        private System.Windows.Forms.DataGridView dgvVillage;
        private System.Windows.Forms.DataGridView dgvBlock;
        private System.Windows.Forms.DataGridView dgvDistrict;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Button btnStartProcessing;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxState;
        private System.Windows.Forms.Label label7;
    }
}