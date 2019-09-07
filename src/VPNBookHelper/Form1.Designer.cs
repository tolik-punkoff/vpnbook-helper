namespace VPNBookHelper
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnNetSettings = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGetConfigs = new System.Windows.Forms.Button();
            this.btnGetPassword = new System.Windows.Forms.Button();
            this.pbConnecting = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lvLog = new VPNBookHelper.MyListView();
            this.colMain = new System.Windows.Forms.ColumnHeader();
            this.btnAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbConnecting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNetSettings
            // 
            this.btnNetSettings.Location = new System.Drawing.Point(523, 382);
            this.btnNetSettings.Name = "btnNetSettings";
            this.btnNetSettings.Size = new System.Drawing.Size(106, 27);
            this.btnNetSettings.TabIndex = 4;
            this.btnNetSettings.Text = "Настройки сети...";
            this.btnNetSettings.UseVisualStyleBackColor = true;
            this.btnNetSettings.Click += new System.EventHandler(this.btnNetSettings_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(523, 349);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(106, 27);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Настройки...";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnGetConfigs
            // 
            this.btnGetConfigs.Location = new System.Drawing.Point(523, 73);
            this.btnGetConfigs.Name = "btnGetConfigs";
            this.btnGetConfigs.Size = new System.Drawing.Size(106, 38);
            this.btnGetConfigs.TabIndex = 1;
            this.btnGetConfigs.Text = "Получить файлы конфигурации...";
            this.btnGetConfigs.UseVisualStyleBackColor = true;
            this.btnGetConfigs.Click += new System.EventHandler(this.btnGetConfigs_Click);
            // 
            // btnGetPassword
            // 
            this.btnGetPassword.Location = new System.Drawing.Point(523, 117);
            this.btnGetPassword.Name = "btnGetPassword";
            this.btnGetPassword.Size = new System.Drawing.Size(106, 38);
            this.btnGetPassword.TabIndex = 2;
            this.btnGetPassword.Text = "Получить пароль...";
            this.btnGetPassword.UseVisualStyleBackColor = true;
            this.btnGetPassword.Click += new System.EventHandler(this.btnGetPassword_Click);
            // 
            // pbConnecting
            // 
            this.pbConnecting.Image = global::VPNBookHelper.Properties.Resources.connecting;
            this.pbConnecting.Location = new System.Drawing.Point(545, 3);
            this.pbConnecting.Name = "pbConnecting";
            this.pbConnecting.Size = new System.Drawing.Size(64, 64);
            this.pbConnecting.TabIndex = 27;
            this.pbConnecting.TabStop = false;
            this.pbConnecting.Visible = false;
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImage = global::VPNBookHelper.Properties.Resources.logo;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbLogo.Location = new System.Drawing.Point(523, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(106, 64);
            this.pbLogo.TabIndex = 28;
            this.pbLogo.TabStop = false;
            // 
            // lvLog
            // 
            this.lvLog.BackColor = System.Drawing.Color.Black;
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMain});
            this.lvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvLog.Location = new System.Drawing.Point(2, 3);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(518, 439);
            this.lvLog.TabIndex = 0;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            this.lvLog.DoubleClick += new System.EventHandler(this.lvLog_DoubleClick);
            // 
            // colMain
            // 
            this.colMain.Text = "";
            this.colMain.Width = 512;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(523, 415);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(106, 27);
            this.btnAbout.TabIndex = 29;
            this.btnAbout.Text = "О программе...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 454);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnGetPassword);
            this.Controls.Add(this.btnGetConfigs);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnNetSettings);
            this.Controls.Add(this.pbConnecting);
            this.Controls.Add(this.lvLog);
            this.Controls.Add(this.pbLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VPNBook Helper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbConnecting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyListView lvLog;
        private System.Windows.Forms.PictureBox pbConnecting;
        private System.Windows.Forms.Button btnNetSettings;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnGetConfigs;
        private System.Windows.Forms.Button btnGetPassword;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.ColumnHeader colMain;
        private System.Windows.Forms.Button btnAbout;
    }
}

