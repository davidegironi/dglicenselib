#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Windows.Forms;

namespace DG.LicenseLib.UC
{
    public partial class DGLicenseLibClientForm : Form
    {
        /// <summary>
        /// Public key for validation
        /// </summary>
        private string _publicKey = null;

        /// <summary>
        /// Device id in use
        /// </summary>
        private string _deviceId = null;

        /// <summary>
        /// License filename
        /// </summary>
        private string _licenseFilename = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public DGLicenseLibClientForm()
        {
            InitializeComponent();

            // set translation values
            this.Text = DGLicenseLibClientUC.Translations[DGLicenseLibClientUC.TranslationKey.DGLicenseLibClientFormTitle.ToString()];
            button_close.Text = DGLicenseLibClientUC.Translations[DGLicenseLibClientUC.TranslationKey.DGLicenseLibClientFormButtonClose.ToString()];
            if (!String.IsNullOrEmpty(DGLicenseLibClientUC.Translations[DGLicenseLibClientUC.TranslationKey.DGLicenseLibClientFormLicenseInfoContent.ToString()]))
            {
                label_licenseinfo.Text = DGLicenseLibClientUC.Translations[DGLicenseLibClientUC.TranslationKey.DGLicenseLibClientFormLicenseInfoLabel.ToString()];
                textBox_licenseinfo.Text = DGLicenseLibClientUC.Translations[DGLicenseLibClientUC.TranslationKey.DGLicenseLibClientFormLicenseInfoContent.ToString()];
            }
            else
            {
                label_licenseinfo.Visible = false;
                textBox_licenseinfo.Visible = false;

                this.Height = this.Height - 70;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="licenseFilename"></param>
        public DGLicenseLibClientForm(string publicKey, string deviceId, string licenseFilename)
            : this()
        {
            this._publicKey = publicKey;
            this._deviceId = deviceId;
            this._licenseFilename = licenseFilename;

            // initialize component
            dgLicenseLibClientUC_main.Init(_publicKey, _deviceId, _licenseFilename);
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGLicenseLibClientForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = dgLicenseLibClientUC_main;
        }

        /// <summary>
        /// Close click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
