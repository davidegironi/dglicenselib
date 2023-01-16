
namespace DG.LicenseLib.UC
{
    partial class DGLicenseLibClientForm
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
            this.textBox_licenseinfo = new System.Windows.Forms.TextBox();
            this.label_licenseinfo = new System.Windows.Forms.Label();
            this.button_close = new System.Windows.Forms.Button();
            this.dgLicenseLibClientUC_main = new DG.LicenseLib.UC.DGLicenseLibClientUC();
            this.SuspendLayout();
            // 
            // textBox_licenseinfo
            // 
            this.textBox_licenseinfo.Location = new System.Drawing.Point(12, 258);
            this.textBox_licenseinfo.Multiline = true;
            this.textBox_licenseinfo.Name = "textBox_licenseinfo";
            this.textBox_licenseinfo.ReadOnly = true;
            this.textBox_licenseinfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_licenseinfo.Size = new System.Drawing.Size(490, 59);
            this.textBox_licenseinfo.TabIndex = 0;
            // 
            // label_licenseinfo
            // 
            this.label_licenseinfo.AutoSize = true;
            this.label_licenseinfo.Location = new System.Drawing.Point(12, 242);
            this.label_licenseinfo.Name = "label_licenseinfo";
            this.label_licenseinfo.Size = new System.Drawing.Size(107, 13);
            this.label_licenseinfo.TabIndex = 1;
            this.label_licenseinfo.Text = "Licensing Information";
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(401, 218);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 3;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // dgLicenseLibClientUC_main
            // 
            this.dgLicenseLibClientUC_main.Location = new System.Drawing.Point(15, 3);
            this.dgLicenseLibClientUC_main.Name = "dgLicenseLibClientUC_main";
            this.dgLicenseLibClientUC_main.Size = new System.Drawing.Size(490, 210);
            this.dgLicenseLibClientUC_main.TabIndex = 2;
            // 
            // DGLicenseLibClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 321);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.dgLicenseLibClientUC_main);
            this.Controls.Add(this.label_licenseinfo);
            this.Controls.Add(this.textBox_licenseinfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DGLicenseLibClientForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "License";
            this.Load += new System.EventHandler(this.DGLicenseLibClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_licenseinfo;
        private System.Windows.Forms.Label label_licenseinfo;
        private DGLicenseLibClientUC dgLicenseLibClientUC_main;
        private System.Windows.Forms.Button button_close;
    }
}