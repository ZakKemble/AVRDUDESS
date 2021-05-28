namespace avrdudess
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cmbProg = new System.Windows.Forms.ComboBox();
            this.cmbMCU = new System.Windows.Forms.ComboBox();
            this.cbForce = new System.Windows.Forms.CheckBox();
            this.cbNoVerify = new System.Windows.Forms.CheckBox();
            this.btnProgram = new System.Windows.Forms.Button();
            this.txtHFuse = new System.Windows.Forms.TextBox();
            this.txtLFuse = new System.Windows.Forms.TextBox();
            this.txtEFuse = new System.Windows.Forms.TextBox();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPresetMgr = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbUSBaspFreq = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBitClock = new System.Windows.Forms.TextBox();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.txtCmdLine = new System.Windows.Forms.TextBox();
            this.gbEEPROMFile = new System.Windows.Forms.GroupBox();
            this.pbEEPROMUsage = new System.Windows.Forms.PictureBox();
            this.btnEEPROMGo = new System.Windows.Forms.Button();
            this.pEEPROMOp = new System.Windows.Forms.Panel();
            this.rbEEPROMOpVerify = new System.Windows.Forms.RadioButton();
            this.rbEEPROMOpRead = new System.Windows.Forms.RadioButton();
            this.rbEEPROMOpWrite = new System.Windows.Forms.RadioButton();
            this.txtEEPROMFile = new System.Windows.Forms.TextBox();
            this.cmbEEPROMFormat = new System.Windows.Forms.ComboBox();
            this.btnEEPROMBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.gbFlashFile = new System.Windows.Forms.GroupBox();
            this.pbFlashUsage = new System.Windows.Forms.PictureBox();
            this.btnFlashGo = new System.Windows.Forms.Button();
            this.pFlashOp = new System.Windows.Forms.Panel();
            this.rbFlashOpVerify = new System.Windows.Forms.RadioButton();
            this.rbFlashOpRead = new System.Windows.Forms.RadioButton();
            this.rbFlashOpWrite = new System.Windows.Forms.RadioButton();
            this.txtFlashFile = new System.Windows.Forms.TextBox();
            this.cmbFlashFormat = new System.Windows.Forms.ComboBox();
            this.btnFlashBrowse = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWriteLock = new System.Windows.Forms.Button();
            this.btnWriteFuses = new System.Windows.Forms.Button();
            this.btnFuseSelector = new System.Windows.Forms.Button();
            this.cbSetLock = new System.Windows.Forms.CheckBox();
            this.cbSetFuses = new System.Windows.Forms.CheckBox();
            this.btnReadLock = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLock = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReadFuses = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdVerbose = new System.Windows.Forms.ComboBox();
            this.cbDoNotWrite = new System.Windows.Forms.CheckBox();
            this.cbDisableFlashErase = new System.Windows.Forms.CheckBox();
            this.cbEraseFlashEEPROM = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnForceStop = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtAdditional = new System.Windows.Forms.TextBox();
            this.statusBar1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTooltip = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDetect = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lblSig = new System.Windows.Forms.Label();
            this.lblFlashSize = new System.Windows.Forms.Label();
            this.lblEEPROMSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOptions = new System.Windows.Forms.Button();
            this.rtxtConsole = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbEEPROMFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEEPROMUsage)).BeginInit();
            this.pEEPROMOp.SuspendLayout();
            this.gbFlashFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlashUsage)).BeginInit();
            this.pFlashOp.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.statusBar1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProg
            // 
            this.cmbProg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProg.FormattingEnabled = true;
            this.cmbProg.Location = new System.Drawing.Point(6, 19);
            this.cmbProg.Name = "cmbProg";
            this.cmbProg.Size = new System.Drawing.Size(417, 21);
            this.cmbProg.TabIndex = 0;
            // 
            // cmbMCU
            // 
            this.cmbMCU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMCU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMCU.FormattingEnabled = true;
            this.cmbMCU.Location = new System.Drawing.Point(6, 19);
            this.cmbMCU.Name = "cmbMCU";
            this.cmbMCU.Size = new System.Drawing.Size(168, 21);
            this.cmbMCU.TabIndex = 0;
            // 
            // cbForce
            // 
            this.cbForce.AutoSize = true;
            this.cbForce.Location = new System.Drawing.Point(6, 19);
            this.cbForce.Name = "cbForce";
            this.cbForce.Size = new System.Drawing.Size(68, 17);
            this.cbForce.TabIndex = 0;
            this.cbForce.Text = "_FORCE";
            this.cbForce.UseVisualStyleBackColor = true;
            // 
            // cbNoVerify
            // 
            this.cbNoVerify.AutoSize = true;
            this.cbNoVerify.Location = new System.Drawing.Point(6, 42);
            this.cbNoVerify.Name = "cbNoVerify";
            this.cbNoVerify.Size = new System.Drawing.Size(88, 17);
            this.cbNoVerify.TabIndex = 1;
            this.cbNoVerify.Text = "_DISVERIFY";
            this.cbNoVerify.UseVisualStyleBackColor = true;
            // 
            // btnProgram
            // 
            this.btnProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProgram.Location = new System.Drawing.Point(6, 356);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(176, 23);
            this.btnProgram.TabIndex = 8;
            this.btnProgram.Text = "_DOPROGRAM";
            this.btnProgram.UseVisualStyleBackColor = true;
            this.btnProgram.Click += new System.EventHandler(this.btnProgram_Click);
            // 
            // txtHFuse
            // 
            this.txtHFuse.Location = new System.Drawing.Point(32, 42);
            this.txtHFuse.Name = "txtHFuse";
            this.txtHFuse.Size = new System.Drawing.Size(43, 20);
            this.txtHFuse.TabIndex = 1;
            this.txtHFuse.Tag = "";
            this.txtHFuse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHex_KeyPress);
            // 
            // txtLFuse
            // 
            this.txtLFuse.Location = new System.Drawing.Point(32, 16);
            this.txtLFuse.Name = "txtLFuse";
            this.txtLFuse.Size = new System.Drawing.Size(43, 20);
            this.txtLFuse.TabIndex = 0;
            this.txtLFuse.Tag = "";
            this.txtLFuse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHex_KeyPress);
            // 
            // txtEFuse
            // 
            this.txtEFuse.Location = new System.Drawing.Point(32, 68);
            this.txtEFuse.Name = "txtEFuse";
            this.txtEFuse.Size = new System.Drawing.Size(43, 20);
            this.txtEFuse.TabIndex = 2;
            this.txtEFuse.Tag = "";
            this.txtEFuse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHex_KeyPress);
            // 
            // cmbPresets
            // 
            this.cmbPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(6, 19);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(168, 21);
            this.cmbPresets.TabIndex = 0;
            this.cmbPresets.SelectedIndexChanged += new System.EventHandler(this.cmbPresets_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPresetMgr);
            this.groupBox1.Controls.Add(this.cmbPresets);
            this.groupBox1.Location = new System.Drawing.Point(0, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 73);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "_GRP_PRESET";
            // 
            // btnPresetMgr
            // 
            this.btnPresetMgr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPresetMgr.Location = new System.Drawing.Point(6, 46);
            this.btnPresetMgr.Name = "btnPresetMgr";
            this.btnPresetMgr.Size = new System.Drawing.Size(168, 23);
            this.btnPresetMgr.TabIndex = 1;
            this.btnPresetMgr.Text = "_PRESETMGR";
            this.btnPresetMgr.UseVisualStyleBackColor = true;
            this.btnPresetMgr.Click += new System.EventHandler(this.btnPresetMgr_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmbUSBaspFreq);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtBitClock);
            this.groupBox2.Controls.Add(this.txtBaudRate);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.cmbPort);
            this.groupBox2.Controls.Add(this.cmbProg);
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 90);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "_GRP_PROGRAMMER";
            // 
            // cmbUSBaspFreq
            // 
            this.cmbUSBaspFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUSBaspFreq.FormattingEnabled = true;
            this.cmbUSBaspFreq.Location = new System.Drawing.Point(288, 58);
            this.cmbUSBaspFreq.Name = "cmbUSBaspFreq";
            this.cmbUSBaspFreq.Size = new System.Drawing.Size(80, 21);
            this.cmbUSBaspFreq.TabIndex = 4;
            this.cmbUSBaspFreq.SelectedIndexChanged += new System.EventHandler(this.cmbUSBaspFreq_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(288, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "_BITCLOCK";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(147, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "_BAUDRATE";
            // 
            // txtBitClock
            // 
            this.txtBitClock.Location = new System.Drawing.Point(288, 59);
            this.txtBitClock.Name = "txtBitClock";
            this.txtBitClock.Size = new System.Drawing.Size(135, 20);
            this.txtBitClock.TabIndex = 3;
            this.txtBitClock.Tag = "";
            this.txtBitClock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(147, 59);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(135, 20);
            this.txtBaudRate.TabIndex = 2;
            this.txtBaudRate.Tag = "";
            this.txtBaudRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "_PORT";
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(6, 59);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(135, 21);
            this.cmbPort.TabIndex = 1;
            // 
            // txtCmdLine
            // 
            this.txtCmdLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmdLine.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCmdLine.Location = new System.Drawing.Point(6, 385);
            this.txtCmdLine.Name = "txtCmdLine";
            this.txtCmdLine.ReadOnly = true;
            this.txtCmdLine.Size = new System.Drawing.Size(431, 20);
            this.txtCmdLine.TabIndex = 12;
            this.txtCmdLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // gbEEPROMFile
            // 
            this.gbEEPROMFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEEPROMFile.Controls.Add(this.pbEEPROMUsage);
            this.gbEEPROMFile.Controls.Add(this.btnEEPROMGo);
            this.gbEEPROMFile.Controls.Add(this.pEEPROMOp);
            this.gbEEPROMFile.Controls.Add(this.txtEEPROMFile);
            this.gbEEPROMFile.Controls.Add(this.cmbEEPROMFormat);
            this.gbEEPROMFile.Controls.Add(this.btnEEPROMBrowse);
            this.gbEEPROMFile.Controls.Add(this.label6);
            this.gbEEPROMFile.Location = new System.Drawing.Point(6, 178);
            this.gbEEPROMFile.Name = "gbEEPROMFile";
            this.gbEEPROMFile.Size = new System.Drawing.Size(431, 73);
            this.gbEEPROMFile.TabIndex = 2;
            this.gbEEPROMFile.TabStop = false;
            this.gbEEPROMFile.Text = "_GRP_EEPROM";
            // 
            // pbEEPROMUsage
            // 
            this.pbEEPROMUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbEEPROMUsage.Location = new System.Drawing.Point(6, 14);
            this.pbEEPROMUsage.Name = "pbEEPROMUsage";
            this.pbEEPROMUsage.Size = new System.Drawing.Size(388, 10);
            this.pbEEPROMUsage.TabIndex = 58;
            this.pbEEPROMUsage.TabStop = false;
            // 
            // btnEEPROMGo
            // 
            this.btnEEPROMGo.Location = new System.Drawing.Point(181, 44);
            this.btnEEPROMGo.Name = "btnEEPROMGo";
            this.btnEEPROMGo.Size = new System.Drawing.Size(52, 23);
            this.btnEEPROMGo.TabIndex = 3;
            this.btnEEPROMGo.Text = "_GO";
            this.btnEEPROMGo.UseVisualStyleBackColor = true;
            this.btnEEPROMGo.Click += new System.EventHandler(this.btnEEPROMGo_Click);
            // 
            // pEEPROMOp
            // 
            this.pEEPROMOp.Controls.Add(this.rbEEPROMOpVerify);
            this.pEEPROMOp.Controls.Add(this.rbEEPROMOpRead);
            this.pEEPROMOp.Controls.Add(this.rbEEPROMOpWrite);
            this.pEEPROMOp.Location = new System.Drawing.Point(3, 45);
            this.pEEPROMOp.Name = "pEEPROMOp";
            this.pEEPROMOp.Size = new System.Drawing.Size(172, 22);
            this.pEEPROMOp.TabIndex = 2;
            // 
            // rbEEPROMOpVerify
            // 
            this.rbEEPROMOpVerify.AutoSize = true;
            this.rbEEPROMOpVerify.Location = new System.Drawing.Point(119, 3);
            this.rbEEPROMOpVerify.Name = "rbEEPROMOpVerify";
            this.rbEEPROMOpVerify.Size = new System.Drawing.Size(69, 17);
            this.rbEEPROMOpVerify.TabIndex = 2;
            this.rbEEPROMOpVerify.Text = "_VERIFY";
            this.rbEEPROMOpVerify.UseVisualStyleBackColor = true;
            this.rbEEPROMOpVerify.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // rbEEPROMOpRead
            // 
            this.rbEEPROMOpRead.AutoSize = true;
            this.rbEEPROMOpRead.Location = new System.Drawing.Point(62, 3);
            this.rbEEPROMOpRead.Name = "rbEEPROMOpRead";
            this.rbEEPROMOpRead.Size = new System.Drawing.Size(61, 17);
            this.rbEEPROMOpRead.TabIndex = 1;
            this.rbEEPROMOpRead.Text = "_READ";
            this.rbEEPROMOpRead.UseVisualStyleBackColor = true;
            this.rbEEPROMOpRead.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // rbEEPROMOpWrite
            // 
            this.rbEEPROMOpWrite.AutoSize = true;
            this.rbEEPROMOpWrite.Checked = true;
            this.rbEEPROMOpWrite.Location = new System.Drawing.Point(6, 3);
            this.rbEEPROMOpWrite.Name = "rbEEPROMOpWrite";
            this.rbEEPROMOpWrite.Size = new System.Drawing.Size(67, 17);
            this.rbEEPROMOpWrite.TabIndex = 0;
            this.rbEEPROMOpWrite.TabStop = true;
            this.rbEEPROMOpWrite.Text = "_WRITE";
            this.rbEEPROMOpWrite.UseVisualStyleBackColor = true;
            this.rbEEPROMOpWrite.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // txtEEPROMFile
            // 
            this.txtEEPROMFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEEPROMFile.Location = new System.Drawing.Point(6, 19);
            this.txtEEPROMFile.Name = "txtEEPROMFile";
            this.txtEEPROMFile.Size = new System.Drawing.Size(388, 20);
            this.txtEEPROMFile.TabIndex = 0;
            // 
            // cmbEEPROMFormat
            // 
            this.cmbEEPROMFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEEPROMFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEEPROMFormat.FormattingEnabled = true;
            this.cmbEEPROMFormat.Location = new System.Drawing.Point(294, 45);
            this.cmbEEPROMFormat.Name = "cmbEEPROMFormat";
            this.cmbEEPROMFormat.Size = new System.Drawing.Size(131, 21);
            this.cmbEEPROMFormat.TabIndex = 4;
            // 
            // btnEEPROMBrowse
            // 
            this.btnEEPROMBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEEPROMBrowse.Location = new System.Drawing.Point(400, 19);
            this.btnEEPROMBrowse.Name = "btnEEPROMBrowse";
            this.btnEEPROMBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnEEPROMBrowse.TabIndex = 1;
            this.btnEEPROMBrowse.Text = "...";
            this.btnEEPROMBrowse.UseVisualStyleBackColor = true;
            this.btnEEPROMBrowse.Click += new System.EventHandler(this.btnFlashEEPROMBrowse_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(249, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "_FORMAT";
            // 
            // gbFlashFile
            // 
            this.gbFlashFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFlashFile.Controls.Add(this.pbFlashUsage);
            this.gbFlashFile.Controls.Add(this.btnFlashGo);
            this.gbFlashFile.Controls.Add(this.pFlashOp);
            this.gbFlashFile.Controls.Add(this.txtFlashFile);
            this.gbFlashFile.Controls.Add(this.cmbFlashFormat);
            this.gbFlashFile.Controls.Add(this.btnFlashBrowse);
            this.gbFlashFile.Controls.Add(this.label11);
            this.gbFlashFile.Location = new System.Drawing.Point(6, 99);
            this.gbFlashFile.Name = "gbFlashFile";
            this.gbFlashFile.Size = new System.Drawing.Size(431, 73);
            this.gbFlashFile.TabIndex = 1;
            this.gbFlashFile.TabStop = false;
            this.gbFlashFile.Text = "_GRP_FLASH";
            // 
            // pbFlashUsage
            // 
            this.pbFlashUsage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFlashUsage.Location = new System.Drawing.Point(6, 14);
            this.pbFlashUsage.Name = "pbFlashUsage";
            this.pbFlashUsage.Size = new System.Drawing.Size(388, 10);
            this.pbFlashUsage.TabIndex = 57;
            this.pbFlashUsage.TabStop = false;
            // 
            // btnFlashGo
            // 
            this.btnFlashGo.Location = new System.Drawing.Point(181, 44);
            this.btnFlashGo.Name = "btnFlashGo";
            this.btnFlashGo.Size = new System.Drawing.Size(52, 23);
            this.btnFlashGo.TabIndex = 3;
            this.btnFlashGo.Text = "_GO";
            this.btnFlashGo.UseVisualStyleBackColor = true;
            this.btnFlashGo.Click += new System.EventHandler(this.btnFlashGo_Click);
            // 
            // pFlashOp
            // 
            this.pFlashOp.Controls.Add(this.rbFlashOpVerify);
            this.pFlashOp.Controls.Add(this.rbFlashOpRead);
            this.pFlashOp.Controls.Add(this.rbFlashOpWrite);
            this.pFlashOp.Location = new System.Drawing.Point(3, 45);
            this.pFlashOp.Name = "pFlashOp";
            this.pFlashOp.Size = new System.Drawing.Size(172, 22);
            this.pFlashOp.TabIndex = 2;
            // 
            // rbFlashOpVerify
            // 
            this.rbFlashOpVerify.AutoSize = true;
            this.rbFlashOpVerify.Location = new System.Drawing.Point(119, 3);
            this.rbFlashOpVerify.Name = "rbFlashOpVerify";
            this.rbFlashOpVerify.Size = new System.Drawing.Size(69, 17);
            this.rbFlashOpVerify.TabIndex = 2;
            this.rbFlashOpVerify.Text = "_VERIFY";
            this.rbFlashOpVerify.UseVisualStyleBackColor = true;
            this.rbFlashOpVerify.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // rbFlashOpRead
            // 
            this.rbFlashOpRead.AutoSize = true;
            this.rbFlashOpRead.Location = new System.Drawing.Point(62, 3);
            this.rbFlashOpRead.Name = "rbFlashOpRead";
            this.rbFlashOpRead.Size = new System.Drawing.Size(61, 17);
            this.rbFlashOpRead.TabIndex = 1;
            this.rbFlashOpRead.Text = "_READ";
            this.rbFlashOpRead.UseVisualStyleBackColor = true;
            this.rbFlashOpRead.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // rbFlashOpWrite
            // 
            this.rbFlashOpWrite.AutoSize = true;
            this.rbFlashOpWrite.Checked = true;
            this.rbFlashOpWrite.Location = new System.Drawing.Point(6, 3);
            this.rbFlashOpWrite.Name = "rbFlashOpWrite";
            this.rbFlashOpWrite.Size = new System.Drawing.Size(67, 17);
            this.rbFlashOpWrite.TabIndex = 0;
            this.rbFlashOpWrite.TabStop = true;
            this.rbFlashOpWrite.Text = "_WRITE";
            this.rbFlashOpWrite.UseVisualStyleBackColor = true;
            this.rbFlashOpWrite.CheckedChanged += new System.EventHandler(this.radioButton_flashEEPROMOp_CheckedChanged);
            // 
            // txtFlashFile
            // 
            this.txtFlashFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFlashFile.Location = new System.Drawing.Point(6, 19);
            this.txtFlashFile.Name = "txtFlashFile";
            this.txtFlashFile.Size = new System.Drawing.Size(388, 20);
            this.txtFlashFile.TabIndex = 0;
            // 
            // cmbFlashFormat
            // 
            this.cmbFlashFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFlashFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlashFormat.FormattingEnabled = true;
            this.cmbFlashFormat.Location = new System.Drawing.Point(294, 44);
            this.cmbFlashFormat.Name = "cmbFlashFormat";
            this.cmbFlashFormat.Size = new System.Drawing.Size(131, 21);
            this.cmbFlashFormat.TabIndex = 4;
            // 
            // btnFlashBrowse
            // 
            this.btnFlashBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlashBrowse.Location = new System.Drawing.Point(400, 19);
            this.btnFlashBrowse.Name = "btnFlashBrowse";
            this.btnFlashBrowse.Size = new System.Drawing.Size(25, 20);
            this.btnFlashBrowse.TabIndex = 1;
            this.btnFlashBrowse.Text = "...";
            this.btnFlashBrowse.UseVisualStyleBackColor = true;
            this.btnFlashBrowse.Click += new System.EventHandler(this.btnFlashEEPROMBrowse_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(249, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "_FORMAT";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnWriteLock);
            this.groupBox4.Controls.Add(this.btnWriteFuses);
            this.groupBox4.Controls.Add(this.btnFuseSelector);
            this.groupBox4.Controls.Add(this.cbSetLock);
            this.groupBox4.Controls.Add(this.cbSetFuses);
            this.groupBox4.Controls.Add(this.btnReadLock);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtLock);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.btnReadFuses);
            this.groupBox4.Controls.Add(this.txtHFuse);
            this.groupBox4.Controls.Add(this.txtLFuse);
            this.groupBox4.Controls.Add(this.txtEFuse);
            this.groupBox4.Location = new System.Drawing.Point(0, 178);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 172);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "_GRP_FUSELOCK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "LB";
            // 
            // btnWriteLock
            // 
            this.btnWriteLock.Location = new System.Drawing.Point(132, 92);
            this.btnWriteLock.Name = "btnWriteLock";
            this.btnWriteLock.Size = new System.Drawing.Size(43, 23);
            this.btnWriteLock.TabIndex = 9;
            this.btnWriteLock.Text = "_WRITE";
            this.btnWriteLock.UseVisualStyleBackColor = true;
            this.btnWriteLock.Click += new System.EventHandler(this.btnWriteLock_Click);
            // 
            // btnWriteFuses
            // 
            this.btnWriteFuses.Location = new System.Drawing.Point(132, 14);
            this.btnWriteFuses.Name = "btnWriteFuses";
            this.btnWriteFuses.Size = new System.Drawing.Size(43, 23);
            this.btnWriteFuses.TabIndex = 4;
            this.btnWriteFuses.Text = "_WRITE";
            this.btnWriteFuses.UseVisualStyleBackColor = true;
            this.btnWriteFuses.Click += new System.EventHandler(this.btnWriteFuses_Click);
            // 
            // btnFuseSelector
            // 
            this.btnFuseSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFuseSelector.Location = new System.Drawing.Point(6, 143);
            this.btnFuseSelector.Name = "btnFuseSelector";
            this.btnFuseSelector.Size = new System.Drawing.Size(170, 23);
            this.btnFuseSelector.TabIndex = 11;
            this.btnFuseSelector.Text = "_BITSELECTOR";
            this.btnFuseSelector.UseVisualStyleBackColor = true;
            this.btnFuseSelector.Click += new System.EventHandler(this.btnFuseSelector_Click);
            // 
            // cbSetLock
            // 
            this.cbSetLock.AutoSize = true;
            this.cbSetLock.Location = new System.Drawing.Point(83, 121);
            this.cbSetLock.Name = "cbSetLock";
            this.cbSetLock.Size = new System.Drawing.Size(81, 17);
            this.cbSetLock.TabIndex = 10;
            this.cbSetLock.Text = "_SETLOCK";
            this.cbSetLock.UseVisualStyleBackColor = true;
            // 
            // cbSetFuses
            // 
            this.cbSetFuses.AutoSize = true;
            this.cbSetFuses.Location = new System.Drawing.Point(81, 43);
            this.cbSetFuses.Name = "cbSetFuses";
            this.cbSetFuses.Size = new System.Drawing.Size(88, 17);
            this.cbSetFuses.TabIndex = 5;
            this.cbSetFuses.Text = "_SETFUSES";
            this.cbSetFuses.UseVisualStyleBackColor = true;
            // 
            // btnReadLock
            // 
            this.btnReadLock.Location = new System.Drawing.Point(83, 92);
            this.btnReadLock.Name = "btnReadLock";
            this.btnReadLock.Size = new System.Drawing.Size(43, 23);
            this.btnReadLock.TabIndex = 8;
            this.btnReadLock.Text = "_READ";
            this.btnReadLock.UseVisualStyleBackColor = true;
            this.btnReadLock.Click += new System.EventHandler(this.btnReadLock_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "E";
            // 
            // txtLock
            // 
            this.txtLock.Location = new System.Drawing.Point(32, 94);
            this.txtLock.Name = "txtLock";
            this.txtLock.Size = new System.Drawing.Size(43, 20);
            this.txtLock.TabIndex = 7;
            this.txtLock.Tag = "";
            this.txtLock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHex_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "L";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "H";
            // 
            // btnReadFuses
            // 
            this.btnReadFuses.Location = new System.Drawing.Point(83, 14);
            this.btnReadFuses.Name = "btnReadFuses";
            this.btnReadFuses.Size = new System.Drawing.Size(43, 23);
            this.btnReadFuses.TabIndex = 3;
            this.btnReadFuses.Text = "_READ";
            this.btnReadFuses.UseVisualStyleBackColor = true;
            this.btnReadFuses.Click += new System.EventHandler(this.btnReadFuses_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.cmdVerbose);
            this.groupBox5.Controls.Add(this.cbDoNotWrite);
            this.groupBox5.Controls.Add(this.cbForce);
            this.groupBox5.Controls.Add(this.cbDisableFlashErase);
            this.groupBox5.Controls.Add(this.cbEraseFlashEEPROM);
            this.groupBox5.Controls.Add(this.cbNoVerify);
            this.groupBox5.Location = new System.Drawing.Point(6, 257);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(431, 93);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "_GRP_OPTIONS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(191, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "_VERBOSITY";
            // 
            // cmdVerbose
            // 
            this.cmdVerbose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdVerbose.FormattingEnabled = true;
            this.cmdVerbose.Location = new System.Drawing.Point(247, 63);
            this.cmdVerbose.Name = "cmdVerbose";
            this.cmdVerbose.Size = new System.Drawing.Size(95, 21);
            this.cmdVerbose.TabIndex = 5;
            // 
            // cbDoNotWrite
            // 
            this.cbDoNotWrite.AutoSize = true;
            this.cbDoNotWrite.Location = new System.Drawing.Point(190, 40);
            this.cbDoNotWrite.Name = "cbDoNotWrite";
            this.cbDoNotWrite.Size = new System.Drawing.Size(107, 17);
            this.cbDoNotWrite.TabIndex = 4;
            this.cbDoNotWrite.Text = "_DONOTWRITE";
            this.cbDoNotWrite.UseVisualStyleBackColor = true;
            // 
            // cbDisableFlashErase
            // 
            this.cbDisableFlashErase.AutoSize = true;
            this.cbDisableFlashErase.Location = new System.Drawing.Point(6, 65);
            this.cbDisableFlashErase.Name = "cbDisableFlashErase";
            this.cbDisableFlashErase.Size = new System.Drawing.Size(120, 17);
            this.cbDisableFlashErase.TabIndex = 2;
            this.cbDisableFlashErase.Text = "_DISFLASHERASE";
            this.cbDisableFlashErase.UseVisualStyleBackColor = true;
            // 
            // cbEraseFlashEEPROM
            // 
            this.cbEraseFlashEEPROM.AutoSize = true;
            this.cbEraseFlashEEPROM.Location = new System.Drawing.Point(190, 19);
            this.cbEraseFlashEEPROM.Name = "cbEraseFlashEEPROM";
            this.cbEraseFlashEEPROM.Size = new System.Drawing.Size(123, 17);
            this.cbEraseFlashEEPROM.TabIndex = 3;
            this.cbEraseFlashEEPROM.Text = "_ERASEFLASHEEP";
            this.cbEraseFlashEEPROM.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(411, 356);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(26, 23);
            this.btnAbout.TabIndex = 11;
            this.btnAbout.Text = "?";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnForceStop
            // 
            this.btnForceStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnForceStop.Location = new System.Drawing.Point(188, 356);
            this.btnForceStop.Name = "btnForceStop";
            this.btnForceStop.Size = new System.Drawing.Size(75, 23);
            this.btnForceStop.TabIndex = 9;
            this.btnForceStop.Text = "_DOSTOP";
            this.btnForceStop.UseVisualStyleBackColor = true;
            this.btnForceStop.Click += new System.EventHandler(this.btnForceStop_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.txtAdditional);
            this.groupBox7.Location = new System.Drawing.Point(0, 357);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(182, 48);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "_GRP_ADDCMDARGS";
            // 
            // txtAdditional
            // 
            this.txtAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdditional.Location = new System.Drawing.Point(6, 19);
            this.txtAdditional.Name = "txtAdditional";
            this.txtAdditional.Size = new System.Drawing.Size(168, 20);
            this.txtAdditional.TabIndex = 0;
            // 
            // statusBar1
            // 
            this.statusBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.toolStripStatusLabel1,
            this.tssTooltip});
            this.statusBar1.Location = new System.Drawing.Point(0, 586);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(644, 22);
            this.statusBar1.TabIndex = 37;
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(88, 17);
            this.tssStatus.Text = "_STATUSREADY";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(525, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // tssTooltip
            // 
            this.tssTooltip.Name = "tssTooltip";
            this.tssTooltip.Size = new System.Drawing.Size(16, 17);
            this.tssTooltip.Text = "...";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAll,
            this.tsmiCopy,
            this.tsmiClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(96, 70);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(95, 22);
            this.tsmiSelectAll.Text = "Select all";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(95, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(95, 22);
            this.tsmiClear.Text = "Clear";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // btnDetect
            // 
            this.btnDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetect.Location = new System.Drawing.Point(99, 61);
            this.btnDetect.Name = "btnDetect";
            this.btnDetect.Size = new System.Drawing.Size(75, 23);
            this.btnDetect.TabIndex = 1;
            this.btnDetect.Text = "_DETECT";
            this.btnDetect.UseVisualStyleBackColor = true;
            this.btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.lblSig);
            this.groupBox9.Controls.Add(this.lblFlashSize);
            this.groupBox9.Controls.Add(this.lblEEPROMSize);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.label3);
            this.groupBox9.Controls.Add(this.cmbMCU);
            this.groupBox9.Controls.Add(this.btnDetect);
            this.groupBox9.Location = new System.Drawing.Point(0, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(182, 90);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "_GRP_MCU";
            // 
            // lblSig
            // 
            this.lblSig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSig.AutoSize = true;
            this.lblSig.Location = new System.Drawing.Point(132, 46);
            this.lblSig.Name = "lblSig";
            this.lblSig.Size = new System.Drawing.Size(10, 13);
            this.lblSig.TabIndex = 58;
            this.lblSig.Text = "-";
            // 
            // lblFlashSize
            // 
            this.lblFlashSize.AutoSize = true;
            this.lblFlashSize.Location = new System.Drawing.Point(59, 46);
            this.lblFlashSize.Name = "lblFlashSize";
            this.lblFlashSize.Size = new System.Drawing.Size(10, 13);
            this.lblFlashSize.TabIndex = 57;
            this.lblFlashSize.Text = "-";
            // 
            // lblEEPROMSize
            // 
            this.lblEEPROMSize.AutoSize = true;
            this.lblEEPROMSize.Location = new System.Drawing.Point(59, 67);
            this.lblEEPROMSize.Name = "lblEEPROMSize";
            this.lblEEPROMSize.Size = new System.Drawing.Size(10, 13);
            this.lblEEPROMSize.TabIndex = 56;
            this.lblEEPROMSize.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "_EEPROMSZ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "_FLASHSZ";
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions.Location = new System.Drawing.Point(330, 356);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(75, 23);
            this.btnOptions.TabIndex = 10;
            this.btnOptions.Text = "_BTN_OPTIONS";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // rtxtConsole
            // 
            this.rtxtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtConsole.BackColor = System.Drawing.Color.Black;
            this.rtxtConsole.ContextMenuStrip = this.contextMenuStrip1;
            this.rtxtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtConsole.ForeColor = System.Drawing.Color.White;
            this.rtxtConsole.Location = new System.Drawing.Point(12, 420);
            this.rtxtConsole.Name = "rtxtConsole";
            this.rtxtConsole.ReadOnly = true;
            this.rtxtConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtxtConsole.Size = new System.Drawing.Size(618, 158);
            this.rtxtConsole.TabIndex = 13;
            this.rtxtConsole.Text = "";
            this.rtxtConsole.WordWrap = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(6, 9);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.btnProgram);
            this.splitContainer1.Panel1.Controls.Add(this.txtCmdLine);
            this.splitContainer1.Panel1.Controls.Add(this.btnOptions);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.gbEEPROMFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnAbout);
            this.splitContainer1.Panel1.Controls.Add(this.gbFlashFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnForceStop);
            this.splitContainer1.Panel1MinSize = 440;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox9);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox7);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(630, 410);
            this.splitContainer1.SplitterDistance = 440;
            this.splitContainer1.TabIndex = 38;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnForceStop;
            this.ClientSize = new System.Drawing.Size(644, 608);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.rtxtConsole);
            this.Controls.Add(this.statusBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbEEPROMFile.ResumeLayout(false);
            this.gbEEPROMFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEEPROMUsage)).EndInit();
            this.pEEPROMOp.ResumeLayout(false);
            this.pEEPROMOp.PerformLayout();
            this.gbFlashFile.ResumeLayout(false);
            this.gbFlashFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFlashUsage)).EndInit();
            this.pFlashOp.ResumeLayout(false);
            this.pFlashOp.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.statusBar1.ResumeLayout(false);
            this.statusBar1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProg;
        private System.Windows.Forms.ComboBox cmbMCU;
        private System.Windows.Forms.CheckBox cbForce;
        private System.Windows.Forms.CheckBox cbNoVerify;
        private System.Windows.Forms.Button btnProgram;
        private System.Windows.Forms.TextBox txtHFuse;
        private System.Windows.Forms.TextBox txtLFuse;
        private System.Windows.Forms.TextBox txtEFuse;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCmdLine;
        private System.Windows.Forms.Button btnFlashBrowse;
        private System.Windows.Forms.TextBox txtFlashFile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnReadFuses;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbFlashFile;
        private System.Windows.Forms.Panel pFlashOp;
        private System.Windows.Forms.RadioButton rbFlashOpVerify;
        private System.Windows.Forms.RadioButton rbFlashOpRead;
        private System.Windows.Forms.RadioButton rbFlashOpWrite;
        private System.Windows.Forms.ComboBox cmbFlashFormat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbEEPROMFile;
        private System.Windows.Forms.Panel pEEPROMOp;
        private System.Windows.Forms.RadioButton rbEEPROMOpVerify;
        private System.Windows.Forms.RadioButton rbEEPROMOpRead;
        private System.Windows.Forms.RadioButton rbEEPROMOpWrite;
        private System.Windows.Forms.TextBox txtEEPROMFile;
        private System.Windows.Forms.ComboBox cmbEEPROMFormat;
        private System.Windows.Forms.Button btnEEPROMBrowse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbDoNotWrite;
        private System.Windows.Forms.CheckBox cbDisableFlashErase;
        private System.Windows.Forms.CheckBox cbEraseFlashEEPROM;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnForceStop;
        private System.Windows.Forms.TextBox txtLock;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtAdditional;
        private System.Windows.Forms.StatusStrip statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.CheckBox cbSetFuses;
        private System.Windows.Forms.Button btnReadLock;
        private System.Windows.Forms.CheckBox cbSetLock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmdVerbose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtBitClock;
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnFuseSelector;
        private System.Windows.Forms.Button btnEEPROMGo;
        private System.Windows.Forms.Button btnFlashGo;
        private System.Windows.Forms.ComboBox cmbUSBaspFreq;
        private System.Windows.Forms.Button btnWriteLock;
        private System.Windows.Forms.Button btnWriteFuses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFlashSize;
        private System.Windows.Forms.Label lblEEPROMSize;
        private System.Windows.Forms.PictureBox pbFlashUsage;
        private System.Windows.Forms.PictureBox pbEEPROMUsage;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnPresetMgr;
        private System.Windows.Forms.RichTextBox rtxtConsole;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssTooltip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblSig;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

