
namespace DG.LicenseLib.UC
{
    partial class DGLicenseLibClientUC
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_copy = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_license = new System.Windows.Forms.TextBox();
            this.label_license = new System.Windows.Forms.Label();
            this.textBox_licenserequest = new System.Windows.Forms.TextBox();
            this.label_licenserequest = new System.Windows.Forms.Label();
            this.panel_license = new System.Windows.Forms.Panel();
            this.dateTimePicker_expiredate = new System.Windows.Forms.DateTimePicker();
            this.label_expiredate = new System.Windows.Forms.Label();
            this.label_expiredatenone = new System.Windows.Forms.Label();
            this.panel_license.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_copy
            // 
            this.button_copy.Location = new System.Drawing.Point(362, 46);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(75, 23);
            this.button_copy.TabIndex = 6;
            this.button_copy.Text = "Copy";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(362, 155);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 4;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_license
            // 
            this.textBox_license.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_license.Location = new System.Drawing.Point(6, 88);
            this.textBox_license.Multiline = true;
            this.textBox_license.Name = "textBox_license";
            this.textBox_license.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_license.Size = new System.Drawing.Size(350, 90);
            this.textBox_license.TabIndex = 3;
            // 
            // label_license
            // 
            this.label_license.AutoSize = true;
            this.label_license.Location = new System.Drawing.Point(3, 72);
            this.label_license.Name = "label_license";
            this.label_license.Size = new System.Drawing.Size(44, 13);
            this.label_license.TabIndex = 2;
            this.label_license.Text = "License";
            // 
            // textBox_licenserequest
            // 
            this.textBox_licenserequest.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_licenserequest.Location = new System.Drawing.Point(6, 19);
            this.textBox_licenserequest.Multiline = true;
            this.textBox_licenserequest.Name = "textBox_licenserequest";
            this.textBox_licenserequest.ReadOnly = true;
            this.textBox_licenserequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_licenserequest.Size = new System.Drawing.Size(350, 50);
            this.textBox_licenserequest.TabIndex = 1;
            this.textBox_licenserequest.Click += new System.EventHandler(this.textBox_licenserequest_Click);
            // 
            // label_licenserequest
            // 
            this.label_licenserequest.AutoSize = true;
            this.label_licenserequest.Location = new System.Drawing.Point(3, 3);
            this.label_licenserequest.Name = "label_licenserequest";
            this.label_licenserequest.Size = new System.Drawing.Size(87, 13);
            this.label_licenserequest.TabIndex = 0;
            this.label_licenserequest.Text = "License Request";
            // 
            // panel_license
            // 
            this.panel_license.Controls.Add(this.label_expiredatenone);
            this.panel_license.Controls.Add(this.dateTimePicker_expiredate);
            this.panel_license.Controls.Add(this.label_expiredate);
            this.panel_license.Controls.Add(this.button_copy);
            this.panel_license.Controls.Add(this.label_licenserequest);
            this.panel_license.Controls.Add(this.button_save);
            this.panel_license.Controls.Add(this.textBox_licenserequest);
            this.panel_license.Controls.Add(this.textBox_license);
            this.panel_license.Controls.Add(this.label_license);
            this.panel_license.Location = new System.Drawing.Point(4, 4);
            this.panel_license.Name = "panel_license";
            this.panel_license.Size = new System.Drawing.Size(442, 208);
            this.panel_license.TabIndex = 1;
            // 
            // dateTimePicker_expiredate
            // 
            this.dateTimePicker_expiredate.Enabled = false;
            this.dateTimePicker_expiredate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_expiredate.Location = new System.Drawing.Point(256, 184);
            this.dateTimePicker_expiredate.Name = "dateTimePicker_expiredate";
            this.dateTimePicker_expiredate.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker_expiredate.TabIndex = 9;
            // 
            // label_expiredate
            // 
            this.label_expiredate.Location = new System.Drawing.Point(6, 187);
            this.label_expiredate.Name = "label_expiredate";
            this.label_expiredate.Size = new System.Drawing.Size(244, 13);
            this.label_expiredate.TabIndex = 8;
            this.label_expiredate.Text = "Expire Date";
            this.label_expiredate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_expiredatenone
            // 
            this.label_expiredatenone.Location = new System.Drawing.Point(256, 187);
            this.label_expiredatenone.Name = "label_expiredatenone";
            this.label_expiredatenone.Size = new System.Drawing.Size(100, 13);
            this.label_expiredatenone.TabIndex = 10;
            this.label_expiredatenone.Text = "none";
            this.label_expiredatenone.Visible = false;
            // 
            // DGLicenseLibClientUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_license);
            this.Name = "DGLicenseLibClientUC";
            this.Size = new System.Drawing.Size(460, 269);
            this.Load += new System.EventHandler(this.DGLicenseLibClientUC_Load);
            this.Resize += new System.EventHandler(this.DGLicenseLibClientUC_Resize);
            this.panel_license.ResumeLayout(false);
            this.panel_license.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_license;
        private System.Windows.Forms.Label label_license;
        private System.Windows.Forms.TextBox textBox_licenserequest;
        private System.Windows.Forms.Label label_licenserequest;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_copy;
        private System.Windows.Forms.Panel panel_license;
        private System.Windows.Forms.DateTimePicker dateTimePicker_expiredate;
        private System.Windows.Forms.Label label_expiredate;
        private System.Windows.Forms.Label label_expiredatenone;
    }
}
