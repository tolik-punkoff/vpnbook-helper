using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VPNBookHelper
{
    public partial class frmAddVPNOption : Form
    {
        public frmAddVPNOption()
        {
            InitializeComponent();
        }
        public string Option = "";
        public bool Cancelled = false;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOption.Text.Trim() == "")
            {
                CommonFunctions.ErrMessage("Опция не может быть пустой!");
                return;
            }

            Option = txtOption.Text.Trim().Replace(' ','-');
            Cancelled = false;
            this.Close();
        }

        private void frmAddVPNOption_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }
    }
}
