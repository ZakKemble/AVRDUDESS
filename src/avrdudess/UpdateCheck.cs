// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Serialization;

namespace avrdudess
{
    enum UpdateCheckState
    {
        Delay,
        Begin,
        Failed,
        Success
    }

    class UpdateCheckEventArgs : EventArgs
    {
        public UpdateCheckState State { get; set; }

        public UpdateCheckEventArgs(UpdateCheckState state)
        {
            State = state;
        }
    }


    [XmlRoot("version")]
    public class UpdateData
    {
        public int major;
        public int minor;
        public long date;
        public string updateAddr = "";

        [XmlArray("releases")]
        [XmlArrayItem("release")]
        public List<UpdateReleaseEntry> releases = new List<UpdateReleaseEntry>();

        [XmlIgnore]
        public Version currentVersion;

        [XmlIgnore]
        public UpdateReleaseEntry Latest
        {
            get
            {
                if (releases == null)
                    return null;

                UpdateReleaseEntry lastestRelease = null;

                releases.ForEach(release => 
                {
                    if (lastestRelease == null || release.Version.CompareTo(lastestRelease.Version) > 0)
                        lastestRelease = release;
                });

                return lastestRelease; // This will be null if releases list is empty
            }
        }

        [XmlIgnore]
        public bool UpdateAvailable
        {
            get
            {
                UpdateReleaseEntry latestRelease = Latest;
                return (latestRelease != null && latestRelease.Version.CompareTo(currentVersion) > 0);
            }
        }

        public UpdateData()
        {
#if DEBUG
            currentVersion = new Version(0, 1);
#else
            currentVersion = new Version(AssemblyData.version.Major, AssemblyData.version.Minor);
#endif
        }
    }

    public class UpdateReleaseEntry
    {
        public int major;
        public int minor;
        public long date;
        public string info;

        [XmlIgnore]
        public DateTime Date
        {
            get => new DateTime(1970, 1, 1).AddSeconds(date);
        }

        [XmlIgnore]
        public Version Version
        {
            get => new Version(major, minor);
        }
    }

    class UpdateCheck
    {
        private const string UPDATE_ADDR = "https://versions.zakkemble.net/avrdudess2.xml";

        public Exception Ex { get; private set; }
        public UpdateData UpdateData = new UpdateData();

        private UpdateCheckState _state;
        public UpdateCheckState State
        {
            get => _state;
            private set
            {
                _state = value;
                OnUpdateCheck?.Invoke(this, new UpdateCheckEventArgs(value));
            }
        }

        public event EventHandler<UpdateCheckEventArgs> OnUpdateCheck;

        public UpdateCheck() { }

        public void Run()
        {
            if (!Needed() || !Config.Prop.checkForUpdates)
                return;

            State = UpdateCheckState.Delay;
            
            Thread t = new Thread(() =>
            {
                Thread.Sleep(5000);
                State = UpdateCheckState.Begin;
                try
                {
                    Now();
                    Config.Prop.updateCheck = Util.UnixTimeStamp();
                    State = UpdateCheckState.Success;
                }
                catch (Exception ex)
                {
                    Ex = ex;
                    State = UpdateCheckState.Failed;
                }
            });
            t.IsBackground = true;
            t.Start();
        }

        private bool Needed()
        {
#if DEBUG
            return true;
#else
            // Check once a day
            return Util.UnixTimeStamp() - Config.Prop.updateCheck > TimeSpan.FromDays(1).TotalSeconds;
#endif
        }

        private void Now()
        {
            UpdateData.releases = new List<UpdateReleaseEntry>();

            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)(3072 | 12288); // TLSv1.2 (Win7+) | TLSv1.3 (Win11+)

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UPDATE_ADDR);
            request.UserAgent = $"Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER {AssemblyData.version})";
            request.ReadWriteTimeout = 30000;
            request.Timeout = 30000;
            request.KeepAlive = false;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#if DEBUG // Getting proxy info is slow, so don't use proxy in debug
            request.Proxy = null;
#endif

            using (Stream responseStream = request.GetResponse().GetResponseStream())
                UpdateData = (UpdateData)new XmlSerializer(typeof(UpdateData)).Deserialize(responseStream);

            if(UpdateData.releases.Count == 0)
                throw new Exception(Language.Translation.get("_UPDATE_BADXML"));
        }
    }
}
