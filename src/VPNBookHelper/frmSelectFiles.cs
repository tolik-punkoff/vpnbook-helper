using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VPNBookHelper
{
    public partial class frmSelectFiles : Form
    {
        public frmSelectFiles()
        {
            InitializeComponent();
        }

        public appSettings Settings = null;
        public bool Cancelled = true;
        public List<string> OVPNList = null;
        string SelectMask = "*.ovpn";

        public List<string> SelectFilesList = null;

        private void frmSelectFiles_Load(object sender, EventArgs e)
        {
            SelectMask = Settings.SelectMask;
            txtSelectMask.Text = SelectMask;
            txtOutputDir.Text = Settings.OutputDir;

            string[] Buf = new string[OVPNList.Count];
            OVPNList.CopyTo(Buf);
            lstFiles.Items.AddRange(Buf);

            ChangeMask();
        }

        private void btnSelectOutputDir_Click(object sender, EventArgs e)
        {
            DialogResult Ans = dlgOutputDir.ShowDialog();

            if (Ans != DialogResult.Cancel)
            {
                txtOutputDir.Text = dlgOutputDir.SelectedPath;
            }
            Settings.OutputDir = txtOutputDir.Text;
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectFilesList = new List<string>();
            for (int i = 0; i < lstFiles.Items.Count; i++)
            {
                if (lstFiles.GetItemChecked(i))
                {
                    SelectFilesList.Add(lstFiles.Items[i].ToString());
                }                
            }

            if (SelectFilesList.Count == 0)
            {
                CommonFunctions.ErrMessage("Элементы не выбраны!");
                return;
            }

            Cancelled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }

        private void txtSelectMask_TextChanged(object sender, EventArgs e)
        {
            ChangeMask();
        }

        private void ChangeMask()
        {
            if (txtSelectMask.Text.Trim() == string.Empty)
            {
                SelectMask = "*.ovpn";
            }
            else
            {
                SelectMask = txtSelectMask.Text.Trim();
            }
            
            Settings.SelectMask = SelectMask;
            if (!Settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(Settings.ConfigError);
            }

            List<string> SelectedList = MainWorker.GetFilesList(
                MainWorker.Unpdir, SelectMask);
            if (SelectedList == null)
            {
                CommonFunctions.ErrMessage(MainWorker.ErrorMessage);
                return;
            }


            for (int i = 0; i < lstFiles.Items.Count; i++)
            {

                if (SelectedList.Contains(lstFiles.Items[i].ToString()))
                {
                    lstFiles.SetItemChecked(i, true);
                }
                else
                {
                    lstFiles.SetItemChecked(i, false);
                }                
            }
            
        }
    }
}
