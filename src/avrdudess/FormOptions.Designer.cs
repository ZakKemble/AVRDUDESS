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
            this.cbShowToolTips = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(412, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File locations (leave blank to auto-detect)";
            // 
            // btnBrowseAvrSize
            // 
            this.btnBrowseAvrSize.Location = new System.Drawing.Point(381, 68);
            this.btnBrowseAvrSize.Name = "btnBrowseAvrSize";
            this.btnBrowseAvrSize.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrSize.TabIndex = 8;
            this.btnBrowseAvrSize.Text = "...";
            this.btnBrowseAvrSize.UseVisualStyleBackColor = true;
            this.btnBrowseAvrSize.Click += new System.EventHandler(this.btnBrowseAvrSize_Click);
            // 
            // btnBrowseAvrdudeConf
            // 
            this.btnBrowseAvrdudeConf.Location = new System.Drawing.Point(381, 42);
            this.btnBrowseAvrdudeConf.Name = "btnBrowseAvrdudeConf";
            this.btnBrowseAvrdudeConf.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrdudeConf.TabIndex = 7;
            this.btnBrowseAvrdudeConf.Text = "...";
            this.btnBrowseAvrdudeConf.UseVisualStyleBackColor = true;
            this.btnBrowseAvrdudeConf.Click += new System.EventHandler(this.btnBrowseAvrdudeConf_Click);
            // 
            // btnBrowseAvrdude
            // 
            this.btnBrowseAvrdude.Location = new System.Drawing.Point(381, 16);
            this.btnBrowseAvrdude.Name = "btnBrowseAvrdude";
            this.btnBrowseAvrdude.Size = new System.Drawing.Size(25, 20);
            this.btnBrowseAvrdude.TabIndex = 6;
            this.btnBrowseAvrdude.Text = "...";
            this.btnBrowseAvrdude.UseVisualStyleBackColor = true;
            this.btnBrowseAvrdude.Click += new System.EventHandler(this.btnBrowseAvrdude_Click);
            // 
            // txtAvrSizeLocation
            // 
            this.txtAvrSizeLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrSizeLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrSizeLocation.Location = new System.Drawing.Point(82, 69);
            this.txtAvrSizeLocation.Name = "txtAvrSizeLocation";
            this.txtAvrSizeLocation.Size = new System.Drawing.Size(293, 20);
            this.txtAvrSizeLocation.TabIndex = 5;
            // 
            // txtAvrdudeConfLocation
            // 
            this.txtAvrdudeConfLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrdudeConfLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrdudeConfLocation.Location = new System.Drawing.Point(82, 42);
            this.txtAvrdudeConfLocation.Name = "txtAvrdudeConfLocation";
            this.txtAvrdudeConfLocation.Size = new System.Drawing.Size(293, 20);
            this.txtAvrdudeConfLocation.TabIndex = 4;
            // 
            // txtAvrdudeLocation
            // 
            this.txtAvrdudeLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtAvrdudeLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAvrdudeLocation.Location = new System.Drawing.Point(82, 16);
            this.txtAvrdudeLocation.Name = "txtAvrdudeLocation";
            this.txtAvrdudeLocation.Size = new System.Drawing.Size(293, 20);
            this.txtAvrdudeLocation.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbShowToolTips);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(412, 45);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other";
            // 
            // cbShowToolTips
            // 
            this.cbShowToolTips.AutoSize = true;
            this.cbShowToolTips.Checked = true;
            this.cbShowToolTips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowToolTips.Location = new System.Drawing.Point(6, 19);
            this.cbShowToolTips.Name = "cbShowToolTips";
            this.cbShowToolTips.Size = new System.Drawing.Size(89, 17);
            this.cbShowToolTips.TabIndex = 39;
            this.cbShowToolTips.Text = "Show tooltips";
            this.cbShowToolTips.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(349, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(268, 169);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(436, 199);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}