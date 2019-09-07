using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VPNBookHelper
{
    #region add_classes
    public enum WorkerStatus
    {
        Start = 0,
        Process = 1,
        Complete = 2,
        NotComplete = 3,
        FatalError = 4,
        CompleteAll = 5,
        Wait=6
    }
    public class WorkerStatusEventArgs : EventArgs
    {
        public string Message { get; set; }
        public WorkerStatus Status { get; set; }
        public string EventCode { get; set; }
    }
    #endregion
    public class MainWorker
    {
        public delegate void OnStatusChanged(object sender, WorkerStatusEventArgs e);

        public event OnStatusChanged StatusChanged;

        public bool CancelFlag { get; set; }
        public string RecognizedPass { get; set; }

        private SendRequest Downloader = null;
        private NetSettings netSettings = null;
        
        public appSettings Settings = null;
        public List<string> OVPNList = null;
        public List<string> SelectedFiles = null;

        public static string Workdir = CommonFunctions.AddSlash(
            Environment.GetEnvironmentVariable("TEMP")) + "vpnbook.tmp\\";
        public static string Unpdir = CommonFunctions.AddSlash(
            Environment.GetEnvironmentVariable("TEMP")) + "vpnbook.tmp\\unpdir\\";
        private string HTMLPage = "vpnbook.html";
        public static string ErrorMessage = "";

        public MainWorker()
        {
            CancelFlag = false;
        }

        private void StatusChange(WorkerStatus Status, string stMessage)
        {
            WorkerStatusEventArgs e = new WorkerStatusEventArgs();
            e.Status = Status;
            e.Message = stMessage;
            if (StatusChanged != null) StatusChanged(this, e);
        }

        private void StatusChange(WorkerStatus Status, string stMessage, string EventCode)
        {
            WorkerStatusEventArgs e = new WorkerStatusEventArgs();
            e.Status = Status;
            e.Message = stMessage;
            e.EventCode = EventCode;
            if (StatusChanged != null) StatusChanged(this, e);
        }

        public bool WorkerInit()
        {
            netSettings = new NetSettings(CommonFunctions.SettingsPath +
                CommonFunctions.NetSettingsFile);
            Settings = new appSettings(CommonFunctions.SettingsPath +
                CommonFunctions.AppSettingsFile);

            if (netSettings.LoadConfig()!=NetConfigStatus.OK)
            {
                StatusChange(WorkerStatus.FatalError, netSettings.ConfigError);
                return false;
            }

            if (!Settings.LoadConfig())
            {
                StatusChange(WorkerStatus.FatalError, Settings.ConfigError);
                return false;
            }

            ClearTemp();

            try
            {
                Directory.CreateDirectory(Unpdir);
            }
            catch (Exception ex)
            {
                StatusChange(WorkerStatus.FatalError, ex.Message);
                return false;
            }

            Downloader = new SendRequest(Settings.PageAddr, Workdir + HTMLPage);
            Downloader.ConnectionTimeout = netSettings.ConnectionTimeout;
            Downloader.ConnectionType = netSettings.ConnectionType;
            Downloader.ProxyAddress = netSettings.ProxyAddress;
            Downloader.ProxyPassword = netSettings.ProxyPassword;
            Downloader.ProxyPort = netSettings.ProxyPort;
            Downloader.ProxyUser = netSettings.ProxyUser;

            return true;
        }

        private void GetConfigs()
        {
            StatusChange(WorkerStatus.Start, "Получаю файлы конфигурации...");
            //получаем HTML-страницу
            StatusChange(WorkerStatus.Process, "Загрузка: " + Settings.PageAddr);
            if (!Downloader.CreateRequest())
            {
                StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                return;
            }
            else
            {
                if (!Downloader.Send())
                {
                    StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                    return;
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }
            }

            StatusChange(WorkerStatus.Process, "Разбираю HTML-файл...");
            MiniParser mp = new MiniParser(Workdir + HTMLPage);
            if (!mp.Load())
            {
                StatusChange(WorkerStatus.FatalError, mp.ErrorMessage);
                return;
            }

            List <string> ZIPFiles = mp.Select(".zip",mp.ParseTags("a","href"));
            if (ZIPFiles.Count == 0)
            {
                StatusChange(WorkerStatus.FatalError, "ZIP-файлы не найдены!");
                return;
            }
            foreach (string ZIPFile in ZIPFiles)
            {
                StatusChange(WorkerStatus.Process, "Обнаружен файл: " + ZIPFile);
            }

            StatusChange(WorkerStatus.Process, "Загрузка файлов: ");
            foreach (string ZIPFile in ZIPFiles)
            {
                string filename = GetUnixFilename(ZIPFile);
                StatusChange(WorkerStatus.Process, "Загрузка: " + filename);

                Downloader.URL = Settings.PageAddr + ZIPFile;
                Downloader.OutputFile = Workdir + filename;
                if (!Downloader.CreateRequest())
                {
                    StatusChange(WorkerStatus.NotComplete, Downloader.ErrorMessage);
                    continue;
                }

                if (!Downloader.Send())
                {
                    StatusChange(WorkerStatus.NotComplete, Downloader.ErrorMessage);
                    continue;
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }
            }

            StatusChange(WorkerStatus.Process, "Распаковка файлов: ");
            List<string> ZIPList = GetFilesList(Workdir, "*.zip");
            if (ZIPList == null)
            {
                StatusChange(WorkerStatus.FatalError, ErrorMessage);
                return;
            }
            if (ZIPList.Count == 0)
            {
                StatusChange(WorkerStatus.FatalError, 
                    "ZIP-файлы для распаковки не найдены!");
                return;
            }

            foreach (string filename in ZIPList)
            {
                StatusChange(WorkerStatus.Process, "Распаковка: " + filename);
                if (!Unzip.UnzipToDir(Workdir + filename, Unpdir))
                {
                    StatusChange(WorkerStatus.NotComplete, Unzip.ErrorMessage);
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }                
            }

            StatusChange(WorkerStatus.Wait, "Ожидаю данных от пользователя...",
                    "OPTIONS");
            if (CancelFlag)
            {
                StatusChange(WorkerStatus.FatalError, "Отменено пользователем.");
                return;
            }
                        
            OVPNList = GetFilesList(Unpdir, "*.ovpn");
            if (OVPNList.Count == 0)
            {
                StatusChange(WorkerStatus.FatalError,
                    "Файлы конфигурации *.ovpn не найдены!");
                return;
            }

            StatusChange(WorkerStatus.Process, "Добавляю опции...");
            foreach (string filename in OVPNList)
            {
                StatusChange(WorkerStatus.Process, filename);
                if (!AddOptions(Unpdir + filename))
                {
                    StatusChange(WorkerStatus.NotComplete, ErrorMessage);
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }
            }

            CancelFlag = false;
            StatusChange(WorkerStatus.Wait, "Ожидаю данных от пользователя...",
                    "FILES");
            if (CancelFlag)
            {
                StatusChange(WorkerStatus.FatalError, "Отменено пользователем.");
                return;
            }

            StatusChange(WorkerStatus.Process, "Копирование файлов...");
            StatusChange(WorkerStatus.Process, "Целевой каталог: " + Settings.OutputDir);
            foreach (string s in SelectedFiles)
            {
                StatusChange(WorkerStatus.Process, s);

                try
                {
                    File.Copy(Unpdir + s, Settings.OutputDir + s,true);
                    StatusChange(WorkerStatus.Complete, "OK");
                }
                catch (Exception ex)
                {
                    StatusChange(WorkerStatus.NotComplete, ex.Message);
                }
            }
            
            StatusChange(WorkerStatus.CompleteAll,"Процесс успешно завершен!");
        }

        private void GetPassword()
        {
            StatusChange(WorkerStatus.Start, "Получаю пароль...");

            if (!File.Exists(CommonFunctions.TessCmd))
            {
                StatusChange(WorkerStatus.FatalError, "Не найден tesseract.exe");
                return;
            }

            //получаем HTML-страницу
            StatusChange(WorkerStatus.Process, "Загрузка: " + Settings.PageAddr);
            if (!Downloader.CreateRequest())
            {
                StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                return;
            }
            else
            {
                if (!Downloader.Send())
                {
                    StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                    return;
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }
            }

            StatusChange(WorkerStatus.Process, "Разбираю HTML-файл...");
            MiniParser mp = new MiniParser(Workdir + HTMLPage);
            if (!mp.Load())
            {
                StatusChange(WorkerStatus.FatalError, mp.ErrorMessage);
                return;
            }
            List<string> PassImages = mp.Select("password.php", 
                mp.ParseTags("img", "src"));
            if (PassImages.Count < 1)
            {
                StatusChange(WorkerStatus.FatalError, 
                    "Ссылка на изображение не найдена!");
                return;
            }

            string imagelink = PassImages[0].Replace(' ','+');
            StatusChange(WorkerStatus.Process, 
                "Загрузка: " + Settings.PageAddr + imagelink);
            Downloader.URL = Settings.PageAddr + imagelink;
            Downloader.OutputFile = Workdir + "password.png";
            if (!Downloader.CreateRequest())
            {
                StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                return;
            }
            else
            {
                if (!Downloader.Send())
                {
                    StatusChange(WorkerStatus.FatalError, Downloader.ErrorMessage);
                    return;
                }
                else
                {
                    StatusChange(WorkerStatus.Complete, "OK");
                }
            }

            StatusChange(WorkerStatus.Process, "Запуск распознавания...");
            if (!RunProcess.OpenProcess(CommonFunctions.TessCmd,
                Workdir + "password.png " + Workdir + "password"))
            {
                StatusChange(WorkerStatus.FatalError, RunProcess.ErrorMessage);
                return;
            }
            else
            {
                StatusChange(WorkerStatus.Complete, "Распознавание завершено.");
            }

            //чтение распознанного пароля
            if (!File.Exists(Workdir + "password.txt"))
            {
                StatusChange(WorkerStatus.FatalError,
                    "Файл с результатами распознавания не найден!");
                StatusChange(WorkerStatus.FatalError,
                    "[" + Workdir + "password.txt]");
                return;
            }
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(Workdir + "password.txt");
                RecognizedPass = sr.ReadLine();
                sr.Close();
            }
            catch (Exception ex)
            {
                if (sr != null) sr.Close();
                StatusChange(WorkerStatus.FatalError, ex.Message);
                return;
            }
            if (string.IsNullOrEmpty(RecognizedPass))
            {
                StatusChange(WorkerStatus.FatalError, "Пароль пустой!");
                return;
            }

            StatusChange(WorkerStatus.Process, "Пароль: " + RecognizedPass);

            StatusChange(WorkerStatus.Wait, "Ожидаю данных от пользователя...",
                    "PASSWORD");
            if (CancelFlag)
            {
                StatusChange(WorkerStatus.FatalError, "Отменено пользователем.");
                return;
            }            
            StatusChange(WorkerStatus.Process, "Сохраняемый пароль: " + 
                RecognizedPass);
            
            StatusChange(WorkerStatus.Process, "Сохраняю в:");
            StatusChange(WorkerStatus.Process, Settings.AuthFile);
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(Settings.AuthFile);
                sw.WriteLine("vpnbook");
                sw.WriteLine(RecognizedPass);
                sw.Close();
            }
            catch (Exception ex)
            {
                if (sw != null) sw.Close();
                StatusChange(WorkerStatus.FatalError, ex.Message);
                return;
            }
            StatusChange(WorkerStatus.Complete, "OK");
            
            StatusChange(WorkerStatus.CompleteAll, "Процесс успешно завершен!");
        }

        private string GetUnixFilename(string UNIXFilename)
        {
            int idx = UNIXFilename.LastIndexOf("/");
            return UNIXFilename.Substring(idx + 1);
        }

        public static List<string> GetFilesList(string stPath, string stMask)
        {            
            List<string> Result = new List<string>();
            ErrorMessage = "";

            DirectoryInfo dir = new DirectoryInfo(stPath);

            try
            {
                FileInfo[] fis = dir.GetFiles(stMask);
                foreach (FileInfo fi in fis)
                {
                    Result.Add(fi.Name);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Result = null;
            }

            return Result;
        }

        private int GetNumberConfigString(string[] Config, string Option)
        {
            int Count = -1;
            foreach (string s in Config)
            {
                Count++;                
                string Check = s.Trim();
                if (Check.StartsWith(Option))
                {
                    return Count;
                }
            }

            return -1;
        }

        private bool AddOptions(string FileName)
        {
            ErrorMessage = "";
            Dictionary<string, string> VPNOptions = Settings.GetVPNOptions();
            if (VPNOptions.Count == 0) return true;
            string[] ConfigBuf = null;

            try
            {
                ConfigBuf = File.ReadAllLines(FileName);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            List<string> AddedOptions = new List<string>();            
            foreach (string key in VPNOptions.Keys)
            {
                int n = GetNumberConfigString(ConfigBuf, key);

                if (n == -1) //строки в конфиге еще нет, добавляем в отдельный List
                {
                    AddedOptions.Add(key + " " + VPNOptions[key]);
                }
                else //строка есть, меняем
                {
                    ConfigBuf[n] = key + " " + VPNOptions[key];
                }
            }

            //сохраняем измененный файл
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(FileName);
                foreach (string s in AddedOptions)
                {
                    sw.WriteLine(s);
                }
                foreach (string s in ConfigBuf)
                {
                    sw.WriteLine(s);
                }
            }
            catch (Exception ex)
            {
                if (sw != null) sw.Close();
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }

        private void ClearTemp()
        {
            List<string> tmpFiles = GetFilesList(Unpdir, "*.*");
            if (tmpFiles != null)
            {
                foreach (string tmpfile in tmpFiles)
                {
                    try
                    {
                        File.Delete(Unpdir + tmpfile);
                    }
                    catch { }
                }
            }

            try
            {
                Directory.Delete(Unpdir);
            }
            catch { }

            tmpFiles = GetFilesList(Workdir, "*.*");
            if (tmpFiles != null)
            {
                foreach (string tmpfile in tmpFiles)
                {
                    try
                    {
                        File.Delete(Workdir + tmpfile);
                    }
                    catch { }
                }
            }

            try
            {
                Directory.Delete(Workdir);
            }
            catch { }
        }

        public void StartGetConfig()
        {
            System.Threading.Thread getcfgThread =
                new System.Threading.Thread(GetConfigs);
            getcfgThread.Start();
        }

        public void StartGetPassword()
        {
            System.Threading.Thread getcfgThread =
                new System.Threading.Thread(GetPassword);
            getcfgThread.Start();
        }
    }
}
