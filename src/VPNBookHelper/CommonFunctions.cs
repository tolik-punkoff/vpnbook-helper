using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VPNBookHelper
{
    public static class CommonFunctions
    {
        public static string SettingsPath = CommonFunctions.AddSlash(
            Application.StartupPath);
        public static string NetSettingsFile = "network.xml";
        public static string AppSettingsFile = "settings.xml";
        public static string TessCmd = SettingsPath + "tesseract\\tesseract.exe";

        public static string AddSlash(string st)
        {
            if (st.EndsWith("\\"))
            {
                return st;
            }

            return st + "\\";
        }

        public static string GetDirName(string FullPath)
        {
            FileInfo fi = new FileInfo(FullPath);
            return fi.DirectoryName;
        }

        public static string AddQuotes(string InputString)
        {
            string RetString = InputString;

            if (InputString.StartsWith("\"") && InputString.EndsWith("\""))
            {
                return RetString;
            }
            else
            {
                if (!InputString.StartsWith("\""))
                {
                    RetString = "\"" + RetString;
                }
                if (!InputString.EndsWith("\""))
                {
                    RetString = RetString + "\"";
                }
            }

            return RetString;
        }        

        public static void ErrMessage(string stMessage)
        {
            MessageBox.Show(stMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }        

    }
}
