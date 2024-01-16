// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2018-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace avrdudess
{
    public partial class FormPresetManager : Form
    {
        public PresetData currentSettings;
        public Presets presets;

        public FormPresetManager()
        {
            InitializeComponent();
            Icon = AssemblyData.icon;
        }

        private void reloadExportList()
        {
            // TODO presets.presets should be a BindingList<T>, but at the moment its a List<T> so we have to manually refresh the data
            clbExport.DataSource = null;
            clbExport.DataSource = new BindingSource(presets.Items, null);
            clbExport.DisplayMember = "name";
        }

        private void FormPresetManager_Load(object sender, EventArgs e)
        {
            presets.SetDataSource(cmbPresets);
            reloadExportList();
 
            // Export
            saveFileDialog1.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog1.Filter += "|All files (*.*)|*.*";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.FileName = "presets.xml";
            saveFileDialog1.Title = Language.Translation.get("_SAVEDIALOG_EXPORT");

            // Import
            openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            openFileDialog1.Filter += "|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.Title = Language.Translation.get("_OPENDIALOG_IMPORT");

            Language.Translation.apply(this);
        }

        private string performTextInputDialog(string title, string prefillText)
        {
            string text = null;

            using (FormEnterText fEnterText = new FormEnterText(title, prefillText))
            {
                DialogResult res = fEnterText.ShowDialog();
                if (res == DialogResult.OK)
                    text = fEnterText.inputText;
            }

            return text;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string name;

            while (true)
            {
                // Check name
                name = performTextInputDialog("_NEWPRESETNAME", "");
                if (name == null)
                    return;
                else if (name.Length < 1)
                    continue;
                else if (name == "Default")
                {
                    MsgBox.notice("_CANTUSEDEFAULT");
                    continue;
                }

                // Check for existing presets with the same name
                PresetData existingPreset = presets.Items.Find(s => s.name == name);
                if (existingPreset != null)
                {
                    if (MsgBox.confirm("_OVERWRITEPRESET", name) != DialogResult.OK)
                        continue;

                    presets.Remove(existingPreset);
                }

                break;
            }

            PresetData newPreset = new PresetData(currentSettings);
            newPreset.name = name;

            // Add new preset
            presets.Add(newPreset);
            presets.Save();

            // Select the new preset
            PresetData p = presets.Items.Find(s => s.name == name);
            if (p != null)
                cmbPresets.SelectedItem = p;

            reloadExportList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<PresetData> toDelete = new List<PresetData>();
            foreach (PresetData item in clbExport.CheckedItems)
            {
                if (item.name != "Default")
                    toDelete.Add(item);
            }

            if (toDelete.Count > 0)
            {
                if (MsgBox.confirm("_DELETEPRESETS", toDelete.Count) == DialogResult.OK)
                {
                    foreach(PresetData p in toDelete)
                        presets.Remove(p);
                    presets.Save();
                    reloadExportList();
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            PresetData selectedPreset = (PresetData)cmbPresets.SelectedItem;
            if (selectedPreset != null)
            {
                string name;
                PresetData p;

                while (true)
                {
                    // Check name
                    name = performTextInputDialog("_NEWPRESETNAME", selectedPreset.name);
                    if (name == null)
                        return;
                    else if (name == ((PresetData)cmbPresets.SelectedItem).name)
                        return;
                    else if (name.Length < 1)
                        continue;
                    else if (name == "Default")
                    {
                        MsgBox.notice("_CANTUSEDEFAULT");
                        continue;
                    }

                    // Check for existing presets with the same name
                    p = presets.Items.Find(s => s.name == name);
                    if (p != null)
                    {
                        if (MsgBox.confirm("_PRESETALREADYEXISTS") != DialogResult.OK)
                            continue;

                        presets.Remove(p);
                    }

                    break;
                }

                selectedPreset.name = name;
                presets.Save();

                reloadExportList();
            }
        }

        private void btnOverwrite_Click(object sender, EventArgs e)
        {
            PresetData selectedPreset = (PresetData)cmbPresets.SelectedItem;

            // Make sure a preset is selected
            if (selectedPreset != null)
            {
                // Make sure its not the default preset
                if (selectedPreset.name == "Default")
                    MsgBox.notice("_CANTOVERWRITEDEFAULT");
                else if (MsgBox.confirm("_PRESETOVERWRITE", selectedPreset.name) == DialogResult.OK)
                {
                    string name = selectedPreset.name;
                    selectedPreset.copyFrom(currentSettings);
                    selectedPreset.name = name;
                    presets.Save();

                    // Select the new preset
                    PresetData p = presets.Items.Find(s => s.name == selectedPreset.name);
                    if (p != null)
                        cmbPresets.SelectedItem = p;

                    reloadExportList();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Presets export = new Presets(saveFileDialog1.FileName);

                foreach (PresetData item in clbExport.CheckedItems)
                {
                    if (item != null)
                    {
                        export.Add(item);
                        Util.consoleWriteLine("_EXPORTINGPRESETS", item.name);
                    }
                }

                export.Save();

                Util.consoleWriteLine("_EXPORTCOMPLETE");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Presets import = new Presets(openFileDialog1.FileName);
                import.Load();

                foreach(PresetData newPreset in import.Items)
                {
                    Util.consoleWriteLine("_IMPORTINGPRESETS", newPreset.name);

                    // If the imported preset name already exists then add the time in hex to the name
                    if (presets.Items.Find(s => s.name == newPreset.name) != null)
                    {
                        string oldName = newPreset.name;
                        newPreset.name = $"{newPreset.name} {DateTime.UtcNow.Ticks:X16}";
                        Util.consoleWarning("_IMPORTALREADYEXISTS", oldName, newPreset.name);
                    }
                    
                    presets.Add(newPreset);
                }

                presets.Save();
                reloadExportList();

                Util.consoleWriteLine("_IMPORTCOMPLETE");
            }
        }
    }
}
