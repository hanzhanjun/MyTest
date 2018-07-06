using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace helpClass
{
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string SerializeObject(object o)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            return JsonConvert.SerializeObject(o, setting);
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T DeserializeObject<T>(string json) where T : class
        {
            try
            {
               return JsonConvert.DeserializeObject<T>(json);              
            }
            catch(Exception ex)
            {                          
                return default(T);
            }
           
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeArrayList<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch
            {
                return default(List<T>);
            }
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            try
            {
                return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            }
            catch
            {
                return default(T);
            }
            
        }
    }
}
