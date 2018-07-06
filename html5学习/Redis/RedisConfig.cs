using System.Configuration;

namespace Redis
{
    public class RedisConfig
    {
        private static readonly string config = ConfigurationManager.AppSettings["redis"];

        /// <summary>
        /// 获取redis连接配置
        /// </summary>
        /// <returns>连接配置</returns>
        public static string Config()
        {
            return config;
        }
    }
}
