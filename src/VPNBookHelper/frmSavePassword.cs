using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VPNBookHelper
{
    public partial class frmSavePassword : Form
    {
        public frmSavePassword()
        {
            InitializeComponent();
        }

        public appSettings Settings = null;
        public string Workdir = "";
        private string PassPict = "";
        public string Password = "";
        public bool Cancelled = true;

        private void frmSavePassword_Load(object sender, EventArgs e)
        {
            PassPict = Workdir + "password.png";
            pbPassword.BackColor = Color.White;
            pbPassword.BackgroundImageLayout = ImageLayout.Center;

            FileStream fs = new FileStream(PassPict, FileMode.Open);
            pbPassword.BackgroundImage = Image.FromStream(fs);
            fs.Close();

            txtAuthFile.Text = Settings.AuthFile;
            txtPassword.Text = Password;
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
                Settings.AuthFile = txtAuthFile.Text;
                if (!Settings.SaveConfig())
                {
                    CommonFunctions.ErrMessage(Settings.ConfigError);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != Password)
            {
                DialogResult Ans = MessageBox.Show(
                    "Пароль был изменен. Сохранить измененный пароль?",
                    "Пароль изменен", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (Ans == DialogResult.Yes)
                {
                    Password = txtPassword.Text;
                }
            }

            Cancelled = false;
            this.Close();
        }        
    }
}
