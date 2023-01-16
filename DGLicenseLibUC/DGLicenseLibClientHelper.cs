#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.IO;

namespace DG.LicenseLib.UC
{
    public class DGLicenseLibClientHelper
    {
        /// <summary>
        /// Check license file
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="licenseFilename"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static bool CheckLicenseFile(string publicKey, string deviceId, string licenseFilename, out Nullable<DGLicenseLib.License> license)
        {
            // load from license file
            string licenseText = null;
            if (File.Exists(licenseFilename))
            {
                licenseText = File.ReadAllText(licenseFilename);
            }

            // validate license
            if (!ValidateLicense(publicKey, deviceId, licenseFilename, out license))
            {
                using (DGLicenseLibClientForm formLicense = new DGLicenseLibClientForm(publicKey, deviceId, licenseFilename))
                {
                    formLicense.ShowDialog();
                }

                return ValidateLicense(publicKey, deviceId, licenseFilename, out license);
            }

            return true;
        }

        /// <summary>
        /// Validate a license
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="licenseFilename"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static bool ValidateLicense(string publicKey, string deviceId, string licenseFilename, out Nullable<DGLicenseLib.License> license)
        {
            bool ret = false;

            license = null;

            // load from license file
            string licenseText = null;
            if (File.Exists(licenseFilename))
            {
                licenseText = File.ReadAllText(licenseFilename);
            }

            // check license
            if (!String.IsNullOrEmpty(licenseText))
            {
                try
                {
                    string licensePayload = null;
                    string licenseKey = null;
                    DGLicenseLib.UnPackLicense(licenseText, out licensePayload, out licenseKey);
                    ret = DGLicenseLib.IsLicensePayloadValid(publicKey, licensePayload, licenseKey, deviceId, out license);
                }
                catch { }
            }

            if (!ret)
                licenseText = null;

            return ret;
        }
    }
}
