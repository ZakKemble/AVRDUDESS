namespace avrdudess
{
    partial class FormUISimple
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
            this.gbFlashFile = new System.Windows.Forms.GroupBox();
            this.pbFlashUsage = new System.Windows.Forms.PictureBox();
            this.txtFlashFile = new System.Windows.Forms.TextBox();
            this.cmbFlashFormat = new System.Windows.Forms.ComboBox();
            this.btnFlashBrowse = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.btnForceStop = new System.Windows.Forms.Button();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.btnProgram = new System.Windows.Forms.Button();
            this.gbFlashFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlashUsage)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFlashFile
            // 
            this.gbFlashFile.Controls.Add(this.pbFlashUsage);
            this.gbFlashFile.Controls.Add(this.txtFlashFile);
            this.gbFlashFile.Controls.Add(this.cmbFlashFormat);
            this.gbFlashFile.Controls.Add(this.btnFlashBrowse);
            this.gbFlashFile.Location = new System.Drawing.Point(12, 12);
            this.gbFlashFile.Name = "gbFlashFile";
            this.gbFlashFile.Size = new System.Drawing.Size(431, 73);
            this.gbFlashFile.TabIndex = 53;
            this.gbFlashFile.TabStop = false;
            this.gbFlashFile.Text = "Flash";
            // 
            // pbFlashUsage
            // 
            this.pbFlashUsage.Location = new System.Drawing.Point(6, 9);
            this.pbFlashUsage.Name = "pbFlashUsage";
            this.pbFlashUsage.Size = new System.Drawing.Size(388, 10);
            this.pbFlashUsage.TabIndex = 57;
            this.pbFlashUsage.TabStop = false;
            // 
            // txtFlashFile
            // 
            this.txtFlashFile.Location = new System.Drawing.Point(6, 19);
            this.txtFlashFile.Name = "txtFlashFile";
            this.txtFlashFile.Size = new System.Drawing.Size(388, 20);
            this.txtFlashFile.TabIndex = 6;
            // 
            // cmbFlashFormat
            // 
            this.cmbFlashFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlashFormat.FormattingEnabled = true;
            this.cmbFlashFormat.Location = new System.Drawing.Point(294, 44);
            this.cmbFlashFormat.Name = "cmbFlashFormat";
            this.cmbFlashFormat.Size = new System.Drawing.Size(131, 21);
            this.cmbFlashFormat.TabIndex = 8;
            // 
            // btnFlashBrowse
            // 
            this.btnFlashBrowse.Location = new System.Drawing.Point(400, 19);
            this.btnFlashBrowse.Name = "btnFlashBrowse";
            this.btnFlashBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnFlashBrowse.TabIndex = 7;
            this.btnFlashBrowse.Text = "...";
            this.btnFlashBrowse.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(12, 119);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(618, 158);
            this.txtConsole.TabIndex = 56;
            this.txtConsole.WordWrap = false;
            // 
            // btnForceStop
            // 
            this.btnForceStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnForceStop.Location = new System.Drawing.Point(555, 91);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.Size = new System.Drawing.Size(75, 23);
            this.btnForceStop.TabIndex = 55;
            this.btnForceStop.Text = "Stop";
            this.btnForceStop.UseVisualStyleBackColor = true;
            // 
            // cmbPresets
            // 
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(12, 92);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(167, 21);
            this.cmbPresets.TabIndex = 18;
            // 
            // btnProgram
            // 
            this.btnProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgram.Location = new System.Drawing.Point(185, 91);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(364, 23);
            this.btnProgram.TabIndex = 54;
            this.btnProgram.Text = "Program!";
            this.btnProgram.UseVisualStyleBackColor = true;
            // 
            // FormUISimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 289);
            this.Controls.Add(this.cmbPresets);
            this.Controls.Add(this.gbFlashFile);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnForceStop);
            this.Controls.Add(this.btnProgram);
            this.Name = "FormUISimple";
            this.Text = "FormUISimple";
            this.gbFlashFile.ResumeLayout(false);
            this.gbFlashFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlashUsage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFlashFile;
        private System.Windows.Forms.PictureBox pbFlashUsage;
        private System.Windows.Forms.TextBox txtFlashFile;
        private System.Windows.Forms.ComboBox cmbFlashFormat;
        private System.Windows.Forms.Button btnFlashBrowse;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnForceStop;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.Button btnProgram;
    }
}