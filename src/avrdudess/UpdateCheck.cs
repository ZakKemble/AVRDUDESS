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
using System.Net.Sockets;
using System.Text;
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


                // Wow, antivirus software really hates it when I use HttpWebRequest, HttpWebResponse and other similar things
                // I've had to use raw sockets here so avrdudess doesn't get detected as malware :/


                // Make request headers
                StringBuilder headers = new StringBuilder();
                headers.AppendLine("GET /avrdudess.xml HTTP/1.0");
                headers.AppendLine("Host: versions.zakkemble.co.uk");
                headers.AppendLine("Connection: Close");
                headers.AppendLine("User-Agent: Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER " + AssemblyData.version.ToString() + ")");
                //headers.AppendLine("Accept-Encoding: gzip");
                headers.AppendLine("");
                //MessageBox.Show(headers.ToString(), "headers");

                // DNS Lookup
                IPAddress[] ips = Dns.GetHostAddresses("versions.zakkemble.co.uk");
                if (ips.Length < 1)
                    throw new Exception("No IPs found for hostname");

                // Connect to server
                IPEndPoint RHost = new IPEndPoint(ips[new Random().Next(0, ips.Length)], 80);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.ReceiveTimeout = 30000;
                socket.SendTimeout = 30000;
                socket.Connect(RHost); // TODO: connect timeout

                // Send request
                socket.Send(Encoding.ASCII.GetBytes(headers.ToString()), SocketFlags.None);

                // Get response data
                byte[] buffer = new byte[socket.ReceiveBufferSize];
                int idx = 0;
                int bytes;
                do
                {
                    bytes = socket.Receive(buffer, idx, buffer.Length - idx, SocketFlags.None);
                    idx += bytes;

                    // Resize array if full
                    if (idx >= buffer.Length)
                    {
                        byte[] newBuffer = new byte[buffer.Length + socket.ReceiveBufferSize];
                        buffer.CopyTo(newBuffer, 0);
                        buffer = newBuffer;
                    }
                }
                while (bytes > 0);

                socket.Close();

                // Convert to string
                String response = Encoding.UTF8.GetString(buffer, 0, idx);
                //MessageBox.Show(response, "Response");

                // Get headers
                int endOfHeaders = response.IndexOf("\r\n\r\n");
                if (endOfHeaders < 0)
                    throw new Exception("End of headers not found");
                string recHeaders = response.Substring(0, endOfHeaders);
                //MessageBox.Show(recHeaders, "Headers");

                // Split headers
                string[] recSplitHeaders = recHeaders.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if(recSplitHeaders.Length < 1)
                    throw new Exception("No headers found");

                // Check status code
                string[] status = recSplitHeaders[0].Split(new string[] { " " }, 3, StringSplitOptions.None);
                if (status.Length != 3 || status[1] != "200")
                    throw new Exception("Status code not 200");

                // Split headers to name/value
                //recSplitHeaders[0] = "aa: ";
                foreach (string s in recSplitHeaders)
                {
                    string[] nameVal = s.Split(new string[] { ": " }, 2, StringSplitOptions.None);

                    if (nameVal.Length == 2 && nameVal[0].ToLower() == "content-encoding")
                    {

                    }
                }

                // Get data part
                response = response.Substring(endOfHeaders + 4);
                //MessageBox.Show(response, "Data");


                // TODO: GZIP decompress response


                // Convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(response);


                //currentVersion = new Version(1, 1);


/*
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UPDATE_ADDR);
                request.UserAgent = "Mozilla/5.0 (compatible; AVRDUDESS VERSION CHECKER " + AssemblyData.version.ToString() + ")";
                request.ReadWriteTimeout = 30000;
                request.Timeout = 30000;
                request.KeepAlive = false;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#if DEBUG // Getting proxy info is slow, so don't use proxy in debug
                request.Proxy = null;
#endif
*/

                // Do request
                //using (var responseStream = request.GetResponse().GetResponseStream())
                using (var responseStream = new MemoryStream(byteArray))
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
