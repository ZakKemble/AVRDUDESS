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
using System.Xml;

namespace avrdudess
{
    public enum UpdateCheckState
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

    public class UpdateData
    {
        public Version currentVersion;
        public string updateAddr = "";
        public List<UpdateReleaseData> releases;

        public UpdateReleaseData Latest
        {
            get
            {
                if (releases == null)
                    return null;

                UpdateReleaseData lastestRelease = null;

                foreach (UpdateReleaseData release in releases)
                {
                    if (lastestRelease == null || release.Version.CompareTo(lastestRelease.Version) > 0)
                        lastestRelease = release;
                }

                return lastestRelease; // This will be null if releases list is empty
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

        public bool UpdateAvailable()
        {
            UpdateReleaseData latestRelease = Latest;
            return (latestRelease != null && latestRelease.Version.CompareTo(currentVersion) > 0);
        }
    }

    public class UpdateReleaseData
    {
        public int major;
        public int minor;
        public long dateUnix;
        public string info;

        public DateTime Date
        {
            get => new DateTime(1970, 1, 1).AddSeconds(dateUnix);
        }

        public Version Version
        {
            get => new Version(major, minor);
        }
    }

    sealed class UpdateCheck
    {
        private static readonly string UPDATE_ADDR = "https://versions.zakkemble.net/avrdudess2.xml";

        public Exception Ex { get; private set; }
        public readonly UpdateData UpdateData = new UpdateData();

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
            UpdateData.releases = new List<UpdateReleaseData>();

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

            UpdateReleaseData release = null;

            // Do request
            using (Stream responseStream = request.GetResponse().GetResponseStream())
            {
                // XML
                using (XmlReader reader = XmlReader.Create(responseStream))
                {
                    while (reader.Read())
                    {
                        string name = reader.Name;

                        if(name == "release")
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                                release = new UpdateReleaseData();
                            else if(reader.NodeType == XmlNodeType.EndElement && release != null)
                            {
                                UpdateData.releases.Add(release);
                                release = null;
                            }
                        }
                        else if(reader.NodeType == XmlNodeType.Element)
                        {
                            reader.Read();
                            if(release == null)
                            {
                                if(name == "updateAddr")
                                    UpdateData.updateAddr = reader.ReadContentAsString();
                            }
                            else if(name == "major")
                                release.major = reader.ReadContentAsInt();
                            else if(name == "minor")
                                release.minor = reader.ReadContentAsInt();
                            else if(name == "date")
                                release.dateUnix = reader.ReadContentAsLong();
                            else if(name == "info")
                                release.info = reader.ReadContentAsString();
                        }
                    }
                }
            }

            if(UpdateData.releases.Count == 0)
                throw new Exception(Language.Translation.get("_UPDATE_BADXML"));
        }
    }
}
