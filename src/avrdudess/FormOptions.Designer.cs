namespace avrdudess
{
    partial class FormOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseAvrSize = new System.Windows.Forms.Button();
            this.btnBrowseAvrdudeConf = new System.Windows.Forms.Button();
            this.btnBrowseAvrdude = new System.Windows.Forms.Button();
            this.txtAvrSizeLocation = new System.Windows.Forms.TextBox();
            this.txtAvrdudeConfLocation = new System.Windows.Forms.TextBox();
            this.txtAvrdudeLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbUsePrevSettings = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.cbShowToolTips = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbHiddenProgrammers = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.clbHiddenMCUs = new System.Windows.Forms.CheckedListBox();
            this.cbCheckForUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "avrdude";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "avr-size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "avrdude.conf";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnBrowseAvrSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBrowseAvrdudeConf);
            this.groupBox1.Controls.Add(this.btnBrowseAvrdude);
            this.groupBox1.Controls.Add(this.txtAvrSizeLocation);
            this.groupBox1.Controls.Add(this.txtAvrdudeConfLocation);
            this.groupBox1.Controls.Add(this.txtAvrdudeLocation);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "_GRP_FILELOCATIONS";
            // 
            // btnBrowseAvrSize
            // 
            this.btnBrowseAvrSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseAvrSize.Location = new System.Drawing.Point(479, 68);
            this.btnBrowseAvrSize.Name = "btnBrowseAvrSize";
            this.btnBrowseAvrSize.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrSize.TabIndex = 5;
            this.btnBrowseAvrSize.Text = "...";
            this.btnBrowseAvrSize.UseVisualStyleBackColor = true;
            this.btnBrowseAvrSize.Click += new System.EventHandler(this.btnBrowseAvrSize_Click);
            // 
            // btnBrowseAvrdudeConf
            // 
            this.btnBrowseAvrdudeConf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseAvrdudeConf.Location = new System.Drawing.Point(479, 42);
            this.btnBrowseAvrdudeConf.Name = "btnBrowseAvrdudeConf";
            this.btnBrowseAvrdudeConf.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrdudeConf.TabIndex = 3;
            this.btnBrowseAvrdudeConf.Text = "...";
            this.btnBrowseAvrdudeConf.UseVisualStyleBackColor = true;
            this.btnBrowseAvrdudeConf.Click += new System.EventHandler(this.btnBrowseAvrdudeConf_Click);
            // 
            // btnBrowseAvrdude
            // 
            this.btnBrowseAvrdude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseAvrdude.Location = new System.Drawing.Point(479, 16);
            this.btnBrowseAvrdude.Name = "btnBrowseAvrdude";
            this.btnBrowseAvrdude.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrdude.TabIndex = 1;
            this.btnBrowseAvrdude.Text = "...";
            this.btnBrowseAvrdude.UseVisualStyleBackColor = true;
            this.btnBrowseAvrdude.Click += new System.EventHandler(this.btnBrowseAvrdude_Click);
            // 
            // txtAvrSizeLocation
            // 
            this.txtAvrSizeLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAvrSizeLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrSizeLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrSizeLocation.Location = new System.Drawing.Point(82, 69);
            this.txtAvrSizeLocation.Name = "txtAvrSizeLocation";
            this.txtAvrSizeLocation.Size = new System.Drawing.Size(391, 20);
            this.txtAvrSizeLocation.TabIndex = 4;
            // 
            // txtAvrdudeConfLocation
            // 
            this.txtAvrdudeConfLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAvrdudeConfLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrdudeConfLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrdudeConfLocation.Location = new System.Drawing.Point(82, 42);
            this.txtAvrdudeConfLocation.Name = "txtAvrdudeConfLocation";
            this.txtAvrdudeConfLocation.Size = new System.Drawing.Size(391, 20);
            this.txtAvrdudeConfLocation.TabIndex = 2;
            // 
            // txtAvrdudeLocation
            // 
            this.txtAvrdudeLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAvrdudeLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrdudeLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrdudeLocation.Location = new System.Drawing.Point(82, 16);
            this.txtAvrdudeLocation.Name = "txtAvrdudeLocation";
            this.txtAvrdudeLocation.Size = new System.Drawing.Size(391, 20);
            this.txtAvrdudeLocation.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbCheckForUpdate);
            this.groupBox2.Controls.Add(this.cbUsePrevSettings);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbLanguage);
            this.groupBox2.Controls.Add(this.cbShowToolTips);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 98);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "_GRP_OTHER";
            // 
            // cbUsePrevSettings
            // 
            this.cbUsePrevSettings.AutoSize = true;
            this.cbUsePrevSettings.Checked = true;
            this.cbUsePrevSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUsePrevSettings.Location = new System.Drawing.Point(261, 47);
            this.cbUsePrevSettings.Name = "cbUsePrevSettings";
            this.cbUsePrevSettings.Size = new System.Drawing.Size(137, 17);
            this.cbUsePrevSettings.TabIndex = 42;
            this.cbUsePrevSettings.Text = "_USEPREVSETTINGS";
            this.cbUsePrevSettings.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "_LANGUAGE";
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(82, 22);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(173, 21);
            this.cbLanguage.TabIndex = 0;
            // 
            // cbShowToolTips
            // 
            this.cbShowToolTips.AutoSize = true;
            this.cbShowToolTips.Checked = true;
            this.cbShowToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToolTips.Location = new System.Drawing.Point(261, 24);
            this.cbShowToolTips.Name = "cbShowToolTips";
            this.cbShowToolTips.Size = new System.Drawing.Size(119, 17);
            this.cbShowToolTips.TabIndex = 1;
            this.cbShowToolTips.Text = "_SHOWTOOLTIPS";
            this.cbShowToolTips.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(447, 634);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "_CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(366, 634);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "_OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.clbHiddenProgrammers);
            this.groupBox3.Location = new System.Drawing.Point(12, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 200);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "_GRP_HIDEPROGS";
            // 
            // clbHiddenProgrammers
            // 
            this.clbHiddenProgrammers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbHiddenProgrammers.CheckOnClick = true;
            this.clbHiddenProgrammers.FormattingEnabled = true;
            this.clbHiddenProgrammers.Items.AddRange(new object[] {
            "aaa",
            "bbb",
            "ccc",
            "aaadd"});
            this.clbHiddenProgrammers.Location = new System.Drawing.Point(6, 19);
            this.clbHiddenProgrammers.Name = "clbHiddenProgrammers";
            this.clbHiddenProgrammers.Size = new System.Drawing.Size(498, 169);
            this.clbHiddenProgrammers.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.clbHiddenMCUs);
            this.groupBox4.Location = new System.Drawing.Point(12, 428);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 200);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "_GRP_HIDEMCUS";
            // 
            // clbHiddenMCUs
            // 
            this.clbHiddenMCUs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbHiddenMCUs.CheckOnClick = true;
            this.clbHiddenMCUs.FormattingEnabled = true;
            this.clbHiddenMCUs.Items.AddRange(new object[] {
            "aaa",
            "bbb",
            "ccc",
            "aaadd"});
            this.clbHiddenMCUs.Location = new System.Drawing.Point(6, 19);
            this.clbHiddenMCUs.Name = "clbHiddenMCUs";
            this.clbHiddenMCUs.Size = new System.Drawing.Size(498, 169);
            this.clbHiddenMCUs.TabIndex = 0;
            // 
            // cbCheckForUpdate
            // 
            this.cbCheckForUpdate.AutoSize = true;
            this.cbCheckForUpdate.Checked = true;
            this.cbCheckForUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCheckForUpdate.Location = new System.Drawing.Point(261, 70);
            this.cbCheckForUpdate.Name = "cbCheckForUpdate";
            this.cbCheckForUpdate.Size = new System.Drawing.Size(141, 17);
            this.cbCheckForUpdate.TabIndex = 43;
            this.cbCheckForUpdate.Text = "_CHECKFORUPDATES";
            this.cbCheckForUpdate.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 664);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "_TITLE_OPTIONS";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseAvrdude;
        private System.Windows.Forms.TextBox txtAvrSizeLocation;
        private System.Windows.Forms.TextBox txtAvrdudeConfLocation;
        private System.Windows.Forms.TextBox txtAvrdudeLocation;
        private System.Windows.Forms.Button btnBrowseAvrSize;
        private System.Windows.Forms.Button btnBrowseAvrdudeConf;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbShowToolTips;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbHiddenProgrammers;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox clbHiddenMCUs;
        private System.Windows.Forms.CheckBox cbUsePrevSettings;
        private System.Windows.Forms.CheckBox cbCheckForUpdate;
    }
}