/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.net
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// http://stackoverflow.com/questions/2367718/automating-the-invokerequired-code-pattern

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}

namespace avrdudess
{
    // Action without parameters doesn't seem to be a thing in .NET 2.0
    public delegate void Action();

    static class Util
    {
        private static RichTextBox console;

        public static void InvokeIfRequired<T>(this T c, Action<T> action)
            where T : Control
        {
            try
            {
                if (c.InvokeRequired)
                    c.Invoke(new Action(() => action(c)));
                else
                    action(c);
            }
            catch (Exception)
            {

            }
        }

        public static void consoleSet(RichTextBox textBox)
        {
            console = textBox;
        }

        public static void consoleError(string text, params object[] args)
        {
            text = Language.Translation.get(text);
            text = string.Format(text, args);
            consoleWrite(string.Format("{0}: {1}{2}", Language.Translation.get("_ERRORUC"), text, Environment.NewLine), Color.Red);
        }

        public static void consoleWarning(string text, params object[] args)
        {
            text = Language.Translation.get(text);
            text = string.Format(text, args);
            consoleWrite(string.Format("{0}: {1}{2}", Language.Translation.get("_WARNINGUC"), text, Environment.NewLine), Color.Yellow);
        }

        public static void consoleSuccess(string text, params object[] args)
        {
            text = Language.Translation.get(text);
            text = string.Format(text, args);
            consoleWrite(string.Format("{0}: {1}{2}", Language.Translation.get("_SUCCESSUC"), text, Environment.NewLine), Color.LightGreen);
        }

        public static void consoleWriteLine()
        {
            consoleWrite(Environment.NewLine, Color.White);
        }

        public static void consoleWriteLine(string text, params object[] args)
        {
            consoleWriteLine(text, Color.White, args);
        }

        public static void consoleWriteLine(string text, Color colour, params object[] args)
        {
            text = Language.Translation.get(text);
            text = string.Format(text, args);
            consoleWrite(text + Environment.NewLine, colour);
        }

        public static void consoleWrite(string text)
        {
            consoleWrite(text, Color.White);
        }

        public static void consoleWrite(string text, Color colour)
        {
            // Credits:
            // Uwe Tanger (Console in main window instead of separate window)
            // Dean (Console in main window instead of separate window)

            if (console != null)
            {
                console.InvokeIfRequired(c =>
                {
                    c.AppendText(text, colour);
                    if(text.Contains("\n")) // Without this the text box spazzes a bit on the progress bars
                        c.ScrollToCaret();
                });
            }
        }

        public static void consoleClear()
        {
            if (console != null)
            {
                console.InvokeIfRequired(c =>
                {
                    c.Clear();
                });
            }
        }

        public static string fileSizeFormat(int value)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            float len = value;
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len /= 1024;
            }

            string result = string.Format("{0:0} {1}", (int)len, sizes[order]);
            return result;
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }

    static class MsgBox
    {
        public static void error(string msg, params object[] args)
        {
            msg = Language.Translation.get(msg);
            msg = string.Format(msg, args);

            MessageBox.Show(
                msg,
                Language.Translation.get("_ERROR"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        public static void warning(string msg, params object[] args)
        {
            msg = Language.Translation.get(msg);
            msg = string.Format(msg, args);

            MessageBox.Show(
                msg,
                Language.Translation.get("_WARNING"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
                );
        }

        public static void notice(string msg, params object[] args)
        {
            msg = Language.Translation.get(msg);
            msg = string.Format(msg, args);

            MessageBox.Show(
                msg,
                Language.Translation.get("_NOTICE"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }

        public static DialogResult confirm(string msg, params object[] args)
        {
            msg = Language.Translation.get(msg);
            msg = string.Format(msg, args);

            return (MessageBox.Show(
                msg,
                Language.Translation.get("_CONFIRM"),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
                ));
        }
    }

    static class AssemblyData
    {
        public static readonly Assembly assembly = Assembly.GetEntryAssembly();

        public static readonly string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyTitleAttribute), false))
                .Title;

        public static readonly string copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyCopyrightAttribute), false))
                .Copyright;

        public static readonly Version version = assembly.GetName().Version;

        public static readonly Icon icon = Icon.ExtractAssociatedIcon(assembly.Location);

        // TODO use .Location instead of .CodeBase?
        // Location returns absolute path
        // CodeBase returns a URI (file://....)
        public static readonly string directory = Path.GetDirectoryName(assembly.Location);
    }
}
