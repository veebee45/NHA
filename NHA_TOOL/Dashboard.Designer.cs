namespace NHA_TOOL
{
    partial class Dashboard
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrinting = new System.Windows.Forms.Button();
            this.District = new System.Windows.Forms.Label();
            this.cbxDistrict = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxUnprocessedState = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgvSeparator = new System.Windows.Forms.DataGridView();
            this.gbExistingState = new System.Windows.Forms.GroupBox();
            this.cbxState = new System.Windows.Forms.ComboBox();
            this.pbAddState = new System.Windows.Forms.PictureBox();
            this.gbNewState = new System.Windows.Forms.GroupBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.pbReturn = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnProcessing = new System.Windows.Forms.Button();
            this.lblSortingInput = new System.Windows.Forms.Label();
            this.checkedListBoxItems = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbpng = new System.Windows.Forms.RadioButton();
            this.cbjpg = new System.Windows.Forms.RadioButton();
            this.cbjpeg = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOuterboxQty = new System.Windows.Forms.TextBox();
            this.txtInnerboxQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeparator)).BeginInit();
            this.gbExistingState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddState)).BeginInit();
            this.gbNewState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbReturn)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(941, 39);
            this.panel1.TabIndex = 97;
            // 
            // pbClose
            // 
            this.pbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbClose.Image = global::NHA_TOOL.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(898, 0);
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
            this.label1.Size = new System.Drawing.Size(279, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "NHA Data Processing";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnPrinting);
            this.groupBox1.Controls.Add(this.District);
            this.groupBox1.Controls.Add(this.cbxDistrict);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxUnprocessedState);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.gbExistingState);
            this.groupBox1.Controls.Add(this.gbNewState);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnProcessing);
            this.groupBox1.Controls.Add(this.lblSortingInput);
            this.groupBox1.Controls.Add(this.checkedListBoxItems);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtOuterboxQty);
            this.groupBox1.Controls.Add(this.txtInnerboxQty);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(41, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 508);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Processing";
            // 
            // btnPrinting
            // 
            this.btnPrinting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrinting.Location = new System.Drawing.Point(637, 450);
            this.btnPrinting.Name = "btnPrinting";
            this.btnPrinting.Size = new System.Drawing.Size(175, 40);
            this.btnPrinting.TabIndex = 157;
            this.btnPrinting.Text = "Generate 24 ups Sheets";
            this.btnPrinting.UseVisualStyleBackColor = true;
            this.btnPrinting.Visible = false;
            this.btnPrinting.Click += new System.EventHandler(this.btnPrinting_Click);
            // 
            // District
            // 
            this.District.AutoSize = true;
            this.District.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.District.Location = new System.Drawing.Point(443, 25);
            this.District.Name = "District";
            this.District.Size = new System.Drawing.Size(64, 17);
            this.District.TabIndex = 156;
            this.District.Text = "District:";
            // 
            // cbxDistrict
            // 
            this.cbxDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDistrict.FormattingEnabled = true;
            this.cbxDistrict.Location = new System.Drawing.Point(518, 22);
            this.cbxDistrict.Name = "cbxDistrict";
            this.cbxDistrict.Size = new System.Drawing.Size(214, 24);
            this.cbxDistrict.TabIndex = 155;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 154;
            this.label3.Text = "State:";
            // 
            // cbxUnprocessedState
            // 
            this.cbxUnprocessedState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUnprocessedState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUnprocessedState.FormattingEnabled = true;
            this.cbxUnprocessedState.Location = new System.Drawing.Point(177, 30);
            this.cbxUnprocessedState.Name = "cbxUnprocessedState";
            this.cbxUnprocessedState.Size = new System.Drawing.Size(214, 24);
            this.cbxUnprocessedState.TabIndex = 153;
            this.cbxUnprocessedState.SelectedIndexChanged += new System.EventHandler(this.cbxUnprocessedState_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(412, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 150;
            this.label2.Text = "Final Summary:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(740, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(100, 67);
            this.richTextBox1.TabIndex = 149;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(415, 163);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(401, 262);
            this.tabControl1.TabIndex = 148;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgvSeparator);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(393, 236);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Separator Wise Summary";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvSeparator
            // 
            this.dgvSeparator.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSeparator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSeparator.Location = new System.Drawing.Point(3, 3);
            this.dgvSeparator.Name = "dgvSeparator";
            this.dgvSeparator.ReadOnly = true;
            this.dgvSeparator.RowHeadersVisible = false;
            this.dgvSeparator.Size = new System.Drawing.Size(387, 230);
            this.dgvSeparator.TabIndex = 145;
            // 
            // gbExistingState
            // 
            this.gbExistingState.Controls.Add(this.cbxState);
            this.gbExistingState.Controls.Add(this.pbAddState);
            this.gbExistingState.Location = new System.Drawing.Point(177, 60);
            this.gbExistingState.Name = "gbExistingState";
            this.gbExistingState.Size = new System.Drawing.Size(266, 41);
            this.gbExistingState.TabIndex = 122;
            this.gbExistingState.TabStop = false;
            // 
            // cbxState
            // 
            this.cbxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxState.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxState.FormattingEnabled = true;
            this.cbxState.Location = new System.Drawing.Point(6, 9);
            this.cbxState.Name = "cbxState";
            this.cbxState.Size = new System.Drawing.Size(208, 26);
            this.cbxState.TabIndex = 116;
            this.cbxState.SelectedIndexChanged += new System.EventHandler(this.cbxState_SelectedIndexChanged);
            // 
            // pbAddState
            // 
            this.pbAddState.Image = global::NHA_TOOL.Properties.Resources.post;
            this.pbAddState.Location = new System.Drawing.Point(229, 9);
            this.pbAddState.Name = "pbAddState";
            this.pbAddState.Size = new System.Drawing.Size(30, 26);
            this.pbAddState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAddState.TabIndex = 117;
            this.pbAddState.TabStop = false;
            this.pbAddState.Click += new System.EventHandler(this.pbAddState_Click);
            // 
            // gbNewState
            // 
            this.gbNewState.Controls.Add(this.txtState);
            this.gbNewState.Controls.Add(this.pbReturn);
            this.gbNewState.Location = new System.Drawing.Point(464, 60);
            this.gbNewState.Name = "gbNewState";
            this.gbNewState.Size = new System.Drawing.Size(263, 44);
            this.gbNewState.TabIndex = 121;
            this.gbNewState.TabStop = false;
            this.gbNewState.Text = "New State";
            this.gbNewState.Visible = false;
            // 
            // txtState
            // 
            this.txtState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(6, 14);
            this.txtState.MaxLength = 8;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(208, 23);
            this.txtState.TabIndex = 118;
            // 
            // pbReturn
            // 
            this.pbReturn.Image = global::NHA_TOOL.Properties.Resources._return;
            this.pbReturn.Location = new System.Drawing.Point(220, 12);
            this.pbReturn.Name = "pbReturn";
            this.pbReturn.Size = new System.Drawing.Size(30, 26);
            this.pbReturn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbReturn.TabIndex = 120;
            this.pbReturn.TabStop = false;
            this.pbReturn.Click += new System.EventHandler(this.pbReturn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 17);
            this.label8.TabIndex = 115;
            this.label8.Text = "Config State:";
            // 
            // btnProcessing
            // 
            this.btnProcessing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessing.Location = new System.Drawing.Point(419, 450);
            this.btnProcessing.Name = "btnProcessing";
            this.btnProcessing.Size = new System.Drawing.Size(175, 40);
            this.btnProcessing.TabIndex = 113;
            this.btnProcessing.Text = "Start Processing";
            this.btnProcessing.UseVisualStyleBackColor = true;
            this.btnProcessing.Click += new System.EventHandler(this.btnProcessing_Click);
            // 
            // lblSortingInput
            // 
            this.lblSortingInput.AutoSize = true;
            this.lblSortingInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSortingInput.Location = new System.Drawing.Point(33, 433);
            this.lblSortingInput.Name = "lblSortingInput";
            this.lblSortingInput.Size = new System.Drawing.Size(36, 17);
            this.lblSortingInput.TabIndex = 112;
            this.lblSortingInput.Text = "Null";
            // 
            // checkedListBoxItems
            // 
            this.checkedListBoxItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxItems.FormattingEnabled = true;
            this.checkedListBoxItems.Items.AddRange(new object[] {
            "name_ben",
            "pmrssm_id",
            "village_name_english",
            "block_name_english",
            "district_name_english",
            "userid",
            "ahl_hhid",
            "health_id"});
            this.checkedListBoxItems.Location = new System.Drawing.Point(177, 259);
            this.checkedListBoxItems.Name = "checkedListBoxItems";
            this.checkedListBoxItems.Size = new System.Drawing.Size(208, 166);
            this.checkedListBoxItems.TabIndex = 111;
            this.checkedListBoxItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxItems_ItemCheck);
            this.checkedListBoxItems.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxItems_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 17);
            this.label9.TabIndex = 105;
            this.label9.Text = "Sorting:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbpng);
            this.groupBox3.Controls.Add(this.cbjpg);
            this.groupBox3.Controls.Add(this.cbjpeg);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(177, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 42);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            // 
            // cbpng
            // 
            this.cbpng.AutoSize = true;
            this.cbpng.Location = new System.Drawing.Point(150, 14);
            this.cbpng.Name = "cbpng";
            this.cbpng.Size = new System.Drawing.Size(50, 21);
            this.cbpng.TabIndex = 39;
            this.cbpng.Text = "png";
            this.cbpng.UseVisualStyleBackColor = true;
            // 
            // cbjpg
            // 
            this.cbjpg.AutoSize = true;
            this.cbjpg.Location = new System.Drawing.Point(17, 15);
            this.cbjpg.Name = "cbjpg";
            this.cbjpg.Size = new System.Drawing.Size(45, 21);
            this.cbjpg.TabIndex = 38;
            this.cbjpg.Text = "jpg";
            this.cbjpg.UseVisualStyleBackColor = true;
            // 
            // cbjpeg
            // 
            this.cbjpeg.AutoSize = true;
            this.cbjpeg.Checked = true;
            this.cbjpeg.Location = new System.Drawing.Point(83, 15);
            this.cbjpeg.Name = "cbjpeg";
            this.cbjpeg.Size = new System.Drawing.Size(53, 21);
            this.cbjpeg.TabIndex = 37;
            this.cbjpeg.TabStop = true;
            this.cbjpeg.Text = "jpeg";
            this.cbjpeg.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 17);
            this.label6.TabIndex = 97;
            this.label6.Text = "Imges Extension:";
            // 
            // txtOuterboxQty
            // 
            this.txtOuterboxQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOuterboxQty.Location = new System.Drawing.Point(177, 201);
            this.txtOuterboxQty.MaxLength = 8;
            this.txtOuterboxQty.Name = "txtOuterboxQty";
            this.txtOuterboxQty.Size = new System.Drawing.Size(208, 23);
            this.txtOuterboxQty.TabIndex = 102;
            // 
            // txtInnerboxQty
            // 
            this.txtInnerboxQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInnerboxQty.Location = new System.Drawing.Point(177, 163);
            this.txtInnerboxQty.MaxLength = 8;
            this.txtInnerboxQty.Name = "txtInnerboxQty";
            this.txtInnerboxQty.Size = new System.Drawing.Size(208, 23);
            this.txtInnerboxQty.TabIndex = 101;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(29, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 100;
            this.label7.Text = "Outer Box Qty:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(29, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 99;
            this.label5.Text = "Inner Box Qty:";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 611);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NHA Dashboard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeparator)).EndInit();
            this.gbExistingState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAddState)).EndInit();
            this.gbNewState.ResumeLayout(false);
            this.gbNewState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbReturn)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnProcessing;
        private System.Windows.Forms.Label lblSortingInput;
        private System.Windows.Forms.CheckedListBox checkedListBoxItems;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton cbpng;
        private System.Windows.Forms.RadioButton cbjpg;
        private System.Windows.Forms.RadioButton cbjpeg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOuterboxQty;
        private System.Windows.Forms.TextBox txtInnerboxQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxState;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pbAddState;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.PictureBox pbReturn;
        private System.Windows.Forms.GroupBox gbExistingState;
        private System.Windows.Forms.GroupBox gbNewState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgvSeparator;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label District;
        private System.Windows.Forms.ComboBox cbxDistrict;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxUnprocessedState;
        private System.Windows.Forms.Button btnPrinting;
    }
}