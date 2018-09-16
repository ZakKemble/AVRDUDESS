/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2018 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

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
            clbExport.Items.Clear();
            foreach (PresetData preset in presets.presets)
                clbExport.Items.Add(preset.name);
        }

        private void FormPresetManager_Load(object sender, EventArgs e)
        {
            presets.setDataSource(cmbPresets);
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

            Language.Translation.ApplyTranslation(this);
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
                PresetData existingPreset = presets.presets.Find(s => s.name == name);
                if (existingPreset != null)
                {
                    if (MsgBox.confirm("_OVERWRITEPRESET", name) != DialogResult.OK)
                        continue;

                    presets.remove(existingPreset);
                }

                break;
            }

            PresetData newPreset = new PresetData(currentSettings);
            newPreset.name = name;

            // Add new preset
            presets.add(newPreset);
            presets.save();

            // Select the new preset
            PresetData p = presets.presets.Find(s => s.name == name);
            if (p != null)
                cmbPresets.SelectedItem = p;

            reloadExportList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<PresetData> toDelete = new List<PresetData>();
            for (int i = 0; i < clbExport.Items.Count; i++)
            {
                if (clbExport.GetItemChecked(i))
                {
                    string name = (string)clbExport.Items[i];
                    if (name != "Default")
                    {
                        PresetData p = presets.presets.Find(s => s.name == name);
                        if (p != null)
                            toDelete.Add(p);
                    }
                }
            }

            if(toDelete.Count > 0)
            {
                if (MsgBox.confirm("_DELETEPRESETS", toDelete.Count) == DialogResult.OK)
                {
                    foreach(PresetData p in toDelete)
                        presets.remove(p);
                    presets.save();
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
                    p = presets.presets.Find(s => s.name == name);
                    if (p != null)
                    {
                        if (MsgBox.confirm("_PRESETALREADYEXISTS") != DialogResult.OK)
                            continue;

                        presets.remove(p);
                    }

                    break;
                }

                selectedPreset.name = name;
                presets.save();

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
                    presets.save();

                    // Select the new preset
                    PresetData p = presets.presets.Find(s => s.name == selectedPreset.name);
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
                Presets export = new Presets(saveFileDialog1.FileName, true);

                for (int i = 0; i < clbExport.Items.Count; i++)
                {
                    if (clbExport.GetItemChecked(i))
                    {
                        string name = (string)clbExport.Items[i];
                        PresetData p = presets.presets.Find(s => s.name == name);
                        if (p != null)
                        {
                            export.add(p);
                            Util.consoleWriteLine("_EXPORTINGPRESETS", p.name);
                        }
                    }
                }

                export.save();

                Util.consoleWriteLine("_EXPORTCOMPLETE");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Presets import = new Presets(openFileDialog1.FileName, true);
                import.load();

                foreach(PresetData newPreset in import.presets)
                {
                    Util.consoleWriteLine("_IMPORTINGPRESETS", newPreset.name);

                    // If the imported preset name already exists then add the time in hex to the name
                    if (presets.presets.Find(s => s.name == newPreset.name) != null)
                    {
                        string oldName = newPreset.name;
                        newPreset.name = string.Format("{0} {1:X16}", newPreset.name, DateTime.UtcNow.Ticks);
                        Util.consoleWarning("_IMPORTALREADYEXISTS", oldName, newPreset.name);
                    }
                    
                    presets.add(newPreset);
                }

                presets.save();
                reloadExportList();

                Util.consoleWriteLine("_IMPORTCOMPLETE");
            }
        }
    }
}
