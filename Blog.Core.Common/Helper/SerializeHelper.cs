using System.Text;
using Newtonsoft.Json;

namespace Blog.Core.Common.Helper
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="item">被序列化的对象</param>
        /// <returns>序列化的字节数组</returns>
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        /// <summary>
        /// 将字节数组反序列化为指定类型对象
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <typeparam name="TEntity">指定类型</typeparam>
        /// <returns>指定类型实例</returns>
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if(value == null)
            {
                return default(TEntity);
            }
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }
    }
}