#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
#if NETSTANDARD
using System.Runtime.InteropServices;
using RuntimeEnvironment = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment;
#endif

namespace DG.LicenseLib.DeviceId.Helpers
{
    public static class GetOSInfo
    {
        /// <summary>
        /// Check if we are running Windows
        /// </summary>
        public static bool IsWindows { get; }
#if NETFRAMEWORK
        = true;
#elif NETSTANDARD
        = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#elif NET5_0_OR_GREATER
        = OperatingSystem.IsWindows();
#endif

        /// <summary>
        /// Check if we are running Linux
        /// </summary>
        public static bool IsLinux { get; }
#if NETFRAMEWORK
        = false;
#elif NETSTANDARD
        = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
#elif NET5_0_OR_GREATER
        = OperatingSystem.IsLinux();
#endif

        /// <summary>
        /// Check if we are running MacOS
        /// </summary>
        public static bool IsMacOS { get; }
#if NETFRAMEWORK
        = false;
#elif NETSTANDARD
        = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#elif NET5_0_OR_GREATER
        = OperatingSystem.IsMacOS();
#endif

        /// <summary>
        /// Get current OS Version
        /// </summary>
        public static string Version { get; }
#if (NETFRAMEWORK || NET5_0_OR_GREATER)
        = Environment.OSVersion.ToString();
#elif NETSTANDARD
        = IsWindows
            ? Environment.OSVersion.ToString()
            : string.Concat(RuntimeEnvironment.OperatingSystem, " ", RuntimeEnvironment.OperatingSystemVersion);
#endif
    }
}
