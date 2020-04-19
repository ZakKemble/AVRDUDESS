/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace avrdudess
{
    public class Presets : XmlFile<List<PresetData>>
    {
        private const string FILE_PRESETS = "presets.xml";

        private BindingList<PresetData> presetList;
        private bool customFileLocation;

        protected override object data
        {
            get { return presetList; }
            set
            {
                if (value != null)
                    presetList = new BindingList<PresetData>((List<PresetData>)value);
            }
        }
        
        // TODO This should return a readonly list... ???
        public List<PresetData> presets
        {
            get { return new List<PresetData>(presetList); }
        }

        public Presets(string xmlFile = FILE_PRESETS)
            : base(xmlFile, "presets", false)
        {
            presetList = new BindingList<PresetData>();
        }

        public Presets(string xmlFile, bool customFileLocation)
            : base(xmlFile, "presets", customFileLocation)
        {
            this.customFileLocation = customFileLocation;
            presetList = new BindingList<PresetData>();
        }

        public void setDataSource(ComboBox cb)
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

        // New preset
        public void add(PresetData preset)
        {
            presetList.Add(preset);
            bumpDefault();
        }

        // Delete preset
        public void remove(PresetData preset)
        {
            presetList.Remove(preset);
            bumpDefault();
        }

        // Make sure default is at the top
        private void bumpDefault()
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

        // Save presets
        public void save()
        {
            write();
        }

        // Load presets
        public void load()
        {
            if (!customFileLocation)
            {
                // If file doesn't exist then make it
                if (!File.Exists(fileLocation))
                {
                    add(new PresetData("Default"));
                    save();
                }
            }

            // Load presets from XML
            read();
            if (presetList == null) // Failed to load
                presetList = new BindingList<PresetData>();
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
        public bool enableMCUAutoDetect = true;

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
            enableMCUAutoDetect = source.enableMCUAutoDetect;
        }
    }
}
