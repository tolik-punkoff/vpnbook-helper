using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;

namespace VPNBookHelper
{
    public static class Unzip
    {
        public static string ErrorMessage = "";
        public static bool UnzipToDir(string FileName,string UnzipDir)
        {
            ZipFile zip = null;

            try
            {
                zip = ZipFile.Read(FileName);
                foreach (ZipEntry e in zip)
                {
                    e.Extract(UnzipDir, 
                        ExtractExistingFileAction.OverwriteSilently); 
                        // перезаписывать существующие
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            return true;
        }
    }
}
