#region License
// Copyright (c) 2022 Davide Gironi
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;

namespace DG.LicenseLib.Helpers
{
    public static class Base64Url
    {
        /// <summary>
        /// Base64 Url encoder
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encode(byte[] value)
        {
            if (value == null)
                return null;

            return Convert.ToBase64String(value)
                .Replace("=", "")
                .Replace("/", "_")
                .Replace("+", "-");
        }

        /// <summary>
        /// Base64 Url decoder
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Decode(string value)
        {
            if (value == null)
                return null;

            return Convert.FromBase64String(value
                    .PadRight(value.Length + (4 - value.Length % 4) % 4, '=')
                    .Replace("_", "/")
                    .Replace("-", "+"));
        }
    }
}