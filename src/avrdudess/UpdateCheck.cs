/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace avrdudess
{
    sealed class UpdateCheck
    {
        private const string UPDATE_ADDR = "http://versions.zakkemble.co.uk/avrdudess.xml";

        public static readonly UpdateCheck check = new UpdateCheck();
        private long now;
        private Version newVersion;

        private UpdateCheck() { }

        public void checkNow()
        {
            now = (long)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);

            // Check once a day
            if (now - Config.Prop.updateCheck > TimeSpan.FromDays(1).TotalSeconds)
            {
                Thread t = new Thread(new ThreadStart(tUpdate));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void skipVersion()
        {
            if(newVersion != null)
                Config.Prop.skipVersion = newVersion;
        }

        private void saveTime()
        {
            Config.Prop.updateCheck = now;
        }

        private void tUpdate()
        {
            Thread.Sleep(500);

            try
            {
                Version currentVersion = new Version(AssemblyData.version.Major, AssemblyData.version.Minor);

                int major = 0;
                int minor = 0;
                //int build = 0;
                //int revision = 0;
                long date = 0;
                string updateAddr = "";
                string updateInfo = "";

                // Setup web request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UPDATE_ADDR);
                request.UserAgent = "Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER " + AssemblyData.version.ToString() + ")";
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
                                        date = reader.ReadContentAsLong();
                                        break;
                                    case "updateAddr":
                                        updateAddr = reader.ReadContentAsString();
                                        break;
                                    case "updateInfo":
                                        updateInfo = reader.ReadContentAsString();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                newVersion = new Version(major, minor);

                saveTime();

                // Notify of new update
                if (Config.Prop.skipVersion != newVersion && currentVersion.CompareTo(newVersion) < 0)
                {
                    string newVersionStr = newVersion.ToString() + " (" + new DateTime(1970, 1, 1).AddSeconds(date).ToLocalTime().ToShortDateString() + ")";

                    Util.UI.Invoke(new MethodInvoker(() =>
                    {
                        FormUpdate f = new FormUpdate();
                        f.doUpdateMsg(currentVersion.ToString(), newVersionStr, updateInfo, updateAddr, skipVersion);
                    }));
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
