// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2014-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace avrdudess
{
    abstract class Executable
    {
        private Process p;
        private Action<object> onFinish;
        private object param;
        public event EventHandler OnProcessStart;
        public event EventHandler OnProcessEnd;
        protected string binary;
        private bool enableConsoleUpdate;
        protected string outputLogStdErr { get; private set; } = string.Empty;
        protected string outputLogStdOut { get; private set; } = string.Empty;
        private Thread tConUpt;
        private readonly ManualResetEvent exitWait = new ManualResetEvent(false);
        private readonly ManualResetEvent stdOutWait = new ManualResetEvent(false);
        private readonly ManualResetEvent stdErrWait = new ManualResetEvent(false);

        // NOTE: can't write to memory and to console at the same time, as one method is async (memory) and the other is sync (console).
        // This is because process bars don't work with async mode as the event only fires on a new line.
        public enum OutputTo
        {
            Memory,
            Console
        }

        private enum Stream
        {
            StdOut,
            StdErr
        }

        protected void load(string defaultBinaryName, string filePath, bool enableConsoleWrite = true)
        {
            binary = searchForBinary(defaultBinaryName, filePath);

            if (binary == null)
                Util.consoleError("_EXECMISSING", defaultBinaryName);
            else if (enableConsoleWrite && tConUpt == null)
            {
                tConUpt = new Thread(new ThreadStart(tConsoleUpdate));
                tConUpt.IsBackground = true;
                tConUpt.Start();
            }
        }

        private string searchForBinary(string defaultBinaryName, string filePath)
        {
            if(Util.isWindows())
                defaultBinaryName += ".exe";

            string app;

            // Check user defined file path
            if (!string.IsNullOrEmpty(filePath))
            {
                if (File.Exists(filePath))
                    return filePath;
                return null;
            }

            // File exists in application directory (mainly for Windows)
            app = Path.Combine(AssemblyData.directory, defaultBinaryName);
            if (File.Exists(app))
                return app;

            // File exists in working directory
            app = Path.Combine(Directory.GetCurrentDirectory(), defaultBinaryName);
            if (File.Exists(app))
                return app;

            // Search PATHs
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(new char[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string path in paths)
            {
                app = Path.Combine(path, defaultBinaryName);
                if (File.Exists(app))
                    return app;
            }

            return null;
        }

        protected bool launch(string args, Action<object> onFinish, object param, OutputTo outputTo)
        {
            if (isActive()) // Another process is active
                return false;

            // Clear log
            outputLogStdErr = string.Empty;
            outputLogStdOut = string.Empty;
            //Util.consoleClear();

            // Binary is missing
            if (binary == null || !File.Exists(binary)) 
                return false;

            this.onFinish = onFinish;
            this.param = param;

            return launch(args, outputTo);
        }

        private bool launch(string args, OutputTo outputTo)
        {
            exitWait.Reset();
            stdOutWait.Reset();
            stdErrWait.Reset();

            Process tmp = new Process();
            tmp.StartInfo.FileName = binary;
            tmp.StartInfo.Arguments = args;
            tmp.StartInfo.CreateNoWindow = true;
            tmp.StartInfo.UseShellExecute = false;
            tmp.StartInfo.RedirectStandardOutput = true;
            tmp.StartInfo.RedirectStandardError = true;
            tmp.EnableRaisingEvents = true;
            if (outputTo == OutputTo.Memory)
            {
                tmp.OutputDataReceived += new DataReceivedEventHandler(outputLogHandler);
                tmp.ErrorDataReceived += new DataReceivedEventHandler(errorLogHandler);
            }
            tmp.Exited += new EventHandler(p_Exited);

            try
            {
                tmp.Start();
            }
            catch (Exception ex)
            {
                Util.consoleError("_EXECFAIL", ex.Message);
                return false;
            }

            OnProcessStart?.Invoke(this, EventArgs.Empty);

            //var _ = ConsumeReader(tmp.StandardOutput);
            //_ = ConsumeReader(tmp.StandardError);

            enableConsoleUpdate = (outputTo == OutputTo.Console);
            p = tmp;

            if (outputTo == OutputTo.Memory)
            {
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            }
            else
            {
                stdOutWait.Set();
                stdErrWait.Set();
            }

            return true;
        }
        /*
        // Better alternative to tConsoleUpdate, but needs .NET 4.5
        // https://stackoverflow.com/a/26722542
        async static Task ConsumeReader(TextReader reader)
        {
            char[] buff = new char[1];
            while ((await reader.ReadAsync(buff, 0, buff.Length)) > 0)
            {
                string s = new string(buff);
                if(s != "\r")
                    Util.consoleWrite(s);
            }
        }
        */
        private void p_Exited(object sender, EventArgs e)
        {
            exitWait.Set();
            OnProcessEnd?.Invoke(this, EventArgs.Empty);
            onFinish?.Invoke(param);
            onFinish = null;
        }

        // Progress bars don't work using async output, since it only fires when a new line is received
        // Problem: Slow if the process outputs a lot of text
        private void tConsoleUpdate()
        {
            while (true)
            {
                Thread.Sleep(15);

                if (!enableConsoleUpdate)
                    continue;

                try
                {
                    if (p != null)
                    {
                        char[] buff = new char[256];
                        
                        // TODO: read from stdError AND stdOut (AVRDUDE outputs stuff through stdError)
                        if (p.StandardError.Read(buff, 0, buff.Length) > 0)
                        {
                            string s = new string(buff);
                            Util.consoleWrite(s);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        // These methods are needed to properly capture the process output for logging
        private bool logger(string s, Stream stream)
        {
            if (s != null) // A null is sent when the stream is closed
            {
                string tmp = s.Replace("\0", string.Empty) + Environment.NewLine;
                if(stream == Stream.StdErr)
                    outputLogStdErr += tmp;
                else if (stream == Stream.StdOut)
                    outputLogStdOut += tmp;
                return true;
            }

            return false;
        }

        private void outputLogHandler(object sender, DataReceivedEventArgs e)
        {
            if (!logger(e.Data, Stream.StdOut))
                stdOutWait.Set();
        }

        private void errorLogHandler(object sender, DataReceivedEventArgs e)
        {
            if (!logger(e.Data, Stream.StdErr))
                stdErrWait.Set();
        }

        protected bool isActive()
        {
            return !p?.HasExited ?? false;
        }

        public bool kill()
        {
            if (!isActive())
                return false;
            p.Kill();
            return true;
        }

        protected void waitForExit()
        {
            if (isActive())
                exitWait.WaitOne();
            //p.WaitForExit(); // This seems to randomly hang if the process exits too quickly, even if a timeout is used

            // There might still be data in a buffer somewhere that needs to be read by the output handler even after the process has ended
            // TODO if the process never started then this should be skipped otherwise we will get stuck here
            stdOutWait.WaitOne();
            stdErrWait.WaitOne();
        }
    }
}
