using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VPNBookHelper
{
    public partial class frmCommonSettings : Form
    {
        public bool Changed = false;
        private FormWorker fWorker = null;
        private appSettings settings = null;
        
        public frmCommonSettings()
        {
            InitializeComponent();
        }

        private void frmCommonSettings_Load(object sender, EventArgs e)
        {
            settings = new appSettings(CommonFunctions.SettingsPath + 
                CommonFunctions.AppSettingsFile);

            if (!settings.LoadConfig())
            {
                CommonFunctions.ErrMessage(settings.ConfigError);
                return;
            }
            fWorker = new FormWorker(settings, this);
            fWorker.FillForm();
        }
                
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {            
            if (!fWorker.GetData())
            {
                CommonFunctions.ErrMessage(fWorker.ErrorMessage);
                return;
            }
            if (!settings.SaveConfig())
            {
                CommonFunctions.ErrMessage(settings.ConfigError);
                return;
            }
            
            Changed = true;
            this.Close();
        }

        private void btnSelectAuthFile_Click(object sender, EventArgs e)
        {
            if (txtAuthFile.Text.Trim() == "")
            {
                dlgAuthFile.InitialDirectory = @"C:\";
            }
            else
            {
                dlgAuthFile.InitialDirectory = 
                    CommonFunctions.GetDirName(txtAuthFile.Text.Trim());
            }

            DialogResult Ans = dlgAuthFile.ShowDialog();

            if (Ans != DialogResult.Cancel)
            {
                txtAuthFile.Text = dlgAuthFile.FileName;
            }
        }

        private void btnSelectOutputDir_Click(object sender, EventArgs e)
        {
            DialogResult Ans = dlgOutputDir.ShowDialog();

            if (Ans != DialogResult.Cancel)
            {
                txtOutputDir.Text = dlgOutputDir.SelectedPath;
            }
        }

        private void btnVPNOptionsList_Click(object sender, EventArgs e)
        {
            frmVPNOptions fVPNOptions = new frmVPNOptions();

            fVPNOptions.Settings = settings;
            fVPNOptions.ShowDialog();
        }
    }
}
