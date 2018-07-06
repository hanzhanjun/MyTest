using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    /// <summary>
    /// 表示基于Redis缓存机制实现的Token管理。
    /// </summary>
    public class RedisToken
    {
        private string token;
        private int timeout;
        private RedisCache redis;

        public RedisToken(string token)
            : this(token, 20)
        {
        }

        public RedisToken(string token, int timeout)
        {
            this.token = token;
            this.timeout = timeout;
            this.redis = new RedisCache();
        }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token
        {
            get { return token; }
        }

        /// <summary>
        /// 判断会话中是否存在指定键
        /// </summary>
        /// <param name="name">会话状态的键。</param>
        /// <returns>返回一个布尔值，成功为true,否则为false</returns>
        public bool Exists(string name)
        {
            string key = GetPrefixKey(name);
            return redis.Exists(key);
        }

        /// <summary>
        /// 从会话状态集合中获取一个新项
        /// </summary>
        /// <param name="name">会话状态的键。</param>
        /// <returns>返回一个会话状态的项</returns>
        public T Get<T>(string name) where T : class
        {
            string key = GetPrefixKey(name);
            return redis.Get<T>(key);
        }

        /// <summary>
        ///  向会话状态集合添加一个新项。
        /// </summary>
        /// <param name="name">会话状态的名称。</param>
        /// <param name="value">会话状态的项。</param>
        public void Add<T>(string name, T value) where T : class
        {
            string key = GetPrefixKey(name);
            redis.Add<T>(key, value, timeout * 60);
        }

        /// <summary>
        ///  删除会话状态集合中的项。
        /// </summary>
        /// <param name="name">会话状态的键。</param>
        public void Remove(string name)
        {
            string key = GetPrefixKey(name);
            redis.Remove(key);
        }

        /// <summary>
        /// 设置会话状态集合的过期时间
        /// </summary>
        /// <param name="name">会话状态的名称。</param>
        /// <param name="timeout">会话状态的过期时间（以分钟为单位）。</param>
        public void SetExpire(string name, int timeout)
        {
            string key = GetPrefixKey(name);
            redis.SetExpire(key, timeout * 60);

        }

        /// <summary>
        /// 获取一个带有前缀的键名
        /// </summary>
        /// <param name="name">会话状态的名称。</param>
        /// <returns>返回一个新的的键名</returns>
        private string GetPrefixKey(string name)
        {
            return string.Format("TOKEN:{0}:{1}", name, token.ToUpper());
        }

    }
}
