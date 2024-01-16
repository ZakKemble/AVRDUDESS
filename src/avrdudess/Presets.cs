// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace avrdudess
{
    public class Presets
    {
        private const string FILE_PRESETS = "presets.xml";
        private readonly XmlFile<BindingList<PresetData>> xmlFile;
        private BindingList<PresetData> presetList = new BindingList<PresetData>();
        private readonly bool isImport = false;

        public List<PresetData> Items
        {
            get => new List<PresetData>(presetList);
        }

        public Presets()
        {
            xmlFile = new XmlFile<BindingList<PresetData>>(FILE_PRESETS);
        }

        public Presets(string file)
        {
            xmlFile = new XmlFile<BindingList<PresetData>>(file, true);
            isImport = true;
        }

        public void SetDataSource(ComboBox cb)
        {
            // Create new instances of BindingSource here instead of using a single class property otherwise
            // all the preset combo boxes (on Form1 and FormPresetManager) will change selection with each other
            BindingSource bSource = new BindingSource();
            bSource.DataSource = presetList;

            cb.DataSource = null;
            cb.ValueMember = null;
            cb.BindingContext = new BindingContext();
            cb.DataSource = bSource;
            cb.DisplayMember = "name";
            cb.SelectedIndex = -1;
        }

        public void Add(PresetData preset)
        {
            presetList.Add(preset);
            BumpDefault();
        }

        public void Remove(PresetData preset)
        {
            presetList.Remove(preset);
            BumpDefault();
        }

        // Make sure default is at the top
        private void BumpDefault()
        {
            // I'm assuming the order of the new List is same as the BindingList...

            int idx = new List<PresetData>(presetList).FindIndex(s => s.name == "Default");
            if (idx > 0)
            {
                PresetData p = presetList[idx];
                presetList.RemoveAt(idx);
                presetList.Insert(0, p);
            }
        }

        public void Save()
        {
            try
            {
                xmlFile.Write(presetList);
            }
            catch (Exception ex)
            {
                MsgBox.error("_XMLWRITEERROR", "presets", ex.Message);
            }
        }

        public void Load()
        {
            try
            {
                presetList = xmlFile.Read();
            }
            catch (Exception ex)
            {
                if (isImport || ex.GetType() != typeof(FileNotFoundException))
                    MsgBox.error($"An error occurred trying to load presets:{Environment.NewLine}{ex.Message}"); // TODO translate
            }

            if (presetList == null)
            {
                presetList = new BindingList<PresetData>();
                if(!isImport)
                    Add(new PresetData("Default"));
            }
        }
    }

    [XmlType(TypeName = "Preset")] // For backwards compatability with old (<v2.0) presets.xml
    public class PresetData : INotifyPropertyChanged
    {
        // This notify property changed stuff is so that the preset dropdown boxes
        // automatically update with the new name when renaming a preset
        public event PropertyChangedEventHandler PropertyChanged;

        // [CallerMemberName] attribute doesn't exist in .NET 2.0
        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;

        public string name {
            get { return _name; }
            set {
                _name = value;
                NotifyPropertyChanged("name");
            }
        }

        public string programmer = "";
        public string mcu = "";
        public string port;
        public string baud;
        public string bitclock;
        public string flashFile;
        public string flashFormat = "a";
        public string flashOp = Form1.FILEOP_WRITE;
        public string EEPROMFile;
        public string EEPROMFormat = "a";
        public string EEPROMOp = Form1.FILEOP_WRITE;
        public bool force;
        public bool disableVerify;
        public bool disableFlashErase;
        public bool eraseFlashAndEEPROM;
        public bool doNotWrite;
        public string lfuse;
        public string hfuse;
        public string efuse;
        public bool setFuses;
        public string lockBits;
        public bool setLock;
        public string additional;
        public byte verbosity;

        public PresetData()
        {
        }

        public PresetData(string name)
        {
            this.name = name;
        }

        public PresetData(PresetData source)
        {
            copyFrom(source);
        }

        public void copyFrom(PresetData source)
        {
            name = source.name;

            programmer = source.programmer;
            mcu = source.mcu;
            port = source.port;
            baud = source.baud;
            bitclock = source.bitclock;
            flashFile = source.flashFile;
            flashFormat = source.flashFormat;
            flashOp = source.flashOp;
            EEPROMFile = source.EEPROMFile;
            EEPROMFormat = source.EEPROMFormat;
            EEPROMOp = source.EEPROMOp;
            force = source.force;
            disableVerify = source.disableVerify;
            disableFlashErase = source.disableFlashErase;
            eraseFlashAndEEPROM = source.eraseFlashAndEEPROM;
            doNotWrite = source.doNotWrite;
            lfuse = source.lfuse;
            hfuse = source.hfuse;
            efuse = source.efuse;
            setFuses = source.setFuses;
            lockBits = source.lockBits;
            setLock = source.setLock;
            additional = source.additional;
            verbosity = source.verbosity;
        }
    }
}
