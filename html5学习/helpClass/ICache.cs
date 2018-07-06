using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helpClass
{
    /// <summary>
    /// 表示实现该接口的类型是能够为应用程序提供缓存机制的类型。
    /// </summary>
    public interface ICache
    {
        #region Methods

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        /// <param name="value">需要缓存的对象。</param>
        void Add<T>(string key, T value) where T : class;

        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="expire">缓存的过期时间（单位：秒）</param>
        void Add<T>(string key, T value, int expire) where T : class;

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        /// <param name="value">需要缓存的对象。</param>
        void Put<T>(string key, T value) where T : class;

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        /// <param name="value">需要缓存的对象。</param>
        /// <param name="expire">缓存的过期时间。（单位：秒）</param>
        void Put<T>(string key, T value, int expire) where T : class;

        /// <summary>
        /// 从缓存中读取对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        /// <returns>被缓存的对象。</returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// 从缓存中移除对象。
        /// </summary>
        /// <param name="key">缓存的键值。</param>
        void Remove(string key);

        /// <summary>
        /// 获取一个<see cref="bool"/>值，该值表示拥有指定键值的缓存是否存在。
        /// </summary>
        /// <param name="key">指定的键值。</param>
        /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
        bool Exists(string key);

        #endregion
    }
}
