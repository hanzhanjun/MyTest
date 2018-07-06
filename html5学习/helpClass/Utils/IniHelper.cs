using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace helpClass.Utils
{
    /// <summary>
    /// 读取ini文件
    /// </summary>
    public static class IniHelper
    {
        public static Dictionary<string, string> filedic = new Dictionary<string, string>();

        [DllImport("kernel32")] // 写入配置文件的接口
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")] // 读取配置文件的接口
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string filePath);

        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault,
            byte[] lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        ///写入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void ProfileWriteValue(string key, string value, string path)
        {
            WritePrivateProfileString("system", key, value, path);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ProfileReadValue(string key)
        {
            StringBuilder sb = new StringBuilder(1000);
            GetPrivateProfileString("system", key, string.Empty, sb, 1000,
                AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            return sb.ToString().Trim();
        }

        /// <summary>
        /// 读取指定区域Keys列表。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ReadAllSectionKey()
        {
            if (filedic.Count > 0)
            {
                return filedic;
            }
            byte[] buf = new byte[65536];
            uint lenf = GetPrivateProfileString("system", null, null, buf, (uint)buf.Length,
                AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            int j = 0;
            for (int i = 0; i < lenf; i++)
                if (buf[i] == 0)
                {
                    if (!filedic.ContainsKey(Encoding.Default.GetString(buf, j, i - j)))
                    {
                        try
                        {
                            filedic.Add(Encoding.Default.GetString(buf, j, i - j), ProfileReadValue(Encoding.Default.GetString(buf, j, i - j)));
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    j = i + 1;
                }
            return filedic;
        }
    }

}
