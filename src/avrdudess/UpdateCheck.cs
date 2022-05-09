/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace avrdudess
{
    public class UpdateData
    {
        public Version currentVersion;
        public string updateAddr = "";
        public List<UpdateReleaseData> releases;

        public UpdateReleaseData latest
        {
            get
            {
                if (releases == null)
                    return null;

                UpdateReleaseData lastestRelease = null;

                foreach (UpdateReleaseData release in releases)
                {
                    if (lastestRelease == null || release.version.CompareTo(lastestRelease.version) > 0)
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

        public bool updateAvailable()
        {
            UpdateReleaseData latestRelease = latest;
            return (latestRelease != null && latestRelease.version.CompareTo(currentVersion) > 0);
        }
    }

    public class UpdateReleaseData
    {
        public int major;
        public int minor;
        public long dateUnix;
        public string info;

        public DateTime date
        {
            get
            {
                return new DateTime(1970, 1, 1).AddSeconds(dateUnix);
            }
        }

        public Version version
        {
            get
            {
                return new Version(major, minor);
            }
        }
    }

    sealed class UpdateCheck
    {
        private const string UPDATE_ADDR = "https://versions.zakkemble.net/avrdudess2.xml";

        public static readonly UpdateCheck check = new UpdateCheck();
        private long timeNow;
        public string errorMsg { get; private set; }

        private UpdateCheck() { }

        public bool needed()
        {
#if DEBUG
            return true;
#else
            timeNow = (long)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);

            // Check once a day
            if (timeNow - Config.Prop.updateCheck > TimeSpan.FromDays(1).TotalSeconds)
                return true;

            return false;
#endif
        }

        private void saveTime()
        {
            Config.Prop.updateCheck = timeNow;
        }

        public bool now(UpdateData updateData)
        {
            bool success = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UPDATE_ADDR);
                request.UserAgent = string.Format("Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER {0})", AssemblyData.version.ToString());
                request.ReadWriteTimeout = 30000;
                request.Timeout = 30000;
                request.KeepAlive = false;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#if DEBUG // Getting proxy info is slow, so don't use proxy in debug
                request.Proxy = null;
#endif

                updateData.releases = new List<UpdateReleaseData>();
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
                                    updateData.releases.Add(release);
                                    release = null;
                                    success = true; // We need at least 1 release entry otherwise something isn't right...
                                }
                            }
                            else if(reader.NodeType == XmlNodeType.Element)
                            {
                                reader.Read();
                                if(release == null)
                                {
                                    if(name == "updateAddr")
                                        updateData.updateAddr = reader.ReadContentAsString();
                                }
                                else
                                {
                                    switch (name)
                                    {
                                        case "major":
                                            release.major = reader.ReadContentAsInt();
                                            break;
                                        case "minor":
                                            release.minor = reader.ReadContentAsInt();
                                            break;
                                        case "date":
                                            release.dateUnix = reader.ReadContentAsLong();
                                            break;
                                        case "info":
                                            release.info = reader.ReadContentAsString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                saveTime();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }

            if (!success)
                errorMsg = Language.Translation.get("_UPDATE_BADXML");

            return success;
        }
    }
}
