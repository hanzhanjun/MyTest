using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace helpClass.Utils
{
    public static class DescExtension
    {
        private static byte[] KEY = { 0x13, 0xFC, 0x35, 0xB0, 0x9B, 0xDB, 0xC5, 0xE9 }; //密钥
        private static byte[] IV = { 0x16, 0xAC, 0x49, 0xC8, 0x9A, 0xC1, 0xCA, 0xE7 };//加密IV向量

        /// <summary>
        /// Desc加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDesc(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(value);
            des.Key = KEY;
            des.IV = IV;

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Desc解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromDesc(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return String.Empty;
            }
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(value);
                des.Key = KEY;
                des.IV = IV;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray()).Replace("&quot;", "\"");
                    }
                }
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
