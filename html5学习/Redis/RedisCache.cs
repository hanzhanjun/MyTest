using helpClass;
using System;
using StackExchange.Redis;


namespace Redis
{
    /// <summary>
    /// 表示基于Redis的缓存机制的实现。
    /// </summary>
    public class RedisCache : ICache
    {
        private readonly ConnectionMultiplexer redis;

        public RedisCache()
        {
            redis = RedisManager.Instance;
        }

        /// <summary>
        /// 向缓存中添加一个对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <param name="value">需要缓存的对象</param>
        public void Add<T>(string key, T value) where T : class
        {
            IDatabase db = redis.GetDatabase();
            db.StringSet(key, JsonHelper.SerializeObject(value));
        }

        /// <summary>
        /// 向缓存中添加一个对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="expire">缓存的过期时间（单位：秒）</param>
        public void Add<T>(string key, T value, int expire) where T : class
        {
            IDatabase db = redis.GetDatabase();
            TimeSpan ts = new TimeSpan(TimeSpan.TicksPerSecond * expire);
            db.StringSet(key, JsonHelper.SerializeObject(value),ts);
        }

        /// <summary>
        /// 获取一个<see cref="bool"/>值，该值表示拥有指定键值的缓存是否存在
        /// </summary>
        /// <param name="key">指定的键值</param>
        /// <returns>如果缓存存在，则返回true，否则返回false</returns>
        public bool Exists(string key)
        {
            IDatabase db = redis.GetDatabase();
            return db.KeyExists(key);
        }

        /// <summary>
        /// 从缓存中读取对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <returns>缓存的对象</returns>
        public T Get<T>(string key) where T : class
        {
            IDatabase db = redis.GetDatabase();
            return JsonHelper.DeserializeObject<T>(db.StringGet(key));
        }

        /// <summary>
        /// 向缓存中更新一个对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <param name="value">需要缓存的对象</param>
        public void Put<T>(string key, T value) where T : class
        {
            Add<T>(key, value);
        }

        /// <summary>
        /// 向缓存中更新一个对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <param name="value">需要缓存的对象</param>
        /// <param name="expire">缓存的过期时间（单位：秒）</param>
        public void Put<T>(string key, T value, int expire) where T : class
        {
            Add<T>(key, value, expire);
        }

        /// <summary>
        /// 从缓存中移除对象
        /// </summary>
        /// <param name="key">缓存的键值</param>
        public void Remove(string key)
        {
            IDatabase db = redis.GetDatabase();
            db.KeyDelete(key);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">缓存的键值</param>
        /// <param name="timeout">过期时间（单位：秒）</param>
        public void SetExpire(string key,int timeout)
        {
            IDatabase db = redis.GetDatabase();
            db.KeyExpire(key, DateTime.Now.AddSeconds(timeout));
        }
    }
}
