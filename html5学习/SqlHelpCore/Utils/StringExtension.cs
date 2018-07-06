using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SqlHelpCore.Utils
{
    /// <summary>
    /// 字符串处理扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转换为整形，失败返回-1
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            int val = 0;
            if (int.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 将字符串转换为整形，失败返回指定整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="failInt">失败时返回的整数</param>
        /// <returns></returns>
        public static int ToInt(this string str, int failInt)
        {
            int val = 0;
            if (int.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return failInt;
            }
        }

        /// <summary>
        /// 将字符串转换为整形，失败返回null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static int? ToIntNull(this string str)
        {
            int val = 0;
            if (int.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转换为时间，失败返回null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string str)
        {
            if (str == null || str.Trim().Length <= 0)
                return null;
            try
            {
                return DateTime.Parse(str);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  字符串转换为时间，失败返回指定时间
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="failDate">失败时返回的时间</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime failDate)
        {
            DateTime val = new DateTime();
            if (DateTime.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return failDate;
            }
        }

        /// <summary>
        /// 字符串转换为双精度类型，失败时返回null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Double? ToDouble(this string str)
        {
            Double val = 0;
            if (Double.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转换为十进制数，失败时返回null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this string str)
        {
            Decimal val = 0;
            if (Decimal.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        public static decimal ToDecimal(this string str, int length)
        {
            string[] s = str.Split('.');
            if (s.Length > 1)
            {
                str = s[0] + "." + s[1].SubstringMax(length);
            }
            return Convert.ToDecimal(str); ;
        }

        public static string SubstringMax(this string val, int length)
        {
            val = val.Trim();
            if (val.Length <= length)
            {
                return val;
            }

            return val.Substring(0, length);
        }

        /// <summary>
        /// 字符串转换为64为整数，失败返回null
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Int64? ToInt64(this string str)
        {
            Int64 val = 0;
            if (Int64.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转换为GUID类型失败返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid? ToGuid(this string str)
        {
            Guid val = new Guid();
            if (Guid.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转换为布尔类型，失败返回false
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static Boolean ToBoolean(this string str)
        {
            Boolean val = false;
            if (Boolean.TryParse(str, out val))
            {
                return val;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 字符串替换当中html脚本等等内容
        /// </summary>
        /// <param name="strHtml">字符串</param>
        /// <returns></returns>
        public static String StripHTML(this string strHtml)
        {
            string[] aryReg =
            {
                @"<script[^>]*?>.*?</script>",
                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(file://[""'tbnr]|[^/7])*?/7|/w+)|.{0})|/s)*?(///s*)?>",
                @"([\r\n])[\s]+",
                @"&(quot|#34);",
                @"&(amp|#38);",
                @"&(lt|#60);",
                @"&(gt|#62);",
                @"&(nbsp|#160);",
                @"&(iexcl|#161);",
                @"&(cent|#162);",
                @"&(pound|#163);",
                @"&(copy|#169);",
                @"&#(\d+);",
                @"-->",
                @"<!--.*\n"
            };
            string[] aryRep =
            {
                "",
                "",
                "",
                "\"",
                "&",
                "<",
                ">",
                " ",
                "\xa1", //chr(161),
                "\xa2", //chr(162),
                "\xa3", //chr(163),
                "\xa9", //chr(169),
                "",
                "\r\n",
                ""
            };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }

        /// <summary>        
        /// 整型格式化为货币型        
        /// </summary>        
        /// <param name="convert">需格式化字符串</param>        
        /// <returns></returns>       
        public static string ToMoney(this object convert)
        {
            try
            {
                var val = convert.ToString();
                return String.Format("{0:C2}", Convert.ToDecimal(val.Replace("￥", "")));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>        
        /// 整型格式化为货币型        
        /// </summary>        
        /// <param name="convert">需格式化字符串</param>  
        /// <param name="point">保留位数</param>  
        /// <returns></returns>       
        public static string ToMoney(this object convert, int point)
        {
            try
            {
                var val = convert.ToString();
                return String.Format(String.Format("{0:C{0}}", point), Convert.ToDecimal(val.Replace("￥", "")));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转换为MD5再转成GUID格式
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMd5Guid(this string val)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(val);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                sb.Append(md5data[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字符串转换成MD5
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMd5(this string val)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(val)));
        }

        /// <summary>
        /// 判断一个字符串是否为空且是否为默认查询ID
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrSelectID(this string val)
        {
            return !(val != null && !String.IsNullOrEmpty(val.Trim())
                     && !GlobalParams.DEFAULT_DETAIL_SELECT_ID.Equals(val.Trim())
                     && !GlobalParams.DEFAULT_QUERY_SELECT_ID.Equals(val.Trim()));
        }

        /// <summary>
        /// 判断一个整形是否为null且是否为默认查询ID
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsIntNullOrSelectID(this int val)
        {
            return !(GlobalParams.DEFAULT_DETAIL_SELECT_ID.ToInt() != val
                     && GlobalParams.DEFAULT_QUERY_SELECT_ID.ToInt() != val);
        }

        /// <summary>
        /// 判断一个整形是是否为默认查询ID
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsIntOrSelectID(this int val)
        {
            return !(GlobalParams.DEFAULT_DETAIL_SELECT_ID.ToInt() != val
                     && GlobalParams.DEFAULT_QUERY_SELECT_ID.ToInt() != val);
        }

        /// <summary>
        /// 判断一个字符串是否为空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string val)
        {
            return String.IsNullOrEmpty(val);
        }

        /// <summary>
        /// 如果字符串为null返回空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToStringIsNullOrEmpty(this string val)
        {
            if (String.IsNullOrEmpty(val))
            {
                return string.Empty;
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// 判断一个int?是否为null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsIntNull(this int? val)
        {
            return val == null;
        }

        /// <summary>
        /// object类型转换为字符串如果为null返回空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToIsNullString(this object val)
        {
            if (val == null)
            {
                return string.Empty;
            }
            else
            {
                return val.ToString();
            }
        }

        public static string ToIsNullString(this int? val)
        {
            if (val == null)
            {
                return "0";
            }
            else
            {
                return val.ToString();
            }
        }

        /// <summary>
        /// 判断字符串是否为有效的手机号
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsMobilePhone(this string input)
        {
            Regex regex = new Regex(@"^1[3|4|5|6|7|8|9][0-9]\d{8}$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否为有效的中文姓名
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsChineseName(this string input)
        {
            Regex regex = new Regex(@"^[\u4e00-\u9fa5]+(·[\u4e00-\u9fa5]+)*$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否为有效的中国公民身份证号码（15位或18位）
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsIDCard(this string input)
        {
            Regex regex =
                new Regex(
                    @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否是数字和字母组成
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsNumberAndChar(this string input)
        {
            string pattern = @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否是6位纯数字
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns><see cref="bool"/></returns>
        public static bool IsSixNumber(this string input)
        {
            string pattern = @"^\d{6}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        public static string ToIsNullString(this decimal? val)
        {
            if (val == null)
            {
                return "0.00";
            }
            else
            {
                return Convert.ToDecimal(val).ToString("#0.00");
            }
        }

        /// <summary>
        /// 判断一个DateTime?是否为null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDateTimeNull(this DateTime? val)
        {
            return val == null;
        }

        /// <summary>
        /// 对特殊的html字符进行转义(\n,\t,\r,\\)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToEscapeHtml(this string val)
        {
            if (val == null) return "";
            StringBuilder retStr = new StringBuilder(val);
            val.Replace("\n", "\\n");
            val.Replace("\t", "\\t");
            val.Replace("\r", "\\r");
            val.Replace("\\", "\\\\");
            return val.ToString();
        }

        /// <summary>
        /// 将11位字符串根据转入参数判断是否隐藏中间4位以*代替
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isShow">不等于1时隐藏中间四位</param>
        /// <returns></returns>
        public static string ToMobile(this string val, bool isShow = false)
        {
            if (val == null || val.Length != 11)
            {
                return val;
            }
            if (!isShow)
            {
                val = val.Substring(0, 3) + "****" + val.Substring(7, 4);
            }
            return val;
        }

        /// <summary>
        /// 将15或18位字符串根据转入参数判断是否隐藏中间4位以*代替
        /// </summary>
        /// <param name="val"></param>
        /// <param name="isShow">不等于1时隐藏中间四位</param>
        /// <returns></returns>
        public static string ToIDCard(this string val, bool isShow)
        {
            if (val == null || (val.Length != 15 && val.Length != 18))
            {
                return val;
            }
            if (!isShow && val.Length == 15)
            {
                val = val.Substring(0, 6) + "****" + val.Substring(11, 3);
            }
            if (!isShow && val.Length == 18)
            {
                val = val.Substring(0, 6) + "****" + val.Substring(14, 4);
            }
            return val;
        }


        /// <summary>
        /// 提取银行卡后4位
        /// </summary>
        /// <param name="userBankCard"></param>
        /// <returns></returns>
        public static string ToBankCardLast(this string userBankCard)
        {
            try
            {
                if (userBankCard.Length > 4)
                {
                    return userBankCard.Remove(0, userBankCard.Length - 4);
                }
                return userBankCard;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetIp(HttpContext httpContext)
        {
            try
            {
                var ip = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = httpContext.Request.ServerVariables["REMOTE_ADDR"];
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = httpContext.Request.UserHostAddress;
                    }
                    if (ip == "::1")
                    {
                        ip = "127.0.0.1";
                    }
                }
                return ip;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取ip地址所属地
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetIpAddress(string ip)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string result = reader.ReadToEnd();
                reader.Close();
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                string rtmsg = (jo["data"]["country"].ToString() + jo["data"]["region"].ToString() + jo["data"]["city"].ToString()
                              + jo["data"]["isp"].ToString()).Replace("X", "");
                if (rtmsg.Contains("内网IP"))
                {
                    return "内网IP";
                }
                return rtmsg;
            }
            catch
            {
                return "";
            }
        }

        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <returns>已经去除后的文字</returns>
        public static string NoHTML(this string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
            RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"–>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!–.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
            RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
}
