#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
#if NETFRAMEWORK
using System.Web.Script.Serialization;
#else
using System.Text.Json;
#endif
using System.Windows.Forms;

namespace DG.LicenseLib.UC
{
    public partial class DGLicenseLibClientUC : UserControl
    {
        /// <summary>
        /// Initialization enabler
        /// </summary>
        private bool _initialied = false;

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
        /// Available translation keys
        /// </summary>
        public enum TranslationKey
        {
            DGLicenseLibClientFormTitle,
            DGLicenseLibClientFormButtonClose,
            DGLicenseLibClientFormLicenseInfoLabel,
            DGLicenseLibClientFormLicenseInfoContent,
            DGLicenseLibClientUIInformationTitle,
            DGLicenseLibClientUILabelLicenseRequest,
            DGLicenseLibClientUILabelLicense,
            DGLicenseLibClientUILabelExpireDate,
            DGLicenseLibClientUILabelExpireDateNone,
            DGLicenseLibClientUIButtonCopy,
            DGLicenseLibClientUIButtonSave,
            DGLicenseLibClientUIErrorTitle,
            DGLicenseLibClientUIErrorLicense,
            DGLicenseLibClientUIErrorLicenseRequest,
            DGLicenseLibClientUIErrorLicenseKey,
            DGLicenseLibClientUIErrorLicenseSavetofile,
            DGLicenseLibClientUISuccessValidation
        }

        /// <summary>
        /// Internationalization strings
        /// </summary>
        public static Dictionary<string, string> Translations = new Dictionary<string, string>()
        {
            { TranslationKey.DGLicenseLibClientFormTitle.ToString(), "License" },
            { TranslationKey.DGLicenseLibClientFormButtonClose.ToString(), "Close" },
            { TranslationKey.DGLicenseLibClientFormLicenseInfoLabel.ToString(), "License Info" },
            { TranslationKey.DGLicenseLibClientFormLicenseInfoContent.ToString(), "" },
            { TranslationKey.DGLicenseLibClientUIInformationTitle.ToString(), "Info" },
            { TranslationKey.DGLicenseLibClientUILabelLicenseRequest.ToString(), "License Request" },
            { TranslationKey.DGLicenseLibClientUILabelLicense.ToString(), "License" },
            { TranslationKey.DGLicenseLibClientUILabelExpireDate.ToString(), "Expire Date" },
            { TranslationKey.DGLicenseLibClientUILabelExpireDateNone.ToString(), "none" },
            { TranslationKey.DGLicenseLibClientUIButtonCopy.ToString(), "Copy" },
            { TranslationKey.DGLicenseLibClientUIButtonSave.ToString(), "Save" },
            { TranslationKey.DGLicenseLibClientUIErrorTitle.ToString(), "Error" },
            { TranslationKey.DGLicenseLibClientUIErrorLicense.ToString(), "Invalid license format" },
            { TranslationKey.DGLicenseLibClientUIErrorLicenseRequest.ToString(), "Invalid license request provided" },
            { TranslationKey.DGLicenseLibClientUIErrorLicenseKey.ToString(), "Invalid license key provided" },
            { TranslationKey.DGLicenseLibClientUIErrorLicenseSavetofile.ToString(), "Error saving license to file {0}" },
            { TranslationKey.DGLicenseLibClientUISuccessValidation.ToString(), "License successfully written to file {0}" },
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public DGLicenseLibClientUC()
        {
            InitializeComponent();

            //set component translations
            label_licenserequest.Text = Translations[TranslationKey.DGLicenseLibClientUILabelLicenseRequest.ToString()];
            label_license.Text = Translations[TranslationKey.DGLicenseLibClientUILabelLicense.ToString()];
            label_expiredate.Text = Translations[TranslationKey.DGLicenseLibClientUILabelExpireDate.ToString()];
            label_expiredatenone.Text = Translations[TranslationKey.DGLicenseLibClientUILabelExpireDateNone.ToString()];
            button_copy.Text = Translations[TranslationKey.DGLicenseLibClientUIButtonCopy.ToString()];
            button_save.Text = Translations[TranslationKey.DGLicenseLibClientUIButtonSave.ToString()];
        }

        /// <summary>
        /// Initialize component
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="licenseFilename"></param>
        public void Init(string publicKey, string deviceId, string licenseFilename)
        {
            // set internval variables
            this._publicKey = publicKey;
            this._deviceId = deviceId;
            this._licenseFilename = licenseFilename;

            // refresh license request
            textBox_licenserequest.Text = DGLicenseLib.PackLicenseRequest(_publicKey, _deviceId);

            // load from license file
            string licenseText = null;
            if (File.Exists(licenseFilename))
            {
                licenseText = File.ReadAllText(licenseFilename);
                textBox_license.Text = licenseText;
            }

            // set components by license
            SetComponentByLicense();

            _initialied = true;
        }

        /// <summary>
        /// Set components by license
        /// </summary>
        private void SetComponentByLicense()
        {
            label_expiredate.Visible = false;
            dateTimePicker_expiredate.Visible = false;
            label_expiredatenone.Visible = false;

            // validate license
            Nullable<DGLicenseLib.License> license = null;
            bool isvalidlicensekey = DGLicenseLibClientHelper.ValidateLicense(_publicKey, _deviceId, _licenseFilename, out license);

            if (isvalidlicensekey)
            {
                textBox_license.ReadOnly = true;
                button_save.Enabled = false;
            }
            else
                textBox_license.Text = "";

            if (isvalidlicensekey && license != null)
            {
                label_expiredate.Visible = true;
                dateTimePicker_expiredate.Visible = !(((DGLicenseLib.License)license).ExpireDate == null);
                label_expiredatenone.Visible = (((DGLicenseLib.License)license).ExpireDate == null);

                this.ActiveControl = button_save;
            }
            else
                this.ActiveControl = textBox_license;
        }

        /// <summary>
        /// Check license
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="licenseFilename"></param>
        /// <param name="deviceId"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static bool CheckLicense(string publicKey, string licenseFilename, string deviceId, out string license)
        {
            bool ret = false;

            // load from license file
            license = null;
            if (File.Exists(licenseFilename))
            {
                license = File.ReadAllText(licenseFilename);
            }

            // check license
            if (!String.IsNullOrEmpty(license))
            {
                try
                {
                    string licensePayload = null;
                    string licenseKey = null;
                    DGLicenseLib.UnPackLicense(license, out licensePayload, out licenseKey);
                    ret = DGLicenseLib.IsLicensePayloadValid(publicKey, licensePayload, licenseKey, deviceId);
                }
                catch { }
            }

            if (!ret)
                license = null;

            return ret;
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGLicenseLibClientUC_Load(object sender, EventArgs e)
        { }

        /// <summary>
        /// Set translation dictionary
        /// </summary>
        /// <param name="translations"></param>
        public static void SetTranslations(IDictionary<string, string> translations)
        {
            //set localization strings
            if (translations != null)
            {
                foreach (KeyValuePair<string, string> translation in translations)
                {
                    if (Translations.ContainsKey(translation.Key))
                        Translations[translation.Key] = translation.Value;
                }
            }
        }

        /// <summary>
        /// Get translation dictionary
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetTranslations()
        {
            return Translations;
        }

        /// <summary>
        /// Load translations from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static IDictionary<string, string> LoadTranslationsFromFile(string filename)
        {
            IDictionary<string, string> ret = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(filename))
            {
                //deserialize the file
                try
                {
                    string jsontext = File.ReadAllText(filename);
#if NETFRAMEWORK
                    Dictionary<string, string> translations = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsontext);
#else
                    Dictionary<string, string> translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsontext);
#endif
                    foreach (KeyValuePair<string, string> translation in translations)
                    {
                        if (!ret.ContainsKey(translation.Key) && Translations.ContainsKey(translation.Key))
                            ret.Add(translation.Key, translation.Value);
                    }
                }
                catch { }
            }

            //add default translations if not in files
            foreach (KeyValuePair<string, string> translation in GetTranslations())
            {
                if (!ret.ContainsKey(translation.Key))
                    ret.Add(translation.Key, translation.Value);
            }

            return ret;
        }

        /// <summary>
        /// Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGLicenseLibClientUC_Resize(object sender, EventArgs e)
        {
            // center main panel
            panel_license.Left = (panel_license.Parent.Width - panel_license.Width) / 2;
            panel_license.Top = (panel_license.Parent.Height - panel_license.Height) / 2;
        }

        /// <summary>
        /// Check if license is valid and save license to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_Click(object sender, EventArgs e)
        {
            if (!_initialied)
                return;

            // sanitize license request
            bool isvalidlicenserequest = false;
            try
            {
                string publicKeyUrlHash = null;
                string deviceId = null;
                if (textBox_licenserequest.Text != null)
                    isvalidlicenserequest = DGLicenseLib.UnPackLicenseRequest(textBox_licenserequest.Text, out publicKeyUrlHash, out deviceId);
            }
            catch { }
            if (!isvalidlicenserequest)
            {
                MessageBox.Show(Translations[TranslationKey.DGLicenseLibClientUIErrorLicenseRequest.ToString()], Translations[TranslationKey.DGLicenseLibClientUIErrorTitle.ToString()], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // unpack license
            string licensePayload = null;
            string licenseKey = null;
            try
            {
                if (textBox_license.Text != null)
                    DGLicenseLib.UnPackLicense(textBox_license.Text, out licensePayload, out licenseKey);
            }
            catch { }
            if (String.IsNullOrEmpty(licensePayload) || String.IsNullOrEmpty(licenseKey))
            {
                MessageBox.Show(Translations[TranslationKey.DGLicenseLibClientUIErrorLicense.ToString()], Translations[TranslationKey.DGLicenseLibClientUIErrorTitle.ToString()], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // validate license
            bool isvalidlicensekey = false;
            try
            {
                isvalidlicensekey = DGLicenseLib.IsLicensePayloadValid(_publicKey, licensePayload, licenseKey, _deviceId);
            }
            catch { }
            if (!isvalidlicensekey)
            {
                MessageBox.Show(Translations[TranslationKey.DGLicenseLibClientUIErrorLicenseKey.ToString()], Translations[TranslationKey.DGLicenseLibClientUIErrorTitle.ToString()], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save to file
            try
            {
                File.WriteAllText(_licenseFilename, textBox_license.Text);
            }
            catch
            {
                MessageBox.Show(String.Format(Translations[TranslationKey.DGLicenseLibClientUIErrorLicenseSavetofile.ToString()], _licenseFilename), Translations[TranslationKey.DGLicenseLibClientUIErrorTitle.ToString()], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // set components by license
            SetComponentByLicense();

            MessageBox.Show(String.Format(Translations[TranslationKey.DGLicenseLibClientUISuccessValidation.ToString()], _licenseFilename), Translations[TranslationKey.DGLicenseLibClientUIInformationTitle.ToString()], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Select all text on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_licenserequest_Click(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }

        /// <summary>
        /// Copy to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_copy_Click(object sender, EventArgs e)
        {
            if (!_initialied)
                return;

            if (!String.IsNullOrEmpty(textBox_licenserequest.Text))
                Clipboard.SetText(textBox_licenserequest.Text);
        }
    }
}
