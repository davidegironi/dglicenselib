#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DG.LicenseLib.Manager
{
    public partial class FormLicenseManager : Form
    {
        /// <summary>
        /// Folder for keys
        /// </summary>
        private readonly string _keypairFolder = null;

        /// <summary>
        /// License key pair file extention
        /// </summary>
        private const string KeypairFileExtention = "llk";

        /// <summary>
        /// Check if keys is loaded
        /// </summary>
        private bool _keypairLoaded = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public FormLicenseManager()
        {
            InitializeComponent();

            // load key pair
            _keypairFolder = ConfigurationManager.AppSettings["keypairFolder"];
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLicenseManager_Load(object sender, EventArgs e)
        {
            // build key pair folder if not exists
            if (!Directory.Exists(_keypairFolder))
            {
                try
                {
                    Directory.CreateDirectory(_keypairFolder);
                }
                catch
                {
                    MessageBox.Show(String.Format("Error creating key pair folder {0}", _keypairFolder), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // refresh key pair list
            PopulateKeypairCombobox();

            // uncheck expire date
            dateTimePicker_expiredate.Value = DateTime.UtcNow.Date;
            checkBox_expiredate.Checked = false;
            checkBox_expiredate_CheckedChanged(sender, e);

            // set focussed component
            this.ActiveControl = textBox_licenserequest;
        }

        /// <summary>
        /// Resized form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLicenseManager_Resize(object sender, EventArgs e)
        {
            // center main panel
            panel_main.Left = (panel_main.Parent.Width - panel_main.Width) / 2 - 10;
            panel_main.Top = (panel_main.Parent.Height - panel_main.Height) / 2 - 30;
        }

        /// <summary>
        /// Populate combobox key pair
        /// </summary>
        private void PopulateKeypairCombobox()
        {
            _keypairLoaded = false;

            // clean combobox content
            comboBox_keypairlist.Items.Clear();

            // get all key pair files
            foreach (string keypairFileName in Directory.GetFiles(_keypairFolder, "*." + KeypairFileExtention, SearchOption.TopDirectoryOnly))
            {
                string filenameWithoutExtenstion = Path.GetFileNameWithoutExtension(keypairFileName);

                // check if is a valid key pair file
                string keypairFileContent = null;
                try
                {
                    keypairFileContent = File.ReadAllText(keypairFileName);

                }
                catch { }
                string privateKeyPem;
                string publicKeyPem;
                if (!String.IsNullOrEmpty(keypairFileContent) && DGLicenseLib.UnPackKeypair(keypairFileContent, out privateKeyPem, out publicKeyPem))
                {
                    comboBox_keypairlist.Items.Add(filenameWithoutExtenstion);
                }
            }

            // unselect key pair
            comboBox_keypairlist.SelectedIndex = -1;
            comboBox_keypairlist_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Select current key pair by public key
        /// </summary>
        /// <param name="publicKeyUrlHash"></param>
        private void SelectKeypairCombobox(string publicKeyUrlHash)
        {
            string selectedItem = null;
            foreach (string item in comboBox_keypairlist.Items)
            {
                if (item.EndsWith(publicKeyUrlHash))
                {
                    selectedItem = item;
                    break;
                }
            }
            if (selectedItem != null)
                comboBox_keypairlist.SelectedItem = selectedItem;
        }

        /// <summary>
        /// Generate new key pair
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_generatekeypair_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_generatekeypairname.Text) || !Regex.Match(textBox_generatekeypairname.Text, @"^[a-zA-Z0-9_]+$").Success)
            {
                MessageBox.Show("Invlaid name, must contains letters, numbers or symbol \"_\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Directory.GetFiles(_keypairFolder, "*." + KeypairFileExtention, SearchOption.TopDirectoryOnly).FirstOrDefault(r => r.StartsWith(_keypairFolder + "\\" + textBox_generatekeypairname.Text + "=")) != null)
            {
                MessageBox.Show("A key with this name already exists, please select a new one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (MessageBox.Show("Do you want to generate a new key pair?", "Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // generate key pair
                string privateKeyPem;
                string publicKeyPem;
                DGLicenseLib.GenerateKeypair(out privateKeyPem, out publicKeyPem);
                if (String.IsNullOrEmpty(privateKeyPem) || String.IsNullOrEmpty(publicKeyPem))
                {
                    MessageBox.Show("Error generating key pair", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // get public key url hash
                string publicKeyUrlHash = DGLicenseLib.GetPublicKeyUrlHash(publicKeyPem);
                if (String.IsNullOrEmpty(publicKeyUrlHash))
                {
                    MessageBox.Show("Error generating public key hash", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // concatenate keys and create file
                string keypairFileName = textBox_generatekeypairname.Text + "=" + publicKeyUrlHash + "." + KeypairFileExtention;
                string keypairFullFileName = _keypairFolder + "\\" + keypairFileName;
                string keypairFileContent = DGLicenseLib.PackKeypair(privateKeyPem, publicKeyPem);
                if (DGLicenseLib.UnPackKeypair(keypairFileContent, out privateKeyPem, out publicKeyPem))
                {
                    try
                    {
                        File.WriteAllText(keypairFullFileName, keypairFileContent);
                    }
                    catch
                    {
                        MessageBox.Show(String.Format("Error writing key pair to file {0}", keypairFullFileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // refresh keys list
                PopulateKeypairCombobox();
                SelectKeypairCombobox(publicKeyUrlHash);
            }
        }

        /// <summary>
        /// Delete selected key pair
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_deletekeypair_Click(object sender, EventArgs e)
        {
            if (comboBox_keypairlist.SelectedIndex != -1 && _keypairLoaded)
            {
                // try to read content from selected key pair file
                string keypairFileName = comboBox_keypairlist.SelectedItem.ToString();
                string keypairFullFileName = _keypairFolder + "\\" + keypairFileName + "." + KeypairFileExtention;
                if (MessageBox.Show(String.Format("Do you want to delete the key pair file {0}", keypairFileName), "Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(keypairFullFileName);
                    }
                    catch
                    {
                        MessageBox.Show(String.Format("Error deleting key pair file {0}", keypairFullFileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // refresh keys list
                PopulateKeypairCombobox();
            }
        }

        /// <summary>
        /// Select key pair changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_keypairlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            _keypairLoaded = false;

            textBox_privatekey.Text = "";
            textBox_publickey.Text = "";

            if (comboBox_keypairlist.SelectedIndex != -1)
            {
                // try to read content from selected key pair file
                string keypairFileName = comboBox_keypairlist.SelectedItem.ToString();
                string keypairFullFileName = _keypairFolder + "\\" + keypairFileName + "." + KeypairFileExtention;
                string keypairFileContent;
                string privateKeyPem;
                string publicKeyPem;
                try
                {
                    keypairFileContent = File.ReadAllText(keypairFullFileName);
                }
                catch
                {
                    MessageBox.Show(String.Format("Error reading content from file {0}", keypairFullFileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // unpack key pair content
                if (DGLicenseLib.UnPackKeypair(keypairFileContent, out privateKeyPem, out publicKeyPem))
                {
                    textBox_privatekey.Text = privateKeyPem.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
                    textBox_publickey.Text = publicKeyPem.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
                    _keypairLoaded = true;
                }
                else
                {
                    MessageBox.Show(String.Format("Error unpacking key pair content from file {0}", keypairFullFileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// Disable expire date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_expiredate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker_expiredate.Value = DateTime.UtcNow.Date;
            dateTimePicker_expiredate.Visible = checkBox_expiredate.Checked;
        }

        /// <summary>
        /// license request changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_licenserequest_TextChanged(object sender, EventArgs e)
        {
            // unselect current key
            comboBox_keypairlist.SelectedIndex = -1;

            checkBox_expiredate.Checked = false;

            string licenseRequest = textBox_licenserequest.Text;
            if (!String.IsNullOrEmpty(licenseRequest))
            {
                // split license request
                string publicKeyUrlHash;
                string deviceId;
                if (!DGLicenseLib.UnPackLicenseRequest(licenseRequest, out publicKeyUrlHash, out deviceId))
                {
                    MessageBox.Show("Invalid license request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                textBox_publickeyurlhash.Text = publicKeyUrlHash;
                textBox_deviceid.Text = deviceId;

                // find public key for license request
                SelectKeypairCombobox(publicKeyUrlHash);
                if (!_keypairLoaded)
                {
                    MessageBox.Show("Public key not found in list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }

        /// <summary>
        /// Sign the selected license request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sign_Click(object sender, EventArgs e)
        {
            if (_keypairLoaded)
            {
                // build license
                string licensePayload = DGLicenseLib.BuildLicensePayload(
                    dateTimePicker_expiredate.Visible ? dateTimePicker_expiredate.Value.Date : (Nullable<DateTime>)null,
                    String.IsNullOrEmpty(textBox_deviceid.Text) ? null : textBox_deviceid.Text,
                    String.IsNullOrEmpty(textBox_additionaldata.Text) ? null : textBox_additionaldata.Text);

                // sign license
                string licenseKey;
                if (!DGLicenseLib.SignLicensePayload(textBox_privatekey.Text.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t"), licensePayload, out licenseKey))
                {
                    MessageBox.Show("Error signing license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // set license
                textBox_license.Text = DGLicenseLib.PackLicense(licensePayload, licenseKey);
            }

        }

        /// <summary>
        /// Select all text on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_license_Click(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.SelectAll();
            textBox.Focus();
        }
    }
}
