using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;

namespace VPNBookHelper
{    
    public class appSettings
    {
        public string PageAddr { get; set; } //адрес страницы
        public string SelectMask { get; set; } //маска для выбора конфигов
        public string OutputDir { get; set; } //куда сохранять файлы конфигов
        public string AuthFile { get; set; } //путь к файлу с логином и паролем
        
        public string ConfigError { get; private set; }
        
        private string TableName = "";
        private string configFile = "";
        private DataSet dsConfig = new DataSet();        

        public appSettings(string filename)
        {
            configFile = filename;
            TableName = this.GetType().Name;
            CreateDataSet();
            string s = Path.GetDirectoryName(filename);
            try
            {
                Directory.CreateDirectory(s);
            }
            catch { }

            PageAddr = "https://www.vpnbook.com/";
            SelectMask = "*.ovpn";
            OutputDir = CommonFunctions
                .AddSlash(Environment.GetEnvironmentVariable("USERPROFILE")) +
                "OpenVPN\\config\\";                
            AuthFile = CommonFunctions
                .AddSlash(Environment.GetEnvironmentVariable("USERPROFILE")) +
                "OpenVPN\\config\\vpnbook.auth";;            
        }        

        public bool LoadConfig()
        {
            //файла нет, значения установлены по умолчанию
            if (!File.Exists(configFile))
            {
                return true;
            }

            //почистим таблицы DataSet перед загрузкой
            foreach (DataTable table in dsConfig.Tables)
            {
                table.Rows.Clear();
            }

            //файл есть, пробуем загрузить в DataSet
            try
            {
                dsConfig.ReadXml(configFile);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            //загрузка полей класса из DataSet
            if (dsConfig.Tables[TableName].Rows.Count > 0)
            {
                PropertyInfo[] properties = this.GetType().GetProperties();
                foreach (PropertyInfo pr in properties)
                {
                    string propName = pr.Name;
                    object propValue = dsConfig.Tables[TableName].Rows[0][propName];
                    if (propValue.GetType() != typeof(System.DBNull))
                    {
                        pr.SetValue(this, propValue, null);
                    }
                }                
            }
                        
            return true;
        }

        public bool SaveConfig()
        {         
            ConfigError = null;            

            dsConfig.Tables[TableName].Rows.Clear();
            DataRow dr = dsConfig.Tables[TableName].NewRow();


            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo pr in properties)
            {
                string propName = pr.Name;
                object propValue = pr.GetValue(this, null);
                dr[propName] = propValue;
            }

            dsConfig.Tables[TableName].Rows.Add(dr);

            try
            {
                dsConfig.WriteXml(configFile);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }

            return true;
        }

        private void CreateDataSet()
        {
            dsConfig.Tables.Add(TableName);

            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo pr in properties)
            {
                dsConfig.Tables[TableName].Columns.Add(pr.Name, pr.PropertyType);
            }

            dsConfig.Tables.Add("VPNOptions");
            dsConfig.Tables["VPNOptions"].Columns.Add("OptionName", typeof(string));
            dsConfig.Tables["VPNOptions"].Columns.Add("OptionValue", typeof(string));
            dsConfig.Tables["VPNOptions"].Columns["OptionName"].Unique = true;            
        }

        public void ClearVPNOptions()
        {
            dsConfig.Tables["VPNOptions"].Rows.Clear();
        }


        public bool AddVPNOption(string OptionName, string OptionValue)
        {
            try
            {
                dsConfig.Tables["VPNOptions"].Rows.Add(OptionName, OptionValue);
            }
            catch (Exception ex)
            {
                ConfigError = ex.Message;
                return false;
            }
            return true;
        }

                
        public bool CreateVPNOptions(Dictionary<string, string> vpnoptions)
        {
            ClearVPNOptions();

            foreach (KeyValuePair<string, string> kvp in vpnoptions)
            {
                if (!AddVPNOption(kvp.Key, kvp.Value))
                {
                    return false;
                }
            }
            return true;
        }
                
        public Dictionary<string, string> GetVPNOptions()
        {
            Dictionary<string, string> result = new Dictionary<string,string>();

            foreach (DataRow dr in dsConfig.Tables["VPNOptions"].Rows)
            {
                result.Add(dr["OptionName"].ToString(), dr["OptionValue"].ToString());
            }

            return result;
        }        
    }
}
