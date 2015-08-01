/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
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
        // No Dispatcher stuff in .NET 2.0, make a static reference to our main form
        public static Form UI;

        private static TextBox console;

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

        public static void consoleSet(TextBox textBox)
        {
            console = textBox;
        }

        // Write to console
        public static void consoleWrite(string text)
        {
            // Credits:
            // Uwe Tanger (Console in main window instead of separate window)
            // Dean (Console in main window instead of separate window)

            if(console != null)
                console.InvokeIfRequired(c => { ((TextBox)c).AppendText(text); });
        }

        // Clear console
        public static void consoleClear()
        {
            if(console != null)
                console.InvokeIfRequired(c => { ((TextBox)c).Clear(); });
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

            string result = String.Format("{0:0} {1}", (int)len, sizes[order]);
            return result;
        }
    }

    static class MsgBox
    {
        public static void error(string msg, Exception ex)
        {
            error(msg + Environment.NewLine + ex.Message);
        }

        public static void error(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void warning(string msg)
        {
            MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void notice(string msg)
        {
            MessageBox.Show(msg, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult confirm(string msg)
        {
            return (MessageBox.Show(msg, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2));
        }
    }

    static class AssemblyData
    {
        private static readonly Assembly assembly = Assembly.GetEntryAssembly();

        public static readonly string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyTitleAttribute), false))
                .Title;

        public static readonly string copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyCopyrightAttribute), false))
                .Copyright;

        public static readonly Version version = assembly.GetName().Version;

        public static readonly Icon icon = Icon.ExtractAssociatedIcon(assembly.Location);

        public static readonly string directory = Path.GetDirectoryName(assembly.CodeBase);
    }
}
