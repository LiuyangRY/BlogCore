using System;

namespace Blog.Core.Common.Redis
{
    /// <summary>
    /// Redis缓存管理接口
    /// </summary>
    public interface IRedisCacheManager
    {
        /// <summary>
        /// 获取Redis缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        string GetValue(string key);

        /// <summary>
        /// 获取值，并反序列化为指定对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <typeparam name="TEntity">指定对象类型</typeparam>
        /// <returns>缓存值反序列化的指定对象实例</returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        /// 保存缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheTime">缓存有效时间</param>
        void Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>存在返回true，否则返回false</returns>
        bool Exist(string key);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 增加或修改缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns>操作成功返回true,否则返回false</returns>
        bool SetValue(string key, byte[] value);
    }
}