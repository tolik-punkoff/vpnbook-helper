using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace VPNBookHelper
{
    public static class RunProcess
    {
        public static string ErrorMessage { get; private set; }

        public static bool OpenProcess(string FileName, string Arg)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = FileName;
            psi.Arguments = Arg;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process Proc = null;
            
            try
            {
                Proc = Process.Start(psi);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            Proc.WaitForExit();

            return true;
        }
    }
}
