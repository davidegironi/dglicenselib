
namespace DG.LicenseLib.Manager
{
    partial class FormLicenseManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicenseManager));
            this.groupBox_keypair = new System.Windows.Forms.GroupBox();
            this.textBox_generatekeypairname = new System.Windows.Forms.TextBox();
            this.label_generatekeypairname = new System.Windows.Forms.Label();
            this.textBox_publickey = new System.Windows.Forms.TextBox();
            this.label_publickey = new System.Windows.Forms.Label();
            this.textBox_privatekey = new System.Windows.Forms.TextBox();
            this.label_privatekey = new System.Windows.Forms.Label();
            this.button_generatekeypair = new System.Windows.Forms.Button();
            this.button_deletekeypair = new System.Windows.Forms.Button();
            this.comboBox_keypairlist = new System.Windows.Forms.ComboBox();
            this.label_keypairlist = new System.Windows.Forms.Label();
            this.groupBox_license = new System.Windows.Forms.GroupBox();
            this.label_additionaldata = new System.Windows.Forms.Label();
            this.textBox_additionaldata = new System.Windows.Forms.TextBox();
            this.label_license = new System.Windows.Forms.Label();
            this.textBox_license = new System.Windows.Forms.TextBox();
            this.checkBox_expiredate = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_expiredate = new System.Windows.Forms.DateTimePicker();
            this.textBox_deviceid = new System.Windows.Forms.TextBox();
            this.label_deviceid = new System.Windows.Forms.Label();
            this.textBox_publickeyurlhash = new System.Windows.Forms.TextBox();
            this.label_publickeyurlhash = new System.Windows.Forms.Label();
            this.label_expiredate = new System.Windows.Forms.Label();
            this.button_sign = new System.Windows.Forms.Button();
            this.textBox_licenserequest = new System.Windows.Forms.TextBox();
            this.label_licenserequest = new System.Windows.Forms.Label();
            this.panel_main = new System.Windows.Forms.Panel();
            this.groupBox_keypair.SuspendLayout();
            this.groupBox_license.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_keypair
            // 
            this.groupBox_keypair.Controls.Add(this.textBox_generatekeypairname);
            this.groupBox_keypair.Controls.Add(this.label_generatekeypairname);
            this.groupBox_keypair.Controls.Add(this.textBox_publickey);
            this.groupBox_keypair.Controls.Add(this.label_publickey);
            this.groupBox_keypair.Controls.Add(this.textBox_privatekey);
            this.groupBox_keypair.Controls.Add(this.label_privatekey);
            this.groupBox_keypair.Controls.Add(this.button_generatekeypair);
            this.groupBox_keypair.Controls.Add(this.button_deletekeypair);
            this.groupBox_keypair.Controls.Add(this.comboBox_keypairlist);
            this.groupBox_keypair.Controls.Add(this.label_keypairlist);
            this.groupBox_keypair.Location = new System.Drawing.Point(5, 3);
            this.groupBox_keypair.Name = "groupBox_keypair";
            this.groupBox_keypair.Size = new System.Drawing.Size(355, 322);
            this.groupBox_keypair.TabIndex = 0;
            this.groupBox_keypair.TabStop = false;
            this.groupBox_keypair.Text = "Keys";
            // 
            // textBox_generatekeypairname
            // 
            this.textBox_generatekeypairname.Location = new System.Drawing.Point(168, 81);
            this.textBox_generatekeypairname.Name = "textBox_generatekeypairname";
            this.textBox_generatekeypairname.Size = new System.Drawing.Size(100, 20);
            this.textBox_generatekeypairname.TabIndex = 12;
            // 
            // label_generatekeypairname
            // 
            this.label_generatekeypairname.AutoSize = true;
            this.label_generatekeypairname.Location = new System.Drawing.Point(127, 84);
            this.label_generatekeypairname.Name = "label_generatekeypairname";
            this.label_generatekeypairname.Size = new System.Drawing.Size(35, 13);
            this.label_generatekeypairname.TabIndex = 11;
            this.label_generatekeypairname.Text = "Name";
            // 
            // textBox_publickey
            // 
            this.textBox_publickey.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_publickey.Location = new System.Drawing.Point(9, 225);
            this.textBox_publickey.Multiline = true;
            this.textBox_publickey.Name = "textBox_publickey";
            this.textBox_publickey.ReadOnly = true;
            this.textBox_publickey.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_publickey.Size = new System.Drawing.Size(340, 90);
            this.textBox_publickey.TabIndex = 7;
            // 
            // label_publickey
            // 
            this.label_publickey.AutoSize = true;
            this.label_publickey.Location = new System.Drawing.Point(6, 207);
            this.label_publickey.Name = "label_publickey";
            this.label_publickey.Size = new System.Drawing.Size(57, 13);
            this.label_publickey.TabIndex = 6;
            this.label_publickey.Text = "Public Key";
            // 
            // textBox_privatekey
            // 
            this.textBox_privatekey.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_privatekey.Location = new System.Drawing.Point(9, 114);
            this.textBox_privatekey.Multiline = true;
            this.textBox_privatekey.Name = "textBox_privatekey";
            this.textBox_privatekey.ReadOnly = true;
            this.textBox_privatekey.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_privatekey.Size = new System.Drawing.Size(340, 90);
            this.textBox_privatekey.TabIndex = 5;
            // 
            // label_privatekey
            // 
            this.label_privatekey.AutoSize = true;
            this.label_privatekey.Location = new System.Drawing.Point(6, 98);
            this.label_privatekey.Name = "label_privatekey";
            this.label_privatekey.Size = new System.Drawing.Size(61, 13);
            this.label_privatekey.TabIndex = 4;
            this.label_privatekey.Text = "Private Key";
            // 
            // button_generatekeypair
            // 
            this.button_generatekeypair.Location = new System.Drawing.Point(274, 79);
            this.button_generatekeypair.Name = "button_generatekeypair";
            this.button_generatekeypair.Size = new System.Drawing.Size(75, 23);
            this.button_generatekeypair.TabIndex = 3;
            this.button_generatekeypair.Text = "Generate";
            this.button_generatekeypair.UseVisualStyleBackColor = true;
            this.button_generatekeypair.Click += new System.EventHandler(this.button_generatekeypair_Click);
            // 
            // button_deletekeypair
            // 
            this.button_deletekeypair.Location = new System.Drawing.Point(274, 50);
            this.button_deletekeypair.Name = "button_deletekeypair";
            this.button_deletekeypair.Size = new System.Drawing.Size(75, 23);
            this.button_deletekeypair.TabIndex = 2;
            this.button_deletekeypair.Text = "Delete";
            this.button_deletekeypair.UseVisualStyleBackColor = true;
            this.button_deletekeypair.Click += new System.EventHandler(this.button_deletekeypair_Click);
            // 
            // comboBox_keypairlist
            // 
            this.comboBox_keypairlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_keypairlist.FormattingEnabled = true;
            this.comboBox_keypairlist.Location = new System.Drawing.Point(56, 23);
            this.comboBox_keypairlist.Name = "comboBox_keypairlist";
            this.comboBox_keypairlist.Size = new System.Drawing.Size(293, 21);
            this.comboBox_keypairlist.TabIndex = 1;
            this.comboBox_keypairlist.SelectedIndexChanged += new System.EventHandler(this.comboBox_keypairlist_SelectedIndexChanged);
            // 
            // label_keypairlist
            // 
            this.label_keypairlist.AutoSize = true;
            this.label_keypairlist.Location = new System.Drawing.Point(6, 26);
            this.label_keypairlist.Name = "label_keypairlist";
            this.label_keypairlist.Size = new System.Drawing.Size(25, 13);
            this.label_keypairlist.TabIndex = 0;
            this.label_keypairlist.Text = "Key";
            // 
            // groupBox_license
            // 
            this.groupBox_license.Controls.Add(this.label_additionaldata);
            this.groupBox_license.Controls.Add(this.textBox_additionaldata);
            this.groupBox_license.Controls.Add(this.label_license);
            this.groupBox_license.Controls.Add(this.textBox_license);
            this.groupBox_license.Controls.Add(this.checkBox_expiredate);
            this.groupBox_license.Controls.Add(this.dateTimePicker_expiredate);
            this.groupBox_license.Controls.Add(this.textBox_deviceid);
            this.groupBox_license.Controls.Add(this.label_deviceid);
            this.groupBox_license.Controls.Add(this.textBox_publickeyurlhash);
            this.groupBox_license.Controls.Add(this.label_publickeyurlhash);
            this.groupBox_license.Controls.Add(this.label_expiredate);
            this.groupBox_license.Controls.Add(this.button_sign);
            this.groupBox_license.Controls.Add(this.textBox_licenserequest);
            this.groupBox_license.Controls.Add(this.label_licenserequest);
            this.groupBox_license.Location = new System.Drawing.Point(366, 3);
            this.groupBox_license.Name = "groupBox_license";
            this.groupBox_license.Size = new System.Drawing.Size(355, 380);
            this.groupBox_license.TabIndex = 1;
            this.groupBox_license.TabStop = false;
            this.groupBox_license.Text = "License";
            // 
            // label_additionaldata
            // 
            this.label_additionaldata.AutoSize = true;
            this.label_additionaldata.Location = new System.Drawing.Point(6, 202);
            this.label_additionaldata.Name = "label_additionaldata";
            this.label_additionaldata.Size = new System.Drawing.Size(79, 13);
            this.label_additionaldata.TabIndex = 20;
            this.label_additionaldata.Text = "Additional Data";
            // 
            // textBox_additionaldata
            // 
            this.textBox_additionaldata.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_additionaldata.Location = new System.Drawing.Point(9, 218);
            this.textBox_additionaldata.Multiline = true;
            this.textBox_additionaldata.Name = "textBox_additionaldata";
            this.textBox_additionaldata.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_additionaldata.Size = new System.Drawing.Size(342, 50);
            this.textBox_additionaldata.TabIndex = 19;
            // 
            // label_license
            // 
            this.label_license.AutoSize = true;
            this.label_license.Location = new System.Drawing.Point(6, 287);
            this.label_license.Name = "label_license";
            this.label_license.Size = new System.Drawing.Size(44, 13);
            this.label_license.TabIndex = 18;
            this.label_license.Text = "License";
            // 
            // textBox_license
            // 
            this.textBox_license.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_license.Location = new System.Drawing.Point(9, 303);
            this.textBox_license.Multiline = true;
            this.textBox_license.Name = "textBox_license";
            this.textBox_license.ReadOnly = true;
            this.textBox_license.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_license.Size = new System.Drawing.Size(342, 70);
            this.textBox_license.TabIndex = 17;
            this.textBox_license.Click += new System.EventHandler(this.textBox_license_Click);
            // 
            // checkBox_expiredate
            // 
            this.checkBox_expiredate.AutoSize = true;
            this.checkBox_expiredate.Location = new System.Drawing.Point(74, 180);
            this.checkBox_expiredate.Name = "checkBox_expiredate";
            this.checkBox_expiredate.Size = new System.Drawing.Size(15, 14);
            this.checkBox_expiredate.TabIndex = 14;
            this.checkBox_expiredate.UseVisualStyleBackColor = true;
            this.checkBox_expiredate.CheckedChanged += new System.EventHandler(this.checkBox_expiredate_CheckedChanged);
            // 
            // dateTimePicker_expiredate
            // 
            this.dateTimePicker_expiredate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_expiredate.Location = new System.Drawing.Point(95, 176);
            this.dateTimePicker_expiredate.Name = "dateTimePicker_expiredate";
            this.dateTimePicker_expiredate.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker_expiredate.TabIndex = 13;
            // 
            // textBox_deviceid
            // 
            this.textBox_deviceid.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_deviceid.Location = new System.Drawing.Point(9, 150);
            this.textBox_deviceid.Name = "textBox_deviceid";
            this.textBox_deviceid.Size = new System.Drawing.Size(342, 20);
            this.textBox_deviceid.TabIndex = 12;
            // 
            // label_deviceid
            // 
            this.label_deviceid.AutoSize = true;
            this.label_deviceid.Location = new System.Drawing.Point(6, 134);
            this.label_deviceid.Name = "label_deviceid";
            this.label_deviceid.Size = new System.Drawing.Size(53, 13);
            this.label_deviceid.TabIndex = 11;
            this.label_deviceid.Text = "Device Id";
            // 
            // textBox_publickeyurlhash
            // 
            this.textBox_publickeyurlhash.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_publickeyurlhash.Location = new System.Drawing.Point(9, 111);
            this.textBox_publickeyurlhash.Name = "textBox_publickeyurlhash";
            this.textBox_publickeyurlhash.ReadOnly = true;
            this.textBox_publickeyurlhash.Size = new System.Drawing.Size(342, 20);
            this.textBox_publickeyurlhash.TabIndex = 10;
            // 
            // label_publickeyurlhash
            // 
            this.label_publickeyurlhash.AutoSize = true;
            this.label_publickeyurlhash.Location = new System.Drawing.Point(6, 95);
            this.label_publickeyurlhash.Name = "label_publickeyurlhash";
            this.label_publickeyurlhash.Size = new System.Drawing.Size(101, 13);
            this.label_publickeyurlhash.TabIndex = 9;
            this.label_publickeyurlhash.Text = "Public Key Url Hash";
            // 
            // label_expiredate
            // 
            this.label_expiredate.AutoSize = true;
            this.label_expiredate.Location = new System.Drawing.Point(6, 179);
            this.label_expiredate.Name = "label_expiredate";
            this.label_expiredate.Size = new System.Drawing.Size(62, 13);
            this.label_expiredate.TabIndex = 8;
            this.label_expiredate.Text = "Expire Date";
            // 
            // button_sign
            // 
            this.button_sign.Location = new System.Drawing.Point(276, 274);
            this.button_sign.Name = "button_sign";
            this.button_sign.Size = new System.Drawing.Size(75, 23);
            this.button_sign.TabIndex = 7;
            this.button_sign.Text = "Sign";
            this.button_sign.UseVisualStyleBackColor = true;
            this.button_sign.Click += new System.EventHandler(this.button_sign_Click);
            // 
            // textBox_licenserequest
            // 
            this.textBox_licenserequest.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_licenserequest.Location = new System.Drawing.Point(9, 42);
            this.textBox_licenserequest.Multiline = true;
            this.textBox_licenserequest.Name = "textBox_licenserequest";
            this.textBox_licenserequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_licenserequest.Size = new System.Drawing.Size(342, 50);
            this.textBox_licenserequest.TabIndex = 6;
            this.textBox_licenserequest.TextChanged += new System.EventHandler(this.textBox_licenserequest_TextChanged);
            // 
            // label_licenserequest
            // 
            this.label_licenserequest.AutoSize = true;
            this.label_licenserequest.Location = new System.Drawing.Point(6, 26);
            this.label_licenserequest.Name = "label_licenserequest";
            this.label_licenserequest.Size = new System.Drawing.Size(87, 13);
            this.label_licenserequest.TabIndex = 1;
            this.label_licenserequest.Text = "License Request";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.groupBox_keypair);
            this.panel_main.Controls.Add(this.groupBox_license);
            this.panel_main.Location = new System.Drawing.Point(7, 6);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(725, 388);
            this.panel_main.TabIndex = 2;
            // 
            // FormLicenseManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel_main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLicenseManager";
            this.Text = "License Manager";
            this.Load += new System.EventHandler(this.FormLicenseManager_Load);
            this.Resize += new System.EventHandler(this.FormLicenseManager_Resize);
            this.groupBox_keypair.ResumeLayout(false);
            this.groupBox_keypair.PerformLayout();
            this.groupBox_license.ResumeLayout(false);
            this.groupBox_license.PerformLayout();
            this.panel_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_keypair;
        private System.Windows.Forms.Label label_keypairlist;
        private System.Windows.Forms.ComboBox comboBox_keypairlist;
        private System.Windows.Forms.Button button_generatekeypair;
        private System.Windows.Forms.Button button_deletekeypair;
        private System.Windows.Forms.TextBox textBox_publickey;
        private System.Windows.Forms.Label label_publickey;
        private System.Windows.Forms.TextBox textBox_privatekey;
        private System.Windows.Forms.Label label_privatekey;
        private System.Windows.Forms.GroupBox groupBox_license;
        private System.Windows.Forms.Label label_licenserequest;
        private System.Windows.Forms.TextBox textBox_licenserequest;
        private System.Windows.Forms.Label label_expiredate;
        private System.Windows.Forms.Button button_sign;
        private System.Windows.Forms.TextBox textBox_publickeyurlhash;
        private System.Windows.Forms.Label label_publickeyurlhash;
        private System.Windows.Forms.TextBox textBox_deviceid;
        private System.Windows.Forms.Label label_deviceid;
        private System.Windows.Forms.DateTimePicker dateTimePicker_expiredate;
        private System.Windows.Forms.CheckBox checkBox_expiredate;
        private System.Windows.Forms.Label label_license;
        private System.Windows.Forms.TextBox textBox_license;
        private System.Windows.Forms.Label label_generatekeypairname;
        private System.Windows.Forms.TextBox textBox_generatekeypairname;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.Label label_additionaldata;
        private System.Windows.Forms.TextBox textBox_additionaldata;
    }
}