using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace helpClass.Utils
{
    ///发送短信   
    public class SMS
    {
        /// <summary>
        /// 一天内允许发送的次数
        /// </summary>
        public static int DaySendTimes = 10;
        /// <summary>
        /// 频率 60秒内只允许发送一次短信
        /// </summary>
        public static int FrequencySms = 60;
        /// <summary>
        /// 当前使用那家短信平台 1华信
        /// </summary>
        public static int UseSms = 1;
        /// <summary>
        /// 华信帐号
        /// </summary>
        public static string HXAccount = "8D00031";
        /// <summary>
        /// 华信帐号接口密码
        /// </summary>
        public static string HXPassword = "8D0003114";
        /// <summary>
        /// 华信短信平台
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="message">发送内容</param>
        /// <returns></returns>
        public static SubmitSMSResult Send(string mobile, string message)
        {
            SubmitSMSResult result = new SubmitSMSResult();
            switch (UseSms)
            {
                //华信短信平台
                case 1:
                    result = HXSMS(mobile, message);
                    break;
                default:
                    result = new SubmitSMSResult { Code = 0, Msg = "未找到提供的短信平台" };
                    break;
            }
            return result;
        }

        /// <summary>
        /// 华信短信发送方法
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="message">发送内容</param>
        /// <returns></returns>
        private static SubmitSMSResult HXSMS(string mobile, string message)
        {
            SubmitSMSResult SubmitSMSResult = new SubmitSMSResult();
            try
            {
                string Sign = "智策管家";
                message += string.Format("【{0}】", Sign);

                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                string param = "action=send&userid=" + HttpUtility.UrlDecode("", myEncoding) + "&account=" + HttpUtility.UrlEncode(HXAccount, myEncoding) + "&password=" + HttpUtility.UrlEncode(HXPassword, myEncoding) + "&mobile=" + HttpUtility.UrlDecode(mobile, myEncoding) + "&content=" + HttpUtility.UrlEncode(message, myEncoding) + "&sendTime=";
                byte[] postBytes = Encoding.ASCII.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://dx.ipyy.net/sms.aspx");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                req.ContentLength = postBytes.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                }
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);
                    System.IO.StreamReader xmlStreamReader = sr;
                    xmlDoc.Load(xmlStreamReader);
                }
                if (xmlDoc == null)
                {
                    SubmitSMSResult.Code = 0;
                    SubmitSMSResult.Msg = "短信提交失败";
                }
                else
                {
                    string ret = xmlDoc.GetElementsByTagName("message").Item(0).InnerText.ToString();
                    string status = xmlDoc.GetElementsByTagName("returnstatus").Item(0).InnerText.ToString();
                    if ("Success".ToUpper().Equals(status.Trim().ToUpper()))
                    {
                        SubmitSMSResult.Code = 1;
                        SubmitSMSResult.Msg = ret;
                    }
                    else
                    {
                        SubmitSMSResult.Code = 0;
                        SubmitSMSResult.Msg = ret;
                    }
                }
            }
            catch
            {
                SubmitSMSResult.Code = 0;
                SubmitSMSResult.Msg = "短信提交失败";
            }
            return SubmitSMSResult;
        }

    }

    /// <summary>
    /// 短信接口回调类
    /// </summary>
    public class SubmitSMSResult
    {
        /// <summary>
        /// 返回值为1时，表示提交成功,其他失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提交结果描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 仅当提交成功后，此字段值才有意义（消息ID）
        /// </summary>
        public int SmsId { get; set; }
    }
}
