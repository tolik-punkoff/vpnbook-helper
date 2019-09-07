using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VPNBookHelper
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            pbLogo.BackColor = Color.FromArgb(20, 20, 46);
        }

        private void btnNetSettings_Click(object sender, EventArgs e)
        {
            frmNetworkSettings fNetworkSettings = new frmNetworkSettings();
            fNetworkSettings.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmCommonSettings fCommonSettings = new frmCommonSettings();
            fCommonSettings.ShowDialog();
        }

        private void DisableAllButtons()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    Button button = (Button)ctrl;
                    button.Enabled = false;
                }
            }
        }

        private void EnableAllButtons()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    Button button = (Button)ctrl;
                    button.Enabled = true;
                }
            }
        }

        private void btnGetConfigs_Click(object sender, EventArgs e)
        {
            lvLog.Items.Clear();
            MainWorker mw = new MainWorker();
            mw.StatusChanged += new MainWorker.OnStatusChanged(mw_StatusChanged);
            if (!mw.WorkerInit())
            {
                return;
            }
            mw.StartGetConfig();
        }

        void mw_StatusChanged(object sender, WorkerStatusEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                lvLog.Items.Add(e.Message);

                Color c = Color.Brown;

                if (e.Status == WorkerStatus.Start)
                {
                    pbLogo.Visible = false;
                    pbConnecting.Visible = true;
                    DisableAllButtons();
                }

                if ((e.Status == WorkerStatus.FatalError) || 
                    (e.Status == WorkerStatus.CompleteAll))
                {
                    pbLogo.Visible = true;
                    pbConnecting.Visible = false;
                    EnableAllButtons();
                }

                switch (e.Status)
                {
                    case WorkerStatus.Start: c = Color.LightSteelBlue; break;
                    case WorkerStatus.Process: c = Color.DarkGray; break;
                    case WorkerStatus.Complete: c = Color.MediumSeaGreen; break;
                    case WorkerStatus.CompleteAll: c = Color.LimeGreen; break;
                    case WorkerStatus.NotComplete: c = Color.Yellow; break;
                    case WorkerStatus.FatalError: c = Color.Red; break;
                    case WorkerStatus.Wait: c = Color.White; break;
                }

                lvLog.Items[lvLog.Items.Count - 1].ForeColor = c;                
                lvLog.TopItem = lvLog.Items[lvLog.Items.Count - 1];

                if (e.EventCode == "OPTIONS")
                {
                    frmVPNOptions fVPNOptions = new frmVPNOptions();
                    fVPNOptions.Settings = ((MainWorker)sender).Settings;
                    fVPNOptions.ShowDialog();
                    if (fVPNOptions.Cancelled) ((MainWorker)sender).CancelFlag = true;
                }

                if (e.EventCode == "FILES")
                {
                    frmSelectFiles fSelectFiles = new frmSelectFiles();
                    fSelectFiles.Settings = ((MainWorker)sender).Settings;
                    fSelectFiles.OVPNList = ((MainWorker)sender).OVPNList;
                    fSelectFiles.ShowDialog();
                    if (fSelectFiles.Cancelled) ((MainWorker)sender).CancelFlag = true;
                    ((MainWorker)sender).SelectedFiles = fSelectFiles.SelectFilesList;
                }


            });
        }

        private void btnGetPassword_Click(object sender, EventArgs e)
        {
            lvLog.Items.Clear();
            MainWorker mw_pass = new MainWorker();
            mw_pass.StatusChanged += new MainWorker.OnStatusChanged(mw_pass_StatusChanged);
            if (!mw_pass.WorkerInit())
            {
                return;
            }
            mw_pass.StartGetPassword();
        }

        void mw_pass_StatusChanged(object sender, WorkerStatusEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                lvLog.Items.Add(e.Message);

                Color c = Color.Brown;

                if (e.Status == WorkerStatus.Start)
                {
                    pbLogo.Visible = false;
                    pbConnecting.Visible = true;
                    DisableAllButtons();
                }

                if ((e.Status == WorkerStatus.FatalError) ||
                    (e.Status == WorkerStatus.CompleteAll))
                {
                    pbLogo.Visible = true;
                    pbConnecting.Visible = false;
                    EnableAllButtons();
                }

                switch (e.Status)
                {
                    case WorkerStatus.Start: c = Color.LightSteelBlue; break;
                    case WorkerStatus.Process: c = Color.DarkGray; break;
                    case WorkerStatus.Complete: c = Color.MediumSeaGreen; break;
                    case WorkerStatus.CompleteAll: c = Color.LimeGreen; break;
                    case WorkerStatus.NotComplete: c = Color.Yellow; break;
                    case WorkerStatus.FatalError: c = Color.Red; break;
                    case WorkerStatus.Wait: c = Color.White; break;
                }

                lvLog.Items[lvLog.Items.Count - 1].ForeColor = c;
                lvLog.TopItem = lvLog.Items[lvLog.Items.Count - 1];

                if (e.EventCode == "PASSWORD")
                {
                    frmSavePassword fSavePassword = new frmSavePassword();
                    fSavePassword.Settings = ((MainWorker)sender).Settings;
                    fSavePassword.Workdir = MainWorker.Workdir;
                    fSavePassword.Password = ((MainWorker)sender).RecognizedPass;
                    fSavePassword.ShowDialog();
                    ((MainWorker)sender).CancelFlag = fSavePassword.Cancelled;
                    ((MainWorker)sender).RecognizedPass = fSavePassword.Password;
                }

            });
        }

        private void lvLog_DoubleClick(object sender, EventArgs e)
        {
            string msg = "";
            foreach (ListViewItem item in lvLog.SelectedItems)
            {
                msg = msg + item.Text + "\r\n";
            }
            MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(L) Tolik Punkoff, 2019", "VPNBook Helper",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
    }
}
