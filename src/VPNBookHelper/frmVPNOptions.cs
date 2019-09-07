using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VPNBookHelper
{
    public partial class frmVPNOptions : Form
    {
        public frmVPNOptions()
        {
            InitializeComponent();
        }

        public appSettings Settings = null;
        public bool Cancelled = true;
        Dictionary<string, string> VPNOptions = new Dictionary<string, string>();
        string SelectedOption = string.Empty; //переменная для хранения выбранной опции

        private void frmVPNOptions_Load(object sender, EventArgs e)
        {
            VPNOptions = Settings.GetVPNOptions();
            
            //добавляем существующие элементы в ListBox
            string[] tmpitems = new string[VPNOptions.Keys.Count];
            VPNOptions.Keys.CopyTo(tmpitems, 0);
            lstOptions.Items.AddRange(tmpitems);            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddVPNOption fAddVPNOption = new frmAddVPNOption();
            fAddVPNOption.ShowDialog();
            if (fAddVPNOption.Cancelled) return;

            if (!VPNOptions.ContainsKey(fAddVPNOption.Option))
            {
                lstOptions.Items.Add(fAddVPNOption.Option);
                VPNOptions.Add(fAddVPNOption.Option, string.Empty);
            }
            else
            {
                CommonFunctions.ErrMessage("Опция " + fAddVPNOption.Option +
                    " уже существует!");
            }
        }

        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOptions.SelectedIndex == -1) return;
            SelectedOption = lstOptions.Items[lstOptions.SelectedIndex].ToString();
            txtValue.Text = VPNOptions[SelectedOption];
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (SelectedOption != string.Empty)
            {
                VPNOptions[SelectedOption] = txtValue.Text.Trim();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lstOptions.Items.RemoveAt(lstOptions.SelectedIndex);
            VPNOptions.Remove(SelectedOption);
            if (lstOptions.Items.Count > 0)
            {
                lstOptions.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (string key in VPNOptions.Keys)
            {
                string v = VPNOptions[key];
                //если в строке есть пробелы...
                if (v.Contains(" "))
                {
                    //проверяем на наличие "", если нет - добавляем
                    //вообще нахуй всякие пути с пробелами
                    //если кто-то мудак, пусть сам и разбирается
                    VPNOptions[key] = CommonFunctions.AddQuotes(VPNOptions[key]);
                }

                // \ меняется на \\ (требование Openvpn для винды)
                VPNOptions[key].Replace(@"\", @"\\");
            }

            //Сохраняем опции
            Settings.ClearVPNOptions();

            if (!Settings.CreateVPNOptions(VPNOptions))
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
                return;
            }

            Settings.SaveConfig();
            Cancelled = false;
            this.Close();
        }
    }
}
