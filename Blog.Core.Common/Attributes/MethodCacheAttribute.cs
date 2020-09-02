using System;

namespace Blog.Core.Common.Attributes
{
    /// <summary>
    /// 启用方法缓存特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class MethodCacheAttribute : Attribute
    {
        public MethodCacheAttribute(int expiration)
        {
            AbsoluteExpiration = expiration;
        }

        /// <summary>
        /// 缓存绝对过期时间（秒）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;
    }
}