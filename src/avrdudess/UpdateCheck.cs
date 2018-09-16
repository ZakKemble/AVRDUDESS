/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Net;
using System.Xml;

namespace avrdudess
{
    public class UpdateData
    {
        public Version newVersion;
        public Version currentVersion;
        public long newVersionDate;
        public string updateAddr = "";
        public string updateInfo = "";

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
            return (newVersion != null && currentVersion.CompareTo(newVersion) < 0);
        }
    }

    sealed class UpdateCheck
    {
        private const string UPDATE_ADDR = "http://versions.zakkemble.net/avrdudess.xml";

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
            try
            {
                int major = 0;
                int minor = 0;
                //int build = 0;
                //int revision = 0;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UPDATE_ADDR);
                request.UserAgent = string.Format("Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER {0})", AssemblyData.version.ToString());
                request.ReadWriteTimeout = 30000;
                request.Timeout = 30000;
                request.KeepAlive = false;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#if DEBUG // Getting proxy info is slow, so don't use proxy in debug
                request.Proxy = null;
#endif

                // Do request
                using (var responseStream = request.GetResponse().GetResponseStream())
                {
                    // XML
                    using (var reader = XmlReader.Create(responseStream))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                string name = reader.Name;
                                reader.Read();
                                switch (name)
                                {
                                    case "major":
                                        major = reader.ReadContentAsInt();
                                        break;
                                    case "minor":
                                        minor = reader.ReadContentAsInt();
                                        break;
                                    /*case "build":
                                        build = reader.ReadContentAsInt();
                                        break;
                                    case "revision":
                                        revision = reader.ReadContentAsInt();
                                        break;*/
                                    case "date":
                                        updateData.newVersionDate = reader.ReadContentAsLong();
                                        break;
                                    case "updateAddr":
                                        updateData.updateAddr = reader.ReadContentAsString();
                                        break;
                                    case "updateInfo":
                                        updateData.updateInfo = reader.ReadContentAsString();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                updateData.newVersion = new Version(major, minor);

                saveTime();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }

            return true;
        }
    }
}
