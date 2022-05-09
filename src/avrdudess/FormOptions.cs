/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2014 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
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
            cbLanguage.DataSource = new BindingSource(langs, null);
            cbLanguage.DisplayMember = "Value";
            cbLanguage.ValueMember = "Key";

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

            int checkedCount = 0;
            for (int i = 0; i < clbHiddenProgrammers.Items.Count; i++)
            {
                if (clbHiddenProgrammers.GetItemChecked(i))
                    checkedCount++;
            }
            checkAllProg = (checkedCount < (clbHiddenProgrammers.Items.Count / 2));

            checkedCount = 0;
            for (int i = 0; i < clbHiddenMCUs.Items.Count; i++)
            {
                if (clbHiddenMCUs.GetItemChecked(i))
                    checkedCount++;
            }
            checkAllMCU = (checkedCount < (clbHiddenMCUs.Items.Count / 2));
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
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if (clb.GetItemChecked(i))
                    hiddenParts.Add(((Part)clb.Items[i]).id);
            }
            return hiddenParts;
        }

        private void hiddenParts_Format(object sender, ListControlConvertEventArgs e)
        {
            Part part = (Part)e.ListItem;
            e.Value = string.Format("{0} ({1})", part.desc, part.id);
        }

        private void btnBrowseAvrdude_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRDUDE_LOCATION");
            browse(txtAvrdudeLocation);
        }

        private void btnBrowseAvrdudeConf_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRDUDECONF_LOCATION");
            browse(txtAvrdudeConfLocation);
        }

        private void btnBrowseAvrSize_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = Language.Translation.get("_AVRSIZE_LOCATION");
            browse(txtAvrSizeLocation);
        }

        private void browse(TextBox txt)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txt.Text = folderBrowserDialog1.SelectedPath;
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
