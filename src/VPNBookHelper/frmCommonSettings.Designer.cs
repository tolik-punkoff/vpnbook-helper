namespace VPNBookHelper
{
    partial class frmCommonSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommonSettings));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dlgOutputDir = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSelectMask = new System.Windows.Forms.TextBox();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectOutputDir = new System.Windows.Forms.Button();
            this.txtAuthFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectAuthFile = new System.Windows.Forms.Button();
            this.btnVPNOptionsList = new System.Windows.Forms.Button();
            this.dlgAuthFile = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(393, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(4, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dlgOutputDir
            // 
            this.dlgOutputDir.Description = "Выберите папку для сохранения файлов OpenVPN";
            this.dlgOutputDir.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Адрес сайта (не меняйте если не уверены):";
            // 
            // txtPageAddr
            // 
            this.txtPageAddr.Location = new System.Drawing.Point(4, 21);
            this.txtPageAddr.Name = "txtPageAddr";
            this.txtPageAddr.Size = new System.Drawing.Size(461, 20);
            this.txtPageAddr.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Маска файлов конфигов для выбора:";
            // 
            // txtSelectMask
            // 
            this.txtSelectMask.Location = new System.Drawing.Point(4, 62);
            this.txtSelectMask.Name = "txtSelectMask";
            this.txtSelectMask.Size = new System.Drawing.Size(260, 20);
            this.txtSelectMask.TabIndex = 2;
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Location = new System.Drawing.Point(4, 103);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.ReadOnly = true;
            this.txtOutputDir.Size = new System.Drawing.Size(383, 20);
            this.txtOutputDir.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Каталог для сохранения конфигов:";
            // 
            // btnSelectOutputDir
            // 
            this.btnSelectOutputDir.Location = new System.Drawing.Point(390, 101);
            this.btnSelectOutputDir.Name = "btnSelectOutputDir";
            this.btnSelectOutputDir.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutputDir.TabIndex = 4;
            this.btnSelectOutputDir.Text = "Выбрать...";
            this.btnSelectOutputDir.UseVisualStyleBackColor = true;
            this.btnSelectOutputDir.Click += new System.EventHandler(this.btnSelectOutputDir_Click);
            // 
            // txtAuthFile
            // 
            this.txtAuthFile.Location = new System.Drawing.Point(4, 144);
            this.txtAuthFile.Name = "txtAuthFile";
            this.txtAuthFile.ReadOnly = true;
            this.txtAuthFile.Size = new System.Drawing.Size(383, 20);
            this.txtAuthFile.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Путь к файлу авторизации:";
            // 
            // btnSelectAuthFile
            // 
            this.btnSelectAuthFile.Location = new System.Drawing.Point(390, 142);
            this.btnSelectAuthFile.Name = "btnSelectAuthFile";
            this.btnSelectAuthFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAuthFile.TabIndex = 24;
            this.btnSelectAuthFile.Text = "Выбрать...";
            this.btnSelectAuthFile.UseVisualStyleBackColor = true;
            this.btnSelectAuthFile.Click += new System.EventHandler(this.btnSelectAuthFile_Click);
            // 
            // btnVPNOptionsList
            // 
            this.btnVPNOptionsList.Location = new System.Drawing.Point(4, 170);
            this.btnVPNOptionsList.Name = "btnVPNOptionsList";
            this.btnVPNOptionsList.Size = new System.Drawing.Size(132, 36);
            this.btnVPNOptionsList.TabIndex = 6;
            this.btnVPNOptionsList.Text = "Список добавляемых опций...";
            this.btnVPNOptionsList.UseVisualStyleBackColor = true;
            this.btnVPNOptionsList.Click += new System.EventHandler(this.btnVPNOptionsList_Click);
            // 
            // dlgAuthFile
            // 
            this.dlgAuthFile.Filter = "Auth files|*.auth|Text files|*.txt|All files|*.*";
            // 
            // frmCommonSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 248);
            this.Controls.Add(this.btnVPNOptionsList);
            this.Controls.Add(this.btnSelectAuthFile);
            this.Controls.Add(this.btnSelectOutputDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtAuthFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.txtSelectMask);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPageAddr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCommonSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Общие настройки";
            this.Load += new System.EventHandler(this.frmCommonSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FolderBrowserDialog dlgOutputDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPageAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSelectMask;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectOutputDir;
        private System.Windows.Forms.TextBox txtAuthFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectAuthFile;
        private System.Windows.Forms.Button btnVPNOptionsList;
        private System.Windows.Forms.SaveFileDialog dlgAuthFile;
    }
}