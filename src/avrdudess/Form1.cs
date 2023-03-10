﻿/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class Form1 : Form
    {
        private const string WEB_ADDR_FUSE_SETTINGS = "https://www.engbedded.com/fusecalc";

        // TODO move these somewhere else
        public const string FILEOP_WRITE = "w";
        public const string FILEOP_READ = "r";
        public const string FILEOP_VERIFY = "v";

        private ToolTip ToolTips;
        private ToolTip ToolTipCmdLine;
        private Avrdude avrdude;
        private Avrsize avrsize;
        private Presets presets;
        private CmdLine cmdLine;
        private string flashOperation   = FILEOP_WRITE;
        private string EEPROMOperation  = FILEOP_WRITE;
        private string presetToLoad;
        private bool drag               = false;
        private Point dragStart;
        private string oldBitClock;
        private Avrdude.UsbAspFreq oldUsbAspFreq;
        private MemTypeFile fileEEPROM;
        private MemTypeFile fileFlash;
        private FormWindowState lastWindowState = FormWindowState.Minimized;
        private Dictionary<Control, string> tooltipData;

        private readonly List<Programmer> programmers;
        private readonly List<MCU> mcus;

        #region Control getters and setters

        public Programmer prog
        {
            get
            {
                Programmer item = ((Programmer)cmbProg.SelectedItem);
                if (item == null || item.id == "")
                    return null;
                return item;
            }
            set
            {
                Programmer p = programmers.Find(s => s.id == value.id);
                if (p != null)
                    cmbProg.SelectedItem = p;
                else
                    Util.consoleWarning("_PROGNOTFOUND", value.id);
            }
        }
        
        public MCU mcu
        {
            get
            {
                MCU item = ((MCU)cmbMCU.SelectedItem);
                if (item == null || item.id == "")
                    return null;
                return item;
            }
            set
            {
                MCU m = mcus.Find(s => s.id == value.id);
                if (m != null)
                    cmbMCU.SelectedItem = m;
                else
                    Util.consoleWarning("_MCUNOTFOUND", value.id);
            }
        }

        public string port
        {
            get { return cmbPort.Text.Trim(); }
            set { cmbPort.Text = value; }
        }

        public string baudRate
        {
            get { return txtBaudRate.Text.Trim(); }
            set { txtBaudRate.Text = value; }
        }

        public string bitClock
        {
            get { return txtBitClock.Text.Trim(); }
            set { txtBitClock.Text = value; }
        }

        public bool force
        {
            get { return cbForce.Checked; }
            set { cbForce.Checked = value; }
        }

        public bool disableVerify
        {
            get { return cbNoVerify.Checked; }
            set { cbNoVerify.Checked = value; }
        }

        public bool disableFlashErase
        {
            get { return cbDisableFlashErase.Checked; }
            set { cbDisableFlashErase.Checked = value; }
        }

        public bool eraseFlashAndEEPROM
        {
            get { return cbEraseFlashEEPROM.Checked; }
            set { cbEraseFlashEEPROM.Checked = value; }
        }

        public bool doNotWrite
        {
            get { return cbDoNotWrite.Checked; }
            set { cbDoNotWrite.Checked = value; }
        }

        public string cmdBox
        {
            set {
                txtCmdLine.Text = value;
                if(ToolTipCmdLine != null)
                    ToolTipCmdLine.SetToolTip(txtCmdLine, value);
            }
        }

        public string flashFile
        {
            get { return txtFlashFile.Text.Trim(); }
            set { txtFlashFile.Text = value; }
        }

        public string flashFileFormat
        {
            get { return ((FileFormat)cmbFlashFormat.SelectedItem).id; }
            set
            {
                FileFormat f = Avrdude.fileFormats.Find(s => s.id == value);
                if (f != null)
                    cmbFlashFormat.SelectedItem = f;
            }
        }

        public string flashFileOperation
        {
            get { return flashOperation; }
            set
            {
                if (value == FILEOP_WRITE)
                    rbFlashOpWrite.Checked = true;
                else if (value == FILEOP_READ)
                    rbFlashOpRead.Checked = true;
                else
                    rbFlashOpVerify.Checked = true;
            }
        }

        public string EEPROMFile
        {
            get { return txtEEPROMFile.Text.Trim(); }
            set { txtEEPROMFile.Text = value; }
        }

        public string EEPROMFileFormat
        {
            get { return ((FileFormat)cmbEEPROMFormat.SelectedItem).id; }
            set
            {
                FileFormat f = Avrdude.fileFormats.Find(s => s.id == value);
                if (f != null)
                    cmbEEPROMFormat.SelectedItem = f;
            }
        }

        public string EEPROMFileOperation
        {
            get { return EEPROMOperation; }
            set
            {
                if (value == FILEOP_WRITE)
                    rbEEPROMOpWrite.Checked = true;
                else if (value == FILEOP_READ)
                    rbEEPROMOpRead.Checked = true;
                else
                    rbEEPROMOpVerify.Checked = true;
            }
        }

        public bool setFuses
        {
            get { return cbSetFuses.Checked; }
            set { cbSetFuses.Checked = value; }
        }

        public string highFuse
        {
            get { return txtHFuse.Text; }
            set { txtHFuse.Text = value; }
        }

        public string lowFuse
        {
            get { return txtLFuse.Text; }
            set { txtLFuse.Text = value; }
        }

        public string exFuse
        {
            get { return txtEFuse.Text; }
            set { txtEFuse.Text = value; }
        }

        public bool setLock
        {
            get { return cbSetLock.Checked; }
            set { cbSetLock.Checked = value; }
        }

        public string lockSetting
        {
            get { return txtLock.Text; }
            set { txtLock.Text = value; }
        }

        public string additionalSettings
        {
            get { return txtAdditional.Text; }
            set { txtAdditional.Text = value; }
        }

        public byte verbosity
        {
            get { return (byte)cmdVerbose.SelectedItem; }
            set { cmdVerbose.SelectedItem = value; }
        }

        #endregion

        public Form1(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
                presetToLoad = args[0];

            programmers = new List<Programmer>();
            mcus = new List<MCU>();

            // Load saved configuration and translations
            // TODO Need to be careful here, if an uncaught exception happens then the program
            // will just close without a JIT error message thing.
            // Having these in Form1_Load() causes some static things to not load their translations
            // like in Avrdude.fileFormats.
            Config.Prop.load();
            Language.Translation.load();

            Icon = AssemblyData.icon;

            Util.consoleSet(rtxtConsole);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setWindowTitle();

            //MaximumSize = new Size(Size.Width, int.MaxValue);
            MinimumSize = new Size(Size.Width, Height - rtxtConsole.Height);

            // Persist window location across sessions
            // Credits:
            // gl.tter
            if (Config.Prop.windowLocation != null && Config.Prop.windowLocation != new Point(0, 0))
            {
                Location = Config.Prop.windowLocation;

                // Make sure the window is on-screen (like if the window was last on a monitor that is no longer connected)
                Screen s = Screen.FromPoint(Location);

                if (Location.X + Width > s.WorkingArea.Width)
                    Location = new Point(s.WorkingArea.Width - Width, Location.Y);

                if (Location.Y + Height > s.WorkingArea.Height)
                    Location = new Point(Location.X, s.WorkingArea.Height - Height);

                if (Location.X < s.WorkingArea.X)
                    Location = new Point(s.WorkingArea.X, Location.Y);

                if (Location.Y < s.WorkingArea.Y)
                    Location = new Point(Location.X, s.WorkingArea.Y);
            }

            // Persist window size across sessions
            if (Config.Prop.windowSize != null && Config.Prop.windowSize != new Size(0, 0))
            {
                if (Config.Prop.windowSize.Width >= MinimumSize.Width &&
                    Config.Prop.windowSize.Height >= MinimumSize.Height)
                    Size = Config.Prop.windowSize;
            }

            cmdLine = new CmdLine(this);
            avrdude = new Avrdude();
            avrsize = new Avrsize();

            avrdude.OnProcessStart += avrdude_OnProcessStart;
            avrdude.OnProcessEnd += avrdude_OnProcessEnd;
            avrdude.OnVersionChange += avrdude_OnVersionChange;
            avrdude.OnDetectedMCU += avrdude_OnDetectedMCU;
            avrdude.OnReadFuseLock += Avrdude_OnReadFuseLock;

            avrdude.load();
            avrsize.load();

            // Setup memory files/usage bars
            // Flash
            fileFlash = new MemTypeFile(txtFlashFile, avrsize);
            fileFlash.sizeChanged += fileFlash_sizeChanged;
            pbFlashUsage.Width = txtFlashFile.Width;
            pbFlashUsage.Height = 3;
            pbFlashUsage.Location = new Point(txtFlashFile.Location.X, txtFlashFile.Location.Y - pbFlashUsage.Height);
            memoryUsageBar(fileFlash, pbFlashUsage, 0, true);

            // EEPROM
            fileEEPROM = new MemTypeFile(txtEEPROMFile, avrsize);
            fileEEPROM.sizeChanged += fileEEPROM_sizeChanged;
            pbEEPROMUsage.Width = txtEEPROMFile.Width;
            pbEEPROMUsage.Height = 3;
            pbEEPROMUsage.Location = new Point(txtEEPROMFile.Location.X, txtEEPROMFile.Location.Y - pbEEPROMUsage.Height);
            memoryUsageBar(fileEEPROM, pbEEPROMUsage, 0, true);

            enableClientAreaDrag(Controls);

            // Update serial ports etc
            cmbPort.DropDown += cbPort_DropDown;

            // Drag and drop flash file
            gbFlashFile.AllowDrop = true;
            gbFlashFile.DragEnter += event_DragEnter;
            gbFlashFile.DragDrop += event_DragDrop;

            // Drag and drop EEPROM file
            gbEEPROMFile.AllowDrop = true;
            gbEEPROMFile.DragEnter += event_DragEnter;
            gbEEPROMFile.DragDrop += event_DragDrop;

            updateProgMCUComboBoxes();
            
            // USBasp frequency settings
            cmbUSBaspFreq.Hide();
            setComboBoxDataSource(cmbUSBaspFreq, Avrdude.USBaspFreqs, "name");
            cmbUSBaspFreq.Width = txtBitClock.Width;
            cmbUSBaspFreq.Left = txtBitClock.Left;
            cmbUSBaspFreq.Top = txtBitClock.Top;

            // Flash & EEPROM file formats
            setComboBoxDataSource(cmbFlashFormat, Avrdude.fileFormats, "desc");
            setComboBoxDataSource(cmbEEPROMFormat, Avrdude.fileFormats, "desc");

            // Verbosity levels
            cmdVerbose.Items.Clear();
            for (byte i=0;i<5;i++)
                cmdVerbose.Items.Add(i);
            cmdVerbose.SelectedIndex = 0;

            // Put tooltip stuff into a dictionary so we can loop over it while appling translations instead
            // of having a ton of individual calls to ToolTips.SetToolTip() and Language.Translation.get()
            tooltipData = new Dictionary<Control, string>() {
                { cmbProg, "_TOOLTIP_PROGRAMMER" },
                { cmbMCU, "_TOOLTIP_MCU" },
                { cmbPort, "_TOOLTIP_PORT" },
                { txtBaudRate, "_TOOLTIP_BAUDRATE" },
                { txtBitClock, "_TOOLTIP_BITCLOCK" },
                { txtFlashFile, "_TOOLTIP_FLASHFILE" },
                { txtEEPROMFile, "_TOOLTIP_EEPROMFILE" },
                { cbForce, "_TOOLTIP_FORCE" },
                { cbNoVerify, "_TOOLTIP_NOVERIFY" },
                { cbDisableFlashErase, "_TOOLTIP_NOERASE" },
                { cbEraseFlashEEPROM, "_TOOLTIP_ERASEBOTH" },
                { cbDoNotWrite, "_TOOLTIP_NOWRITE" },
                { txtLFuse, "_TOOLTIP_LOWFUSE" },
                { txtHFuse, "_TOOLTIP_HIGHFUSE" },
                { txtEFuse, "_TOOLTIP_EXFUSE" },
                { txtLock, "_TOOLTIP_LOCKBITS" },
                { btnFlashGo, "_TOOLTIP_ONLYFLASH" },
                { btnEEPROMGo, "_TOOLTIP_ONLYEEPROM" },
                { btnWriteFuses, "_TOOLTIP_WRITEFUSES" },
                { btnWriteLock, "_TOOLTIP_WRITELOCK" },
                { btnReadFuses, "_TOOLTIP_READFUSES" },
                { btnReadLock, "_TOOLTIP_READLOCK" },
                { cbSetFuses, "_TOOLTIP_WRITEFUSESPROG" },
                { cbSetLock, "_TOOLTIP_WRITELOCKPROG" }
            };

            // Tool tips
            ToolTips = new ToolTip();
            ToolTips.ReshowDelay = 100;
            ToolTips.UseAnimation = false;
            ToolTips.UseFading = false;

            foreach (KeyValuePair<Control, string> tt in tooltipData)
            {
                ToolTips.SetToolTip(tt.Key, Language.Translation.get(tt.Value));
                tt.Key.MouseEnter += Key_MouseEnter;
                tt.Key.MouseLeave += Key_MouseLeave;
            }

            ToolTipCmdLine = new ToolTip();
            ToolTipCmdLine.ReshowDelay = 100;
            ToolTipCmdLine.UseAnimation = false;
            ToolTipCmdLine.UseFading = false;
            ToolTipCmdLine.SetToolTip(txtCmdLine, txtCmdLine.Text);

            // Load saved presets
            presets = new Presets();
            presets.load();
            presets.setDataSource(cmbPresets);

            // Enable/disable tool tips based on saved config
            ToolTips.Active = Config.Prop.toolTips;

            // If a preset has not been specified by the command line then use the last used preset
            // Credits:
            // Uwe Tanger (specifying preset in command line)
            if (presetToLoad == null)
            {
                cmbPresets.SelectedItem = presets.presets.Find(s => s.name == "Default");
                loadPresetData(Config.Prop.previousSettings);
            }
            else
            {
                PresetData p = presets.presets.Find(s => s.name == presetToLoad);
                cmbPresets.SelectedItem = (p != null) ? p : presets.presets.Find(s => s.name == "Default");
            }

            // Force update control enabled states and generate cmd line after loading preset data
            event_controlChanged(null, EventArgs.Empty);

            // Update control enabled states and generate cmd line whenever a control is changed
            cmbProg.SelectedIndexChanged += event_controlChanged;
            cmbMCU.SelectedIndexChanged += event_controlChanged;
            cbForce.CheckedChanged += event_controlChanged;
            cbNoVerify.CheckedChanged += event_controlChanged;
            txtHFuse.TextChanged += event_controlChanged;
            txtLFuse.TextChanged += event_controlChanged;
            txtEFuse.TextChanged += event_controlChanged;
            txtBitClock.TextChanged += event_controlChanged;
            txtBaudRate.TextChanged += event_controlChanged;
            cmbPort.TextChanged += event_controlChanged;
            txtEEPROMFile.TextChanged += event_controlChanged;
            cmbEEPROMFormat.SelectedIndexChanged += event_controlChanged;
            txtFlashFile.TextChanged += event_controlChanged;
            cmbFlashFormat.SelectedIndexChanged += event_controlChanged;
            cbSetLock.CheckedChanged += event_controlChanged;
            cbSetFuses.CheckedChanged += event_controlChanged;
            txtLock.TextChanged += event_controlChanged;
            cmdVerbose.SelectedIndexChanged += event_controlChanged;
            cbDoNotWrite.CheckedChanged += event_controlChanged;
            cbDisableFlashErase.CheckedChanged += event_controlChanged;
            cbEraseFlashEEPROM.CheckedChanged += event_controlChanged;
            txtAdditional.TextChanged += event_controlChanged;

            Language.Translation.apply(this);
        }

        private void Key_MouseLeave(object sender, EventArgs e)
        {
            tssTooltip.Text = "";
        }

        private void Key_MouseEnter(object sender, EventArgs e)
        {
            foreach(KeyValuePair<Control, string> tt in tooltipData)
            {
                if (tt.Key == ((Control)sender))
                {
                    tssTooltip.Text = Language.Translation.get(tt.Value);
                    break;
                }
            }
        }

        // Show AVRDUDE version etc
        private void setWindowTitle()
        {
            string avrdudeVersion = (avrdude != null) ? avrdude.version : "";
            if (avrdudeVersion == "")
                avrdudeVersion = "avrdude version UNKNOWN";
            Text = string.Format(
#if DEBUG
                "AVRDUDESS {0}.{1} ({2}) [DEBUG]",
#else
                "AVRDUDESS {0}.{1} ({2})",
#endif
                AssemblyData.version.Major,
                AssemblyData.version.Minor,
                avrdudeVersion
                );
        }

        // Update programmer and MCU combo boxes with only the non-hidden parts
        private void updateProgMCUComboBoxes()
        {
            setComboBoxDataSource(cmbMCU, null, "");
            setComboBoxDataSource(cmbProg, null, "");

            programmers.Clear();
            mcus.Clear();

            foreach (Programmer prog in avrdude.programmers)
            {
                if (!prog.hide && Config.Prop.hiddenProgrammers.Find(x => x == prog.id) == null)
                    programmers.Add(prog);
            }

            foreach (MCU mcu in avrdude.mcus)
            {
                if (!mcu.hide && Config.Prop.hiddenMCUs.Find(x => x == mcu.id) == null)
                    mcus.Add(mcu);
            }

            // Add default
            programmers.Insert(0, new Programmer("", Language.Translation.get("_SELECTPROG")));
            mcus.Insert(0, new MCU("", Language.Translation.get("_SELECTMCU")));

            // MCU & programmer combo box data source
            setComboBoxDataSource(cmbMCU, mcus, "desc");
            cmbMCU.SelectedIndexChanged -= cmbMCU_SelectedIndexChanged;
            cmbMCU.SelectedIndexChanged += cmbMCU_SelectedIndexChanged;
            setComboBoxDataSource(cmbProg, programmers, "desc");
            cmbProg.SelectedIndexChanged -= cmbProg_SelectedIndexChanged;
            cmbProg.SelectedIndexChanged += cmbProg_SelectedIndexChanged;
        }

        // Set combo box data source etc
        private void setComboBoxDataSource(ComboBox cmb, object src, string displayMember)
        {
            cmb.DataSource = null;
            cmb.ValueMember = null;
            cmb.DataSource = src != null ? new BindingSource(src, null) : null;
            cmb.DisplayMember = displayMember;
            if(cmb.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }

        // Flash size changed, update usage bar
        private void fileFlash_sizeChanged(object sender, EventArgs e)
        {
            if (mcu != null)
                memoryUsageBar(fileFlash, pbFlashUsage, mcu.flash, false);
        }

        // EEPROM size changed, update usage bar
        private void fileEEPROM_sizeChanged(object sender, EventArgs e)
        {
            if (mcu != null)
                memoryUsageBar(fileEEPROM, pbEEPROMUsage, mcu.eeprom, false);
        }
        
        // Click and drag (almost) anywhere to move window
        private void enableClientAreaDrag(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is GroupBox || c is Label || c is PictureBox || c is SplitContainer || c is SplitterPanel)
                {
                    c.MouseDown += Form1_MouseDown;
                    c.MouseUp   += Form1_MouseUp;
                    c.MouseMove += Form1_MouseMove;
                    enableClientAreaDrag(c.Controls);
                }
            }
        }

        private void Avrdude_OnReadFuseLock(object sender, ReadFuseLockEventArgs e)
        {
            // Credits:
            // Simone Chifari (output formatting)
            string fuse = "0x" + e.value.ToUpper().Replace("0X", "").PadLeft(2, '0');

            Invoke(new MethodInvoker(() =>
            {
                switch (e.type)
                {
                    case Avrdude.FuseLockType.Hfuse:
                        txtHFuse.Text = fuse;
                        Util.consoleSuccess("_READHFUSE");
                        break;
                    case Avrdude.FuseLockType.Lfuse:
                        txtLFuse.Text = fuse;
                        Util.consoleSuccess("_READLFUSE");
                        break;
                    case Avrdude.FuseLockType.Efuse:
                        txtEFuse.Text = fuse;
                        Util.consoleSuccess("_READEFUSE");
                        break;
                    case Avrdude.FuseLockType.Fuse:
                        txtLFuse.Text = fuse;
                        Util.consoleSuccess("_READFUSE");
                        break;
                    case Avrdude.FuseLockType.Lock:
                        txtLock.Text = fuse;
                        Util.consoleSuccess("_READLOCKBITS");
                        break;
                    default:
                        Util.consoleError("_FUSELOCKREADFAIL");
                        Util.consoleWriteLine();
                        Util.consoleWriteLine(avrdude.log);
                        break;
                }
            }));
        }
        
        // Draw usage bar and show info in console
        private void memoryUsageBar(MemTypeFile file, PictureBox pic, int availableSpace, bool onlyRedraw)
        {
            bool outOfSpace = file.size > availableSpace;

            // Info
            if (!onlyRedraw && file.size != Avrsize.INVALID && file.location != "")
            {
                float perc = 0;
                if (availableSpace > 0)
                    perc = ((float)file.size / availableSpace) * 100;

                string outOfSpaceStr = "";
                Color colour = Color.White;
                if (outOfSpace)
                {
                    outOfSpaceStr = " [!]";
                    colour = Color.Red;
                }

                Util.consoleWriteLine(
                    "{0}: {1:#,#0} / {2:#,#0} Bytes ({3:0.00}%){4}",
                    colour,
                    Path.GetFileName(file.location),
                    file.size, availableSpace,
                    perc,
                    outOfSpaceStr
                    );
            }

            Bitmap bmp = new Bitmap(pic.Width, pic.Height);

            int usageWidth;
            Color barColour = Color.Red;

            if (outOfSpace)
                usageWidth = bmp.Width;
            else if (availableSpace > 0 && file.size != Avrsize.INVALID)
                usageWidth = (int)(bmp.Width * ((float)file.size / availableSpace));
            else
                usageWidth = 0;

            // Blue gradient thing
            byte startColour = 128;
            byte endColour = 192;
            float colourDiff = (float)(endColour - startColour) / usageWidth;

            // Fill in used space pixels
            for (int i = 0; i < usageWidth; i++)
            {
                if (!outOfSpace)
                    barColour = Color.FromArgb(0x00, startColour + (byte)(colourDiff * i), 0xFF);

                for (byte h = 0; h < bmp.Height; h++)
                    bmp.SetPixel(i, h, barColour);
            }

            // Fill in available space pixels
            for (int i = usageWidth; i < bmp.Width; i++)
            {
                for (byte h = 0; h < bmp.Height; h++)
                    bmp.SetPixel(i, h, Color.Transparent);
            }

            pic.Image = bmp;
        }

        // AVRDUDE process has started
        private void avrdude_OnProcessStart(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                tssStatus.Text = Language.Translation.get("_STATUSRUNNING");
            }));

            // TODO This conflicts with the stuff in the enableControls() method
            //btnProgram.Enabled = false;
            //btnForceStop.Enabled = true;
        }

        // AVRDUDE process has ended
        private void avrdude_OnProcessEnd(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                // ??? Usually there's no problems here, but one time it randomly
                // crashed due to cross-thread access so I've added the Invoke thing.
                tssStatus.Text = Language.Translation.get("_STATUSREADY");
            }));

            /*
            // TODO This conflicts with the stuff in the enableControls() method
            Invoke(new MethodInvoker(() =>
            {
                btnProgram.Enabled = true;
                btnForceStop.Enabled = false;
            }));
            */
        }

        // Found MCU
        private void avrdude_OnDetectedMCU(object sender, DetectedMCUEventArgs e)
        {
            if (e.mcu != null)
            {
                Util.consoleWriteLine("_DETECTSUCCESS", e.mcu.signature, e.mcu.desc);
                Invoke(new MethodInvoker(() =>
                {
                    // Select the MCU that was found
                    cmbMCU.SelectedItem = e.mcu;
                }));
            }
            else
            {
                // Failed to detect MCU, show log so we can see what went wrong
                Util.consoleWarning("_DETECTFAIL");
                Util.consoleWriteLine();
                Util.consoleWriteLine(avrdude.log);
            }
        }

        // Version change
        private void avrdude_OnVersionChange(object sender, EventArgs e)
        {
            setWindowTitle();
        }

        // Disable buttons if a programmer or MCU hasn't been selected
        private void enableControls()
        {
            bool progOK = (prog != null);

            btnDetect.Enabled = progOK;

            bool enable = (mcu != null && progOK);
            btnFuseSelector.Enabled = enable;
            btnWriteFuses.Enabled = enable;
            btnReadFuses.Enabled = enable;
            btnWriteLock.Enabled = enable;
            btnReadLock.Enabled = enable;
            btnProgram.Enabled = enable;
            btnFlashGo.Enabled = enable;
            btnEEPROMGo.Enabled = enable;

            if (mcu != null)
            {
                txtLFuse.Enabled = mcu.memoryTypes.Contains("lfuse") || mcu.memoryTypes.Contains("fuse");
                txtHFuse.Enabled = mcu.memoryTypes.Contains("hfuse");
                txtEFuse.Enabled = mcu.memoryTypes.Contains("efuse");
            }
        }

#region UI Events

        // Drag and drop
        private void event_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        // Drag and drop
        private void event_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (((GroupBox)sender).Name == "gbFlashFile")
                txtFlashFile.Text = files[0];
            else
                txtEEPROMFile.Text = files[0];
        }

        // Port drop down, refresh available ports
        private void cbPort_DropDown(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();

            PlatformID os = Environment.OSVersion.Platform;
            if (os == PlatformID.Unix || os == PlatformID.MacOSX)
            {
                string[] devPrefixs = new string[]
                {
                    "ttyS", // Normal serial port
                    "ttyUSB", // USB <-> serial converter
                    "ttyACM", // USB <-> serial converter (usually an Arduino)
                    "lp" // Parallel port
                };

                // https://stackoverflow.com/questions/434494/serial-port-rs232-in-mono-for-multiple-platforms
                string[] devs;
                try
                {
                    devs = Directory.GetFiles("/dev/", "*", SearchOption.TopDirectoryOnly);
                }
                catch (Exception)
                {
                    return;
                }

                Array.Sort(devs);

                // Loop through each device
                foreach (string dev in devs)
                {
                    // See if device starts with one of the prefixes
                    foreach (string prefix in devPrefixs)
                    {
                        if (dev.StartsWith("/dev/" + prefix))
                        {
                            cmbPort.Items.Add(dev);
                            break;
                        }
                    }
                }
            }
            else // Windows
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string p in ports)
                    cmbPort.Items.Add(p);

                cmbPort.Items.Add("usb");
                cmbPort.Items.Add("lpt1");
                cmbPort.Items.Add("lpt2");
                cmbPort.Items.Add("lpt3");
            }
        }

        // General event for when a control changes
        private void event_controlChanged(object sender, EventArgs e)
        {
            // NOTE: Radio button change doesn't generate an event here
            cmdLine.generate();
            enableControls();
        }

        // Programmer choice changed
        private void cmbProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Credits:
            // Simone Chifari (USBasp frequency stuff)

            // Hide/show USBasp frequency/bit clock boxes

            if (prog != null && prog.id == "usbasp") // USBasp has been selected
            {
                if (txtBitClock.Visible)
                {
                    // Store bit clock
                    oldBitClock = txtBitClock.Text;

                    // Show/hide stuff
                    txtBitClock.Hide();
                    cmbUSBaspFreq.Show();

                    // Make sure a selected index changed event occurs
                    cmbUSBaspFreq.SelectedIndex = -1;

                    // Restore USBasp frequency
                    if (oldUsbAspFreq != null)
                        cmbUSBaspFreq.SelectedItem = oldUsbAspFreq;
                    else
                        cmbUSBaspFreq.SelectedIndex = 0;
                }
            }
            else
            {
                if (!txtBitClock.Visible)
                {
                    // Store selected USBasp frequency
                    oldUsbAspFreq = ((Avrdude.UsbAspFreq)cmbUSBaspFreq.SelectedItem);

                    // Restore bit clock
                    txtBitClock.Text = oldBitClock;

                    // Show/hide stuff
                    txtBitClock.Show();
                    cmbUSBaspFreq.Hide();
                }
            }
        }

        // MCU choice changed
        private void cmbMCU_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update flash and EEPROM size info
            if (mcu != null)
            {
                lblFlashSize.Text = Util.fileSizeFormat(mcu.flash);
                lblEEPROMSize.Text = Util.fileSizeFormat(mcu.eeprom);
                memoryUsageBar(fileFlash, pbFlashUsage, mcu.flash, false);
                memoryUsageBar(fileEEPROM, pbEEPROMUsage, mcu.eeprom, false);
                lblSig.Text = (mcu.signature != null) ? mcu.signature.ToUpper() : "?";
            }
            else
            {
                lblFlashSize.Text = "-";
                lblEEPROMSize.Text = "-";
                lblSig.Text = "-";
            }
        }

        private string getFlashEEPROMOp(Control container)
        {
            foreach (Control control in container.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    if (radio.Name == "rbFlashOpWrite" || radio.Name == "rbEEPROMOpWrite")
                        return FILEOP_WRITE;
                    else if (radio.Name == "rbFlashOpRead" || radio.Name == "rbEEPROMOpRead")
                        return FILEOP_READ;
                    return FILEOP_VERIFY;
                }
            }

            return null;
        }

        // Flash & EEPROM operation radio buttons
        private void radioButton_flashEEPROMOp_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                string op = getFlashEEPROMOp(radioButton.Parent);
                if(op != null)
                {
                    if (radioButton.Parent.Name == "pFlashOp")
                        flashOperation = op;
                    else
                        EEPROMOperation = op;

                    cmdLine.generate();
                }
            }
        }

        // Browse for flash/EEPROM file
        private void btnFlashEEPROMBrowse_Click(object sender, EventArgs e)
        {
            Control fileOpRadioButtons;
            TextBox fileLocation;
            string filter;
            string titleOpen;
            string titleSave;

            if(((Control)sender).Name == "btnFlashBrowse")
            {
                fileOpRadioButtons = pFlashOp;
                fileLocation = txtFlashFile;
                titleOpen = Language.Translation.get("_BROWSE_OPEN_FLASH");
                titleSave = Language.Translation.get("_BROWSE_SAVE_FLASH");
                filter = Language.Translation.get("_BROWSE_FILTER_HEX") + "|*.hex";
                filter += "|" + Language.Translation.get("_BROWSE_FILTER_EEP") + "|*.eep";
            }
            else
            {
                fileOpRadioButtons = pEEPROMOp;
                fileLocation = txtEEPROMFile;
                titleOpen = Language.Translation.get("_BROWSE_OPEN_EEPROM");
                titleSave = Language.Translation.get("_BROWSE_SAVE_EEPROM");
                filter = Language.Translation.get("_BROWSE_FILTER_EEP") + "|*.eep";
                filter += "|" + Language.Translation.get("_BROWSE_FILTER_HEX") + "|*.hex";
            }
            filter += "|" + Language.Translation.get("_BROWSE_FILTER_ALL") + "|*.*";

            string op = getFlashEEPROMOp(fileOpRadioButtons);
            if(op != null)
            {
                if(op == FILEOP_WRITE || op == FILEOP_VERIFY)
                {
                    openFileDialog1.Filter = filter;
                    openFileDialog1.FileName = "";
                    openFileDialog1.Title = titleOpen;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        fileLocation.Text = openFileDialog1.FileName;
                }
                else
                {
                    saveFileDialog1.Filter = filter;
                    saveFileDialog1.FileName = "";
                    saveFileDialog1.Title = titleSave;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        fileLocation.Text = saveFileDialog1.FileName;
                }
            }
        }

        // Options
        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormOptions fOptions = new FormOptions(avrdude.programmers, avrdude.mcus);
            fOptions.toolTips = Config.Prop.toolTips;
            fOptions.usePreviousSettings = Config.Prop.usePreviousSettings;
            fOptions.avrdudeLocation = Config.Prop.avrdudeLoc;
            fOptions.avrdudeConfLocation = Config.Prop.avrdudeConfLoc;
            fOptions.avrSizeLocation = Config.Prop.avrSizeLoc;
            fOptions.language = Config.Prop.language;
            fOptions.hiddenProgrammers = Config.Prop.hiddenProgrammers;
            fOptions.hiddenMCUs = Config.Prop.hiddenMCUs;
            fOptions.checkForUpdates = Config.Prop.checkForUpdates;

            if (fOptions.ShowDialog() == DialogResult.OK)
            {
                bool changedAvrdudeLoc = (Config.Prop.avrdudeLoc != fOptions.avrdudeLocation);
                bool changedAvrdudeConfLoc = (Config.Prop.avrdudeConfLoc != fOptions.avrdudeConfLocation);
                bool changedAvrSizeLoc = (Config.Prop.avrSizeLoc != fOptions.avrSizeLocation);

                Config.Prop.toolTips = fOptions.toolTips;
                Config.Prop.usePreviousSettings = fOptions.usePreviousSettings;
                Config.Prop.avrdudeLoc = fOptions.avrdudeLocation;
                Config.Prop.avrdudeConfLoc = fOptions.avrdudeConfLocation;
                Config.Prop.avrSizeLoc = fOptions.avrSizeLocation;
                Config.Prop.language = fOptions.language;
                Config.Prop.hiddenProgrammers = fOptions.hiddenProgrammers;
                Config.Prop.hiddenMCUs = fOptions.hiddenMCUs;
                Config.Prop.checkForUpdates = fOptions.checkForUpdates;

                ToolTips.Active = Config.Prop.toolTips;

                if (changedAvrdudeLoc || changedAvrdudeConfLoc)
                    avrdude.load();

                updateProgMCUComboBoxes();

                if (changedAvrSizeLoc)
                {
                    avrsize.load();
                    fileFlash.updateSize();
                    fileEEPROM.updateSize();
                }
            }
        }

        // Window size changed, update usage bars (doesn't work when maximized/normalized, see Form1_Resize())
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (mcu != null)
            {
                memoryUsageBar(fileFlash, pbFlashUsage, mcu.flash, true);
                memoryUsageBar(fileEEPROM, pbEEPROMUsage, mcu.eeprom, true);
            }

            Config.Prop.windowSize = Size;
        }

        // Deal with maximized/normalized resizes and update memory usage bars
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState != lastWindowState)
            {
                lastWindowState = WindowState;

                if (WindowState != FormWindowState.Minimized && mcu != null)
                {
                    memoryUsageBar(fileFlash, pbFlashUsage, mcu.flash, true);
                    memoryUsageBar(fileEEPROM, pbEEPROMUsage, mcu.eeprom, true);
                }
            }
        }

        // Only allow digits and delete
        // This isn't perfect, but its close enough
        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = ((TextBox)sender);
            if (e.KeyChar != 8 && (ModifierKeys & Keys.Control) != Keys.Control)
            {
                if ((!char.IsDigit(e.KeyChar) && e.KeyChar != '.') || (txtBox.SelectionLength == 0 && txtBox.Text.Length >= 10))
                {
                    e.Handled = true;
                    SystemSounds.Beep.Play();
                }
            }
        }

        // Only allow hex digits and delete
        // This isn't perfect, but its close enough
        private void txtHex_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = ((TextBox)sender);
            if (e.KeyChar != 8 && (ModifierKeys & Keys.Control) != Keys.Control)
            {
                if ((!char.IsDigit(e.KeyChar) && !"ABCDEFX".Contains(char.ToUpper(e.KeyChar).ToString())) || (txtBox.SelectionLength == 0 && txtBox.Text.Length >= 4))
                {
                    e.Handled = true;
                    SystemSounds.Beep.Play();
                }
            }
        }

        // Program!
        private void btnProgram_Click(object sender, EventArgs e)
        {
            avrdude.launch(txtCmdLine.Text);
        }

        // Force stop
        private void btnForceStop_Click(object sender, EventArgs e)
        {
            if (avrdude.kill())
            {
                Util.consoleWriteLine();
                Util.consoleWriteLine("_AVRDUDEKILLED", Color.Red);
            }
        }

        private PresetData makePresetData(string name)
        {
            PresetData preset = new PresetData(name);
            preset.programmer = (prog != null) ? prog.id : "";
            preset.mcu = (mcu != null) ? mcu.id : "";
            preset.port = port;
            preset.baud = baudRate;
            preset.bitclock = bitClock;
            preset.flashFile = flashFile;
            preset.flashFormat = flashFileFormat;
            preset.flashOp = flashFileOperation;
            preset.EEPROMFile = EEPROMFile;
            preset.EEPROMFormat = EEPROMFileFormat;
            preset.EEPROMOp = EEPROMFileOperation;
            preset.force = force;
            preset.disableVerify = disableVerify;
            preset.disableFlashErase = disableFlashErase;
            preset.eraseFlashAndEEPROM = eraseFlashAndEEPROM;
            preset.doNotWrite = doNotWrite;
            preset.lfuse = lowFuse;
            preset.hfuse = highFuse;
            preset.efuse = exFuse;
            preset.setFuses = setFuses;
            preset.lockBits = lockSetting;
            preset.setLock = setLock;
            preset.additional = additionalSettings;
            preset.verbosity = verbosity;

            return preset;
        }

        private void loadPresetData(PresetData item)
        {
            prog = new Programmer(item.programmer);
            mcu = new MCU(item.mcu);
            port = item.port;
            baudRate = item.baud;
            bitClock = item.bitclock;
            flashFile = item.flashFile;
            flashFileFormat = item.flashFormat;
            flashFileOperation = item.flashOp;
            EEPROMFile = item.EEPROMFile;
            EEPROMFileFormat = item.EEPROMFormat;
            EEPROMFileOperation = item.EEPROMOp;
            force = item.force;
            disableVerify = item.disableVerify;
            disableFlashErase = item.disableFlashErase;
            eraseFlashAndEEPROM = item.eraseFlashAndEEPROM;
            doNotWrite = item.doNotWrite;
            lowFuse = item.lfuse;
            highFuse = item.hfuse;
            exFuse = item.efuse;
            setFuses = item.setFuses;
            lockSetting = item.lockBits;
            setLock = item.setLock;
            additionalSettings = item.additional;
            verbosity = item.verbosity;

            // If preset uses USBasp we need to workout the frequency to use from the bit clock
            if (prog != null && prog.id == "usbasp")
            {
                string bitClockStr = txtBitClock.Text;

                // Make sure an index changed event occurs
                cmbUSBaspFreq.SelectedIndex = -1;

                // Convert bit clock to frequency

                // String to double
                double bitClock;
                if (!double.TryParse(bitClockStr,
                    NumberStyles.Float | NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture, // Bit clock is saved with a '.', we don't want it to try and parse it expecting a ',' or something else
                    out bitClock))
                {
                    cmbUSBaspFreq.SelectedIndex = 0;
                    return;
                }

                int freq = (int)(1 / (bitClock * 0.000001));

                // Make sure frequency is between min and max
                if (freq > Avrdude.USBaspFreqs[0].freq)
                    freq = Avrdude.USBaspFreqs[0].freq;
                else if (freq < Avrdude.USBaspFreqs[Avrdude.USBaspFreqs.Count - 1].freq)
                    freq = Avrdude.USBaspFreqs[Avrdude.USBaspFreqs.Count - 1].freq;

                // Show frequency
                cmbUSBaspFreq.SelectedItem = Avrdude.USBaspFreqs.Find(s => freq >= s.freq - 1);
            }
        }

        // Preset choice changed
        private void cmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            PresetData item = (PresetData)cmbPresets.SelectedItem;
            if (item != null)
                loadPresetData(item);
        }

        // Fuse link clicked
        // Credits:
        // buttim (Load up selected MCU and fuses when opening the fuse calc web page)
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sParam;
            string sURL = WEB_ADDR_FUSE_SETTINGS;

            if (mcu != null)
            {
                sParam = "?P=" + mcu.desc;

                if (mcu.memoryTypes.Contains("fuse") && txtLFuse.Text != "")
                    sParam += "&V_BYTE0=" + txtLFuse.Text;

                if (mcu.memoryTypes.Contains("lfuse") && txtLFuse.Text != "")
                    sParam += "&V_LOW=" + txtLFuse.Text;

                if (mcu.memoryTypes.Contains("hfuse") && txtHFuse.Text != "")
                    sParam += "&V_HIGH=" + txtHFuse.Text;

                if (mcu.memoryTypes.Contains("efuse") && txtEFuse.Text != "")
                    sParam += "&V_EXTENDED=" + txtEFuse.Text;

                sParam += "&O_HEX=Apply+values";
                sURL += sParam;
            }

            Util.openURL(sURL);
        }

        // Read fuses
        private void btnReadFuses_Click(object sender, EventArgs e)
        {
            List<Avrdude.FuseLockType> types = new List<Avrdude.FuseLockType>();
            if(mcu.memoryTypes.Contains("hfuse"))
                types.Add(Avrdude.FuseLockType.Hfuse);
            if (mcu.memoryTypes.Contains("lfuse"))
                types.Add(Avrdude.FuseLockType.Lfuse);
            if (mcu.memoryTypes.Contains("efuse"))
                types.Add(Avrdude.FuseLockType.Efuse);
            if (mcu.memoryTypes.Contains("fuse"))
                types.Add(Avrdude.FuseLockType.Fuse);

            if (types.Count == 0)
                Util.consoleError("_NOSUPPORTEDFUSES", mcu.desc);
            else
            {
                Util.consoleWriteLine("_READINGFUSES");
                string cmd = cmdLine.generateReadFusesLock(types.ToArray());
                avrdude.readFusesLock(cmd, types.ToArray());
            }
        }

        // Read lock byte
        private void btnReadLock_Click(object sender, EventArgs e)
        {
            Util.consoleWriteLine("_READINGLOCKBITS");
            Avrdude.FuseLockType[] types = new Avrdude.FuseLockType[] { Avrdude.FuseLockType.Lock };
            string cmd = cmdLine.generateReadFusesLock(types);
            avrdude.readFusesLock(cmd, types);
        }

        // Only write fuses
        // Credits:
        // Dean (Option to only set fuses)
        private void btnWriteFuses_Click(object sender, EventArgs e)
        {
            Util.consoleWriteLine("_WRITINGINGFUSES");
            string cmd = cmdLine.generateWriteFuses();
            avrdude.launch(cmd);
        }

        // Only write lock
        private void btnWriteLock_Click(object sender, EventArgs e)
        {
            Util.consoleWriteLine("_WRITINGINGLOCKBITS");
            avrdude.launch(cmdLine.generateWriteLock());
        }

        // Only read/write/varify flash
        private void btnFlashGo_Click(object sender, EventArgs e)
        {
            avrdude.launch(cmdLine.generateFlash());
        }

        // Only read/write/varify EEPROM
        private void btnEEPROMGo_Click(object sender, EventArgs e)
        {
            avrdude.launch(cmdLine.generateEEPROM());
        }

        // Detect MCU
        // Credits:
        // Simone Chifari (Auto detect MCU)
        private void btnDetect_Click(object sender, EventArgs e)
        {
            avrdude.detectMCU(cmdLine.genReadSig());
        }

        // Open fuse selector window
        // Credits:
        // Simone Chifari (Fuse selector)
        private void btnFuseSelector_Click(object sender, EventArgs e)
        {
            // Make sure MCU is valid
            if (mcu == null)
                return;

            // Get fuse values
            string[] fuses = { txtLFuse.Text, txtHFuse.Text, txtEFuse.Text, txtLock.Text };

            // Remove 0x
            for (int i = 0; i < fuses.Length; i++)
                fuses[i] = fuses[i].ToLower().Replace("0x", "");

            // Open fuse selector form
            FormFuseSelector f = new FormFuseSelector();
            string[] newFuses = f.editFuseAndLocks(mcu, fuses);

            if (newFuses != null)
            {
                // Add 0x back on
                for (int i = 0; i < newFuses.Length; i++)
                    newFuses[i] = "0x" + newFuses[i];

                // Set fuse values
                txtLFuse.Text = newFuses[0];
                txtHFuse.Text = newFuses[1];
                txtEFuse.Text = newFuses[2];
                txtLock.Text = newFuses[3];
            }
        }

        // Workout bit clock for USBasp programmer frequency
        private void cmbUSBaspFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            Avrdude.UsbAspFreq freq = ((Avrdude.UsbAspFreq)((ComboBox)sender).SelectedItem);
            if (cmbUSBaspFreq.Visible && freq != null)
                txtBitClock.Text = freq.bitClock;
        }

        // About
        private void btnAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        // Drag client area
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;

            Point screenPos = PointToScreen(new Point(0, 0));

            dragStart = new Point(e.X + (screenPos.X - Location.X), e.Y + (screenPos.Y - Location.Y));

            Control c = (Control)sender;
            while (c is GroupBox || c is Label || c is PictureBox || c is SplitContainer || c is SplitterPanel)
            {
                dragStart.X += c.Location.X;
                dragStart.Y += c.Location.Y;
                c = c.Parent;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
                Location = new Point(Cursor.Position.X - dragStart.X, Cursor.Position.Y - dragStart.Y);
        }

        // CTRL + A to select all
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        // Tool item select all
        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            rtxtConsole.SelectAll();
        }

        // Tool item copy
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            if (rtxtConsole.SelectionLength > 0)
                Clipboard.SetText(rtxtConsole.SelectedText);
        }

        // Tool item clear
        private void tsmiClear_Click(object sender, EventArgs e)
        {
            Util.consoleClear();
        }

        // Save configuration when closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Prop.previousSettings = Config.Prop.usePreviousSettings ? makePresetData("") : new PresetData();
            Config.Prop.save();
        }

        // Open the presets manager form
        private void btnPresetMgr_Click(object sender, EventArgs e)
        {
            using (FormPresetManager fPresetMgr = new FormPresetManager())
            {
                fPresetMgr.presets = presets;
                fPresetMgr.currentSettings = makePresetData("");
                fPresetMgr.ShowDialog();
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            // Persist window location across sessions
            // Credits:
            // gl.tter
            if (WindowState == FormWindowState.Normal)
                Config.Prop.windowLocation = Location;
        }

        #endregion
    }
}
