#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using DG.LicenseLib.Helpers;
using EllipticCurve;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DG.LicenseLib
{
    public static class DGLicenseLib
    {
        /// <summary>
        /// License structure
        /// </summary>
        public struct License
        {
            public Nullable<DateTime> ExpireDate { get; set; }
            public string DeviceId { get; set; }
            public string Data { get; set; }
        }

        /// <summary>
        /// Generate new key pair
        /// </summary>
        /// <param name="privateKeyPem"></param>
        /// <param name="publicKeyPem"></param>
        public static void GenerateKeypair(out string privateKeyPem, out string publicKeyPem)
        {
            PrivateKey privateKey = new PrivateKey();
            PublicKey publicKey = privateKey.publicKey();

            privateKeyPem = privateKey.toPem();
            publicKeyPem = publicKey.toPem();
        }

        /// <summary>
        /// Sign a message
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Sign(string privateKey, string message)
        {
            string ret = null;

            try
            {
                ret = Ecdsa.sign(message, PrivateKey.fromPem(privateKey)).toBase64();
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// Verify a message
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="message"></param>
        /// <param name="signatureBase64"></param>
        /// <returns></returns>
        public static bool Verify(string publicKey, string message, string signatureBase64)
        {
            bool ret = false;

            try
            {
                Signature signature = Signature.fromBase64(signatureBase64);
                ret = Ecdsa.verify(message, signature, PublicKey.fromPem(publicKey));
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// Build license payload
        /// </summary>
        /// <param name="expireDate"></param>
        /// <param name="deviceId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string BuildLicensePayload(Nullable<DateTime> expireDate, string deviceId, string data)
        {
            // validate parameters
            if (!String.IsNullOrEmpty(deviceId) && Convert.FromBase64String(deviceId).Length != 32)
                throw new ArgumentException("Invalid device id");

            // build license
            License license = new License()
            {
                ExpireDate = expireDate,
                DeviceId = deviceId,
                Data = data
            };

            // serialize license
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(License));
            string licenseXML = "";
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(writer, license);
                    licenseXML = stringWriter.ToString();
                }
            }

            // return base 64 license
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(licenseXML));
        }

        /// <summary>
        /// Read a license payload
        /// </summary>
        /// <param name="licensePayload"></param>
        /// <returns></returns>
        public static Nullable<License> ReadLicensePayload(string licensePayload)
        {
            License license = new License();

            try
            {
                // deserialize license
                string licenseXML = Encoding.UTF8.GetString(Convert.FromBase64String(licensePayload));
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(License));
                using (TextReader reader = new StringReader(licenseXML))
                {
                    license = (License)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                return null;
            }

            return license;
        }

        /// <summary>
        /// Sign a license
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="licensePayload"></param>
        /// <param name="licenseKey"></param>
        /// <returns></returns>
        public static bool SignLicensePayload(string privateKey, string licensePayload, out string licenseKey)
        {
            // check if license format is valid
            licenseKey = null;
            if (ReadLicensePayload(licensePayload) == null)
                return false;

            // sign the license
            licenseKey = Sign(privateKey, licensePayload);

            return !String.IsNullOrEmpty(licenseKey);
        }

        /// <summary>
        /// Check if a license payload is valid
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="licensePayload"></param>
        /// <param name="licenseKey"></param>
        /// <param name="deviceId"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static bool IsLicensePayloadValid(string publicKey, string licensePayload, string licenseKey, string deviceId, out Nullable<License> license)
        {
            license = null;

            // read the license
            try
            {
                license = (License)ReadLicensePayload(licensePayload);
            }
            catch
            {
                return false;
            }

            // check license expiration
            if (((License)license).ExpireDate != null && ((DateTime)((License)license).ExpireDate).Date < DateTime.UtcNow.Date)
            {
                license = null;
                return false;
            }

            // check invalid deviceid
            if (((License)license).DeviceId != null && deviceId != ((License)license).DeviceId)
            {
                license = null;
                return false;
            }

            // check license key
            if (!Verify(publicKey, licensePayload, licenseKey))
            {
                license = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if a license payload is valid
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="licensePayload"></param>
        /// <param name="licenseKey"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static bool IsLicensePayloadValid(string publicKey, string licensePayload, string licenseKey, string deviceId)
        {
            Nullable<License> license = null;
            return IsLicensePayloadValid(publicKey, licensePayload, licenseKey, deviceId, out license);
        }

        /// <summary>
        /// Pack license
        /// </summary>
        /// <param name="licensePayload"></param>
        /// <param name="licenseKey"></param>
        /// <returns></returns>
        public static string PackLicense(string licensePayload, string licenseKey)
        {
            if (String.IsNullOrEmpty(licensePayload) || String.IsNullOrEmpty(licenseKey))
                return null;

            return licensePayload + "." + licenseKey;
        }

        /// <summary>
        /// Unpack license
        /// </summary>
        /// <param name="license"></param>
        /// <param name="licensePayload"></param>
        /// <param name="licenseKey"></param>
        /// <returns></returns>
        public static bool UnPackLicense(string license, out string licensePayload, out string licenseKey)
        {
            bool ret = false;

            licensePayload = null;
            licenseKey = null;

            if (String.IsNullOrEmpty(license))
                return false;

            try
            {
                string[] splitcontent = license.Split('.');
                if (splitcontent.Length == 2)
                {
                    licensePayload = splitcontent[0];
                    licenseKey = splitcontent[1];
                    ret = true;
                }
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// Pack key pair to content
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string PackKeypair(string privateKey, string publicKey)
        {
            // pack key pair
            return privateKey + "." + publicKey;
        }

        /// <summary>
        /// Unpack key pair from content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool UnPackKeypair(string content, out string privateKey, out string publicKey)
        {
            bool ret = false;

            privateKey = null;
            publicKey = null;

            try
            {
                // split content and get keys
                string[] splitcontent = content.Split('.');
                if (splitcontent.Length == 2)
                {
                    privateKey = splitcontent[0];
                    publicKey = splitcontent[1];
                    ret = true;
                }
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// Get the public key url hash
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string GetPublicKeyUrlHash(string publicKey)
        {
            if (String.IsNullOrEmpty(publicKey))
                return null;

            // remove linefeed from public key
            publicKey = publicKey.Replace("\n", "").Replace("\r", "").Replace("\t", "");

            // get hash for the public key
            byte[] publicKeyHash = null;
            using (SHA256 sha256 = SHA256.Create())
            {
                publicKeyHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(publicKey));
            }

            return Base64Url.Encode(publicKeyHash);
        }

        /// <summary>
        /// Pack license request
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static string PackLicenseRequest(string publicKey, string deviceId)
        {
            if (String.IsNullOrEmpty(publicKey) || String.IsNullOrEmpty(deviceId))
                return null;

            return GetPublicKeyUrlHash(publicKey) + "." + deviceId;
        }

        /// <summary>
        /// Unpack license request
        /// </summary>
        /// <param name="licenseRequest"></param>
        /// <param name="publicKeyUrlHash"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static bool UnPackLicenseRequest(string licenseRequest, out string publicKeyUrlHash, out string deviceId)
        {
            bool ret = false;

            publicKeyUrlHash = null;
            deviceId = null;

            if (String.IsNullOrEmpty(licenseRequest))
                return false;

            string[] splitted = licenseRequest.Split('.');
            if (splitted.Length == 2)
            {
                publicKeyUrlHash = splitted[0];
                deviceId = splitted[1];
                ret = true;
            }

            return ret;
        }

    }
}
