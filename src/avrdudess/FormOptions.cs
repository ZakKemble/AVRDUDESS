// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2014-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormOptions : Form
    {
        private bool checkAllProg;
        private bool checkAllMCU;

        #region Control getters and setters

        public bool toolTips
        {
            get { return cbShowToolTips.Checked; }
            set { cbShowToolTips.Checked = value; }
        }

        public bool usePreviousSettings
        {
            get { return cbUsePrevSettings.Checked; }
            set { cbUsePrevSettings.Checked = value; }
        }

        public string avrdudeLocation
        {
            get { return txtAvrdudeLocation.Text; }
            set { txtAvrdudeLocation.Text = value; }
        }

        public string avrdudeConfLocation
        {
            get { return txtAvrdudeConfLocation.Text; }
            set { txtAvrdudeConfLocation.Text = value; }
        }

        public string avrSizeLocation
        {
            get { return txtAvrSizeLocation.Text; }
            set { txtAvrSizeLocation.Text = value; }
        }

        public string language
        {
            get { return (string)cbLanguage.SelectedValue; }
            set { cbLanguage.SelectedValue = value; }
        }

        public List<string> hiddenProgrammers
        {
            get { return getHiddenCheckBoxes(clbHiddenProgrammers); }
            set { setHiddenCheckBoxes(value, clbHiddenProgrammers); }
        }

        public List<string> hiddenMCUs
        {
            get { return getHiddenCheckBoxes(clbHiddenMCUs); }
            set { setHiddenCheckBoxes(value, clbHiddenMCUs); }
        }

        public bool checkForUpdates
        {
            get { return cbCheckForUpdate.Checked; }
            set { cbCheckForUpdate.Checked = value; }
        }

        #endregion

        public FormOptions(List<Programmer> programmers, List<MCU> mcus)
        {
            InitializeComponent();

            Icon = AssemblyData.icon;

            Dictionary<string, string> langs = Language.Translation.getLanguages();
            cbLanguage.Items.Clear();
            if (langs.Count > 0)
            {
                cbLanguage.DataSource = new BindingSource(langs, null);
                cbLanguage.DisplayMember = "Value";
                cbLanguage.ValueMember = "Key";
            }

            clbHiddenProgrammers.Items.Clear();
            clbHiddenProgrammers.DataSource = new BindingSource(programmers, null);
            clbHiddenProgrammers.Format += hiddenParts_Format;

            clbHiddenMCUs.Items.Clear();
            clbHiddenMCUs.DataSource = new BindingSource(mcus, null);
            clbHiddenMCUs.Format += hiddenParts_Format;
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            Language.Translation.apply(this);

            // If less than half of checkboxes are checked then the first button click should check all, otherwise uncheck all
            checkAllProg = clbHiddenProgrammers.CheckedItems.Count < (clbHiddenProgrammers.Items.Count / 2);
            checkAllMCU = clbHiddenMCUs.CheckedItems.Count < (clbHiddenMCUs.Items.Count / 2);
        }

        private void setHiddenCheckBoxes(List<string> hiddenParts, CheckedListBox clb)
        {
            foreach (string hidden in hiddenParts)
            {
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    if (((Part)clb.Items[i]).id == hidden)
                    {
                        clb.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private List<string> getHiddenCheckBoxes(CheckedListBox clb)
        {
            List<string> hiddenParts = new List<string>();
            foreach (var item in clb.CheckedItems)
                hiddenParts.Add(((Part)item).id);
            return hiddenParts;
        }

        private void hiddenParts_Format(object sender, ListControlConvertEventArgs e)
        {
            Part part = (Part)e.ListItem;
            e.Value = $"{part.desc} ({part.id})";
        }

        private void btnBrowseAvrdude_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = Language.Translation.get("avrdude location"); // TODO translate
            browse(txtAvrdudeLocation);
        }

        private void btnBrowseAvrdudeConf_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = Language.Translation.get("avrdude config location"); // TODO translate
            browse(txtAvrdudeConfLocation);
        }

        private void btnBrowseAvrSize_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = Language.Translation.get("avr-size location"); // TODO translate
            browse(txtAvrSizeLocation);
        }

        private void browse(TextBox txt)
        {
            openFileDialog1.FileName = Path.GetFileName(txt.Text);
            try
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(txt.Text);
            }
            catch (Exception) { }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txt.Text = openFileDialog1.FileName;
        }

        private void btnCheckUncheckProgs_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbHiddenProgrammers.Items.Count; i++)
                clbHiddenProgrammers.SetItemChecked(i, checkAllProg);
            checkAllProg = !checkAllProg;
        }

        private void btnCheckUncheckMCU_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbHiddenMCUs.Items.Count; i++)
                clbHiddenMCUs.SetItemChecked(i, checkAllMCU);
            checkAllMCU = !checkAllMCU;
        }
    }
}
