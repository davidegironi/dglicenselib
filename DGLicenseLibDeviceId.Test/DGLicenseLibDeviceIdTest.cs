using DG.LicenseLib.DeviceId;
using DG.LicenseLib.DeviceId.Helpers;
using NUnit.Framework;

namespace DG.LicenseLib.Test
{
    [TestFixture]
    public class DGLicenseLibDeviceIdTest
    {
        public DGLicenseLibDeviceIdTest()
        { }

        [Test]
        public void GetOSIs()
        {
            int value =
                (GetOSInfo.IsWindows ? 1 : 0) +
                (GetOSInfo.IsLinux ? 1 : 0) +
                (GetOSInfo.IsMacOS ? 1 : 0);
            Assert.AreEqual(1, value);
        }

        [Test]
        public void GetOSVersion()
        {
            string value = GetOSInfo.Version;
            Assert.NotNull(value);
        }

        [Test]
        public void GetMachineId()
        {
            string value = DGLicenseLibDeviceId.GetMachineId();
            Assert.NotNull(value);
        }

        [Test]
        public void GetMotherboardSerialNumber()
        {
            string value = DGLicenseLibDeviceId.GetMotherboardSerialNumber();
            Assert.NotNull(value);
        }

        [Test]
        public void GetCPUInfo()
        {
            string value = DGLicenseLibDeviceId.GetCPUInfo();
            Assert.NotNull(value);
        }

        [Test]
        public void GetDeviceId()
        {
            string value = DGLicenseLibDeviceId.GetDeviceId();
            Assert.NotNull(value);
        }
    }
}