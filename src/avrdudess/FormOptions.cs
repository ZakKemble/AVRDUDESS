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

        public HashSetD<string> hiddenProgrammers
        {
            get { return getHiddenCheckBoxes(clbHiddenProgrammers); }
            set { setHiddenCheckBoxes(value, clbHiddenProgrammers); }
        }

        public HashSetD<string> hiddenMCUs
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

            Dictionary<string, string> langs = Language.Translation.Languages;
            cbLanguage.Items.Clear();
            if (langs.Count > 0)
            {
                cbLanguage.DataSource = new BindingSource(langs, null);
                cbLanguage.DisplayMember = "Value";
                cbLanguage.ValueMember = "Key";
            }

            clbHiddenProgrammers.Items.Clear();
            clbHiddenProgrammers.DataSource = new BindingSource(programmers, null);
            clbHiddenProgrammers.Format += (object _, ListControlConvertEventArgs e) =>
            {
                Part part = (Part)e.ListItem;
                e.Value = $"{part.id} -- ({part.desc})";
            };

            clbHiddenMCUs.Items.Clear();
            clbHiddenMCUs.DataSource = new BindingSource(mcus, null);
            clbHiddenMCUs.Format += (object _, ListControlConvertEventArgs e) =>
            {
                Part part = (Part)e.ListItem;
                e.Value = $"{part.desc} ({part.id})";
            };
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            Language.Translation.Apply(this);

            // If less than half of checkboxes are checked then the first button click should check all, otherwise uncheck all
            checkAllProg = clbHiddenProgrammers.CheckedItems.Count < (clbHiddenProgrammers.Items.Count / 2);
            checkAllMCU = clbHiddenMCUs.CheckedItems.Count < (clbHiddenMCUs.Items.Count / 2);
        }

        private void setHiddenCheckBoxes(HashSetD<string> hiddenParts, CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
                clb.SetItemChecked(i, hiddenParts.Contains(((Part)clb.Items[i]).id));
        }

        private HashSetD<string> getHiddenCheckBoxes(CheckedListBox clb)
        {
            HashSetD<string> hiddenParts = new HashSetD<string>();
            foreach (Part item in clb.CheckedItems)
                hiddenParts.Add(item.id);
            return hiddenParts;
        }

        private void btnBrowseAvrdude_Click(object sender, EventArgs e)
        {
            var filter = Util.isWindows() ? "Executable (*.exe)|*.exe|" + Language.Translation.get("_BROWSE_FILTER_ALL") + "|*.*" : "";
            browse("_AVRDUDE_LOCATION", filter, txtAvrdudeLocation);
        }

        private void btnBrowseAvrdudeConf_Click(object sender, EventArgs e)
        {
            var filter = "conf (*.conf)|*.conf|" + Language.Translation.get("_BROWSE_FILTER_ALL")  + "|*.*";
            browse("_AVRDUDECONF_LOCATION", filter, txtAvrdudeConfLocation);
        }

        private void btnBrowseAvrSize_Click(object sender, EventArgs e)
        {
            var filter = Util.isWindows() ? "Executable (*.exe)|*.exe|" + Language.Translation.get("_BROWSE_FILTER_ALL")  + "|*.*" : "";
            browse("_AVRSIZE_LOCATION", filter, txtAvrSizeLocation);
        }

        private void browse(string title, string filter, TextBox txt)
        {
            openFileDialog1.Title = Language.Translation.get(title);
            openFileDialog1.Filter = filter;
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
