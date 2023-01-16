using DG.LicenseLib.DeviceId;
using DG.LicenseLib.UC;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DG.LicenseLib.ClientSample
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Insert here you public key license
        /// </summary>
        private const string LicensePublicKey = "-----BEGIN PUBLIC KEY-----\nMFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEKKAzjD8Ln5dJagBqIVWImHJH4R4VTQ9A\nfSbdjk7B7pcwZFiBP6xCV7A4fpN1C7YmgtfJ5WdO+Sjwxq1S8pwM/A==\n-----END PUBLIC KEY-----";

        /// <summary>
        /// Load a device id for this software
        /// </summary>
        private readonly string _licenseDeviceId = DGLicenseLibDeviceId.GetDeviceId();

        /// <summary>
        /// License file name
        /// </summary>
        private readonly string _licenseFilename = "license.lic";

        /// <summary>
        /// Loaded license
        /// </summary>
        private DGLicenseLib.License _license = new DGLicenseLib.License();

        private bool _testtranslations = false;
        private bool _testtranslationsFromFile = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //set localization strings
            Dictionary<string, string> translations = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> translation in DGLicenseLibClientUC.Translations)
            {
                if (!translations.ContainsKey(translation.Key))
                    translations.Add(translation.Key, "." + translation.Value);
            }
            if (_testtranslations)
            {
                DGLicenseLibClientUC.SetTranslations(translations);
            }
            if (_testtranslationsFromFile)
            {
                DGLicenseLibClientUC.SetTranslations(DGLicenseLibClientUC.LoadTranslationsFromFile("lang.json"));
            }
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        { }

        /// <summary>
        /// Shown form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            // check license and close it if it is not valid
            Nullable<DGLicenseLib.License> license = null;
            if (!DGLicenseLibClientHelper.CheckLicenseFile(LicensePublicKey, _licenseDeviceId, _licenseFilename, out license))
                this.Close();
            else
                _license = (DGLicenseLib.License)license;
        }

        /// <summary>
        /// Exit click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// License click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show license form
            new DGLicenseLibClientForm(LicensePublicKey, _licenseDeviceId, _licenseFilename).ShowDialog();
        }
    }
}
