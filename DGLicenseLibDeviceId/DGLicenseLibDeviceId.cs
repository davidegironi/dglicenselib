#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using DG.LicenseLib.DeviceId.Helpers;
using Microsoft.Management.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DG.LicenseLib.DeviceId
{
    public static class DGLicenseLibDeviceId
    {

        /// <summary>
        /// Get Machine Id
        /// </summary>
        /// <returns></returns>
        public static string GetMachineId()
        {
            string machineId = null;

            if (GetOSInfo.IsWindows)
            {
#if NETFRAMEWORK
                machineId = GetValueWindowsRegistry(Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32, RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\SQMClient", "MachineId");
#else
                machineId = GetValueWindowsRegistry(RegistryView.Default, RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\SQMClient", "MachineId");
#endif
            }
            else if (GetOSInfo.IsLinux)
            {
                machineId = GetValueLinux("/var/lib/dbus/machine-id", false);
                if (String.IsNullOrEmpty(machineId))
                    machineId = GetValueLinux("/etc/machine-id", false);
            }
            else if (GetOSInfo.IsMacOS)
                machineId = GetValueMacOS($"-c \"{"ioreg -d2 -c IOPlatformExpertDevice | grep IOPlatformUUID | sed 's/.*= //' | sed 's/\"//g'".Replace("\"", "\\\"")}\"");

            return machineId;
        }

        /// <summary>
        /// Get CPU information
        /// </summary>
        /// <returns></returns>
        public static string GetCPUInfo()
        {
            if (GetOSInfo.IsWindows)
                return GetValueWindowsCim("Win32_Processor", "ProcessorId");
            else if (GetOSInfo.IsLinux)
                return GetValueLinux("/proc/cpuinfo", true);
            else if (GetOSInfo.IsMacOS)
                return GetValueMacOS($"-c \"{"sysctl -n machdep.cpu.brand_string".Replace("\"", "\\\"")}\"");

            return null;
        }

        /// <summary>
        /// Get Motherboard Serial Number
        /// </summary>
        /// <returns></returns>
        public static string GetMotherboardSerialNumber()
        {
            if (GetOSInfo.IsWindows)
                return GetValueWindowsCim("Win32_BaseBoard", "SerialNumber");
            else if (GetOSInfo.IsLinux)
                return GetValueLinux("/sys/class/dmi/id/board_serial", false);
            else if (GetOSInfo.IsMacOS)
                return GetValueMacOS($"-c \"{"ioreg -l | grep IOPlatformSerialNumber | sed 's/.*= //' | sed 's/\"//g'".Replace("\"", "\\\"")}\"");

            return null;
        }

        /// <summary>
        /// Execute a command and return the value
        /// </summary>
        /// <param name="shell"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private static string CommandExecutor(string shell, string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = shell;
            processStartInfo.Arguments = command;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;

            string processReturn = null;
            Process process = Process.Start(processStartInfo);
            if (process != null)
            {
                process.WaitForExit();
                processReturn = process.StandardOutput.ReadToEnd();
            }

            return processReturn.Trim('\r').Trim('\n').TrimEnd().TrimStart();
        }

        /// <summary>
        /// Get component value running a bash command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string GetValueMacOS(string command)
        {
            return CommandExecutor("/bin/bash", command);
        }

        /// <summary>
        /// Gets the component value opening file content
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hashContent"></param>
        /// <returns></returns>
        private static string GetValueLinux(string path, bool hashContent)
        {
            if (!File.Exists(path))
                return null;

            try
            {
                string fileContent;
                using (StreamReader file = File.OpenText(path))
                {
                    fileContent = file.ReadToEnd();
                }
                fileContent = fileContent.Trim();

                if (!hashContent)
                    return fileContent;

                byte[] fileContentHash;
                using (MD5 md5 = MD5.Create())
                {
                    fileContentHash = md5.ComputeHash(Encoding.ASCII.GetBytes(fileContent));
                }
                return BitConverter.ToString(fileContentHash).Replace("-", "").ToUpper();
            }
            catch
            { }

            return null;
        }

        /// <summary>
        /// Gets the component value using CimSession
        /// </summary>
        /// <param name="className"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string GetValueWindowsCim(string className, string propertyName)
        {
            List<string> values = new List<string>();

            try
            {
                using (CimSession session = CimSession.Create(null))
                {
                    IEnumerable<CimInstance> instances = session.QueryInstances(@"root\cimv2", "WQL", $"SELECT {propertyName} FROM {className}");
                    foreach (CimInstance instance in instances)
                    {
                        try
                        {
                            if (instance.CimInstanceProperties[propertyName].Value is string value)
                                values.Add(value);
                        }
                        finally
                        {
                            instance.Dispose();
                        }
                    }
                }
            }
            catch
            { }

            values.Sort();

            return values.Count > 0 ? String.Join(",", values.ToArray()) : null;
        }

        /// <summary>
        /// Gets the component value using Registry
        /// </summary>
        /// <param name="registryView"></param>
        /// <param name="registryHive"></param>
        /// <param name="keyName"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        private static string GetValueWindowsRegistry(RegistryView registryView, RegistryHive registryHive, string keyName, string valueName)
        {
            string valueText = null;

            try
            {
                using (RegistryKey registry = RegistryKey.OpenBaseKey(registryHive, registryView))
                {
                    using (RegistryKey subKey = registry.OpenSubKey(keyName))
                    {
                        if (subKey != null)
                        {
                            object value = subKey.GetValue(valueName);
                            if (value != null)
                                valueText = value.ToString().TrimStart('{').TrimEnd('}');
                        }
                    }
                }

            }
            catch { }

            return valueText;
        }

        /// <summary>
        /// Get device unique id
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceId()
        {
            string deviceId = GetMachineId() + GetMotherboardSerialNumber() + GetCPUInfo();
            byte[] deviceIdHash = null;
            using (SHA256 sha256 = SHA256.Create())
            {
                deviceIdHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(deviceId));
            }
            return Convert.ToBase64String(deviceIdHash);
        }
    }
}
