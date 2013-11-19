namespace avrdudess
{
    partial class FormUpdate
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnLater = new System.Windows.Forms.Button();
            this.txtUpdateInfo = new System.Windows.Forms.TextBox();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSkip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(279, 284);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnLater
            // 
            this.btnLater.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLater.Location = new System.Drawing.Point(441, 284);
            this.btnLater.Name = "btnLater";
            this.btnLater.Size = new System.Drawing.Size(75, 23);
            this.btnLater.TabIndex = 3;
            this.btnLater.Text = "Later";
            this.btnLater.UseVisualStyleBackColor = true;
            this.btnLater.Click += new System.EventHandler(this.btnLater_Click);
            // 
            // txtUpdateInfo
            // 
            this.txtUpdateInfo.Location = new System.Drawing.Point(15, 38);
            this.txtUpdateInfo.Multiline = true;
            this.txtUpdateInfo.Name = "txtUpdateInfo";
            this.txtUpdateInfo.ReadOnly = true;
            this.txtUpdateInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUpdateInfo.Size = new System.Drawing.Size(501, 240);
            this.txtUpdateInfo.TabIndex = 4;
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(99, 22);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(35, 13);
            this.lblNewVersion.TabIndex = 5;
            this.lblNewVersion.Text = "label1";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(99, 9);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentVersion.TabIndex = 6;
            this.lblCurrentVersion.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Current version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "New version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(429, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Update available";
            // 
            // btnSkip
            // 
            this.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSkip.Location = new System.Drawing.Point(360, 284);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 23);
            this.btnSkip.TabIndex = 10;
            this.btnSkip.Text = "Skip version";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // FormUpdate
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnLater;
            this.ClientSize = new System.Drawing.Size(528, 316);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.txtUpdateInfo);
            this.Controls.Add(this.btnLater);
            this.Controls.Add(this.btnUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormUpdate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnLater;
        private System.Windows.Forms.TextBox txtUpdateInfo;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSkip;
    }
}