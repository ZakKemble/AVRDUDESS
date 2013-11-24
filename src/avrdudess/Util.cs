/*
 * Project: AVRDUDESS - A GUI for AVRDUDE
 * Author: Zak Kemble, contact@zakkemble.co.uk
 * Copyright: (C) 2013 by Zak Kemble
 * License: GNU GPL v3 (see License.txt)
 * Web: http://blog.zakkemble.co.uk/avrdudess-a-gui-for-avrdude/
 */

using System;
using System.Drawing;
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
    }

    static class AssemblyData
    {
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        public static readonly string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyTitleAttribute), false))
                .Title;

        public static readonly string copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                assembly, typeof(AssemblyCopyrightAttribute), false))
                .Copyright;

        public static readonly Version version = assembly.GetName().Version;

        public static readonly Icon icon = Icon.ExtractAssociatedIcon(assembly.Location);
    }
}
