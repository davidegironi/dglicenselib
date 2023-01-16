using NUnit.Framework;
using System;
using System.Linq;

namespace DG.LicenseLib.Test
{
    [TestFixture]
    public class DGLicenseLibTest
    {
        private string _deviceId = "SJKUe2fEFmnjZ1jNyW440HMNU8MWGEU3xQGuxd5GTLY=";

        public DGLicenseLibTest()
        { }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Test]
        public void GenerateKeypair()
        {
            string privateKey1;
            string publicKey1;
            DGLicenseLib.GenerateKeypair(out privateKey1, out publicKey1);
            Assert.IsNotEmpty(privateKey1);
            Assert.IsNotEmpty(publicKey1);

            string privateKey2;
            string publicKey2;
            DGLicenseLib.GenerateKeypair(out privateKey2, out publicKey2);

            Assert.AreNotEqual(privateKey1, privateKey2);
            Assert.AreNotEqual(publicKey1, publicKey2);
        }

        [Test]
        public void SignVerify()
        {
            string privateKey1;
            string publicKey1;
            DGLicenseLib.GenerateKeypair(out privateKey1, out publicKey1);
            string privateKey2;
            string publicKey2;
            DGLicenseLib.GenerateKeypair(out privateKey2, out publicKey2);
            Assert.AreNotEqual(privateKey1, privateKey2);
            Assert.AreNotEqual(publicKey1, publicKey2);

            string message = RandomString(32768);
            string signedMessage = DGLicenseLib.Sign(privateKey1, message);

            Assert.IsTrue(DGLicenseLib.Verify(publicKey1, message, signedMessage));

            string signedMessage2 = DGLicenseLib.Sign(privateKey2, message);
            Assert.IsFalse(DGLicenseLib.Verify(publicKey1, message, signedMessage2));
            Assert.IsTrue(DGLicenseLib.Verify(publicKey2, message, signedMessage2));
        }

        [Test]
        public void BuildLicensePayload()
        {
            string licensepayload1 = null;
            DateTime dateTime = new DateTime(2022, 02, 01);
            licensepayload1 = DGLicenseLib.BuildLicensePayload(dateTime, _deviceId, null);
            Assert.IsNotEmpty(licensepayload1);

            string licensepayload2 = null;
            licensepayload2 = DGLicenseLib.BuildLicensePayload(null, _deviceId, null);
            Assert.IsNotEmpty(licensepayload2);
            Assert.AreNotEqual(licensepayload1, licensepayload2);
        }

        [Test]
        public void ReadLicensePayload()
        {
            string licensepayload = DGLicenseLib.BuildLicensePayload(null, _deviceId, null);
            Assert.IsNotNull(DGLicenseLib.ReadLicensePayload(licensepayload));

            Assert.IsNull(DGLicenseLib.ReadLicensePayload(licensepayload + " invalid"));

            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);

            DGLicenseLib.License license = new DGLicenseLib.License();

            DateTime expireDate = DateTime.UtcNow.Date;
            license = (DGLicenseLib.License)DGLicenseLib.ReadLicensePayload(DGLicenseLib.BuildLicensePayload(expireDate, _deviceId, "additional data"));
            Assert.AreEqual(expireDate, license.ExpireDate);
            Assert.AreEqual(_deviceId, license.DeviceId);
            Assert.AreEqual("additional data", license.Data);

            license = (DGLicenseLib.License)DGLicenseLib.ReadLicensePayload(DGLicenseLib.BuildLicensePayload(null, _deviceId, null));
            Assert.AreEqual(null, license.ExpireDate);
            Assert.AreEqual(_deviceId, license.DeviceId);
            Assert.AreEqual(null, license.Data);
        }

        [Test]
        public void SignLicensePayload()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);

            string licensepayload = DGLicenseLib.BuildLicensePayload(null, _deviceId, null);

            string licenseKey = null;
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey, licensepayload, out licenseKey));
            Assert.IsNotNull(licenseKey);

            Assert.IsFalse(DGLicenseLib.SignLicensePayload(privateKey, licensepayload + " invalid", out licenseKey));
            Assert.IsNull(licenseKey);
        }

        [Test]
        public void IsLicensePayloadValid()
        {
            string privateKey1;
            string publicKey1;
            DGLicenseLib.GenerateKeypair(out privateKey1, out publicKey1);

            string privateKey2;
            string publicKey2;
            DGLicenseLib.GenerateKeypair(out privateKey2, out publicKey2);

            Nullable<DGLicenseLib.License> license = null;
            string licensepayload = null;
            string licenseKey = null;

            licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsTrue(DGLicenseLib.IsLicensePayloadValid(publicKey1, licensepayload, licenseKey, _deviceId, out license));
            Assert.NotNull(license);

            string licenseExpired = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.Date.AddDays(-10), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licenseExpired, out licenseKey));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey1, licenseExpired, licenseKey, _deviceId, out license));
            Assert.IsNull(license);

            licensepayload = DGLicenseLib.BuildLicensePayload(null, _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey2, licensepayload, licenseKey, _deviceId, out license));
            Assert.IsNull(license);

            string licenseCounterfit = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(500), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey1, licenseCounterfit, licenseKey, _deviceId, out license));
            Assert.IsNull(license);

            licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey1, licensepayload, " counterfix key", _deviceId, out license));
            Assert.IsNull(license);

            string licenseKeyOtherSoftware;
            licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey2, licensepayload, out licenseKeyOtherSoftware));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey1, licensepayload, licenseKeyOtherSoftware, _deviceId, out license));
            Assert.IsNull(license);

            licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsFalse(DGLicenseLib.IsLicensePayloadValid(publicKey1, licensepayload, licenseKey, "other device id", out license));
            Assert.IsNull(license);

            licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), null, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey1, licensepayload, out licenseKey));
            Assert.IsTrue(DGLicenseLib.IsLicensePayloadValid(publicKey1, licensepayload, licenseKey, "other device id", out license));
            Assert.NotNull(license);
        }

        [Test]
        public void PackUnPackLicense()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);

            string licenseKey = null;
            string licensepayload = DGLicenseLib.BuildLicensePayload(DateTime.UtcNow.AddDays(100), _deviceId, null);
            Assert.IsTrue(DGLicenseLib.SignLicensePayload(privateKey, licensepayload, out licenseKey));
            string license = DGLicenseLib.PackLicense(licensepayload, licenseKey);
            Assert.NotNull(license);

            string licensepayloadUnpack;
            string licenseKeyUnpack;
            Assert.IsTrue(DGLicenseLib.UnPackLicense(license, out licensepayloadUnpack, out licenseKeyUnpack));
            Assert.AreEqual(licensepayload, licensepayloadUnpack);
            Assert.AreEqual(licenseKey, licenseKeyUnpack);

            Assert.IsFalse(DGLicenseLib.UnPackLicense("invalid content", out licensepayloadUnpack, out licenseKeyUnpack));
            Assert.IsNull(licensepayloadUnpack);
            Assert.IsNull(licenseKeyUnpack);
        }

        [Test]
        public void PackUnPackKeypair()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);

            string packedkeys = DGLicenseLib.PackKeypair(privateKey, publicKey);
            Assert.NotNull(packedkeys);

            string privateKeyUnpack;
            string publicKeyUnpack;
            Assert.IsTrue(DGLicenseLib.UnPackKeypair(packedkeys, out privateKeyUnpack, out publicKeyUnpack));
            Assert.AreEqual(privateKey, privateKeyUnpack);
            Assert.AreEqual(publicKey, publicKeyUnpack);

            Assert.IsFalse(DGLicenseLib.UnPackKeypair("invalid content", out privateKeyUnpack, out publicKeyUnpack));
            Assert.IsNull(privateKeyUnpack);
            Assert.IsNull(publicKeyUnpack);
        }

        [Test]
        public void GetPublicKeyUrlHash()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);
            Assert.IsNotEmpty(privateKey);
            Assert.IsNotEmpty(publicKey);

            string publicKeyHash = DGLicenseLib.GetPublicKeyUrlHash(publicKey);
            Assert.NotNull(publicKeyHash);
        }

        [Test]
        public void PackLicenseRequest()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);
            Assert.IsNotEmpty(privateKey);
            Assert.IsNotEmpty(publicKey);

            string licenseRequest = DGLicenseLib.PackLicenseRequest(publicKey, _deviceId);
            Assert.NotNull(licenseRequest);
        }

        [Test]
        public void UnPackLicenseRequest()
        {
            string privateKey;
            string publicKey;
            DGLicenseLib.GenerateKeypair(out privateKey, out publicKey);
            Assert.IsNotEmpty(privateKey);
            Assert.IsNotEmpty(publicKey);

            string licenseRequest = DGLicenseLib.PackLicenseRequest(publicKey, _deviceId);
            string publicUrlKeyHashSplit;
            string deviceIdSplit;
            Assert.IsTrue(DGLicenseLib.UnPackLicenseRequest(licenseRequest, out publicUrlKeyHashSplit, out deviceIdSplit));
            Assert.NotNull(publicUrlKeyHashSplit);
            Assert.AreEqual(_deviceId, deviceIdSplit);

            Assert.IsFalse(DGLicenseLib.UnPackLicenseRequest(licenseRequest + ".spliterr", out publicUrlKeyHashSplit, out deviceIdSplit));
            Assert.IsNull(publicUrlKeyHashSplit);
            Assert.IsNull(deviceIdSplit);
        }
    }
}