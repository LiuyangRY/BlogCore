using System;
using Blog.Core.Common.Helper;
using StackExchange.Redis;

namespace Blog.Core.Common.Redis
{
    /// <summary>
    /// Redis缓存管理类
    /// </summary>
    public class RedisCacheManager : IRedisCacheManager
    {
        private readonly string redisConnectionString;

        public volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLock = new object();

        public RedisCacheManager()
        {
            string redisConnectionConfiguration = Appsettings.App( new string[] { "AppSettings", "RedisCacheAOP", "ConnectionString" });
            if(string.IsNullOrWhiteSpace(redisConnectionConfiguration))
            {
                throw new ArgumentException("Redis config is empty.", nameof(redisConnectionConfiguration));
            }
            redisConnectionString = redisConnectionConfiguration;
            redisConnection = GetRedisConnect();
        }

        /// <summary>
        /// 获取Redis连接实例
        /// </summary>
        /// <returns>Redis连接实例</returns>
        private ConnectionMultiplexer GetRedisConnect()
        {
            if(redisConnection != null && redisConnection.IsConnected)
            {
                return redisConnection;
            }
            lock (redisConnectionLock)
            {
                if(redisConnection != null)
                {
                    redisConnection.Dispose();
                }
                try
                {
                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        AllowAdmin = true,
                        ConnectTimeout = 15000,     // 15s
                        SyncTimeout = 5000,
                        EndPoints = { redisConnectionString }
                    };
                    redisConnection = ConnectionMultiplexer.Connect(config);
                }
                catch (Exception ex)
                {
                    throw new Exception("Redis服务未启用，请开启该服务，并且请注意端口号。", ex);
                }
            }
            return redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in GetRedisConnect().GetEndPoints())
            {
                var server = GetRedisConnect().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public bool Exist(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if(value.HasValue)
            {
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }

        public string GetValue(string key)
        {
            return redisConnection.GetDatabase().StringGet(key);
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if(value != null)
            {
                redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
        }

        public bool SetValue(string key, byte[] value)
        {
            return redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }
    }
}