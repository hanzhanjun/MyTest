using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace helpClass.Utils
{
    public static class JsonExtension
    {
        static Newtonsoft.Json.Converters.IsoDateTimeConverter iso = new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
        public static string ToJson(this Object obj)
        {
            if (obj == null)
                return "{}";
            return JsonConvert.SerializeObject(obj, Formatting.None, iso).Replace("\r\n", "");
        }

        /// <summary>
        /// 将json数组转换成Entity List
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="data">json字符串</param>
        /// <returns></returns>
        public static List<T> ToJsonListEntity<T>(string data)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<T>));
            var ms = new MemoryStream(
                Encoding.UTF8.GetBytes(data));
            ms.Position = 0;
            var result = (List<T>)serializer.ReadObject(ms);
            ms.Close();
            return result;
        }

        /// <summary>
        /// 将json转换成Entity
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="data">json字符串</param>
        /// <returns></returns>
        public static T ToJsonEntity<T>(string data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var ms = new MemoryStream(
                Encoding.UTF8.GetBytes(data));
            ms.Position = 0;
            var result = (T)serializer.ReadObject(ms);
            ms.Close();
            return result;
        }
    }
}
