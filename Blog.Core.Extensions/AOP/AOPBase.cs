using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Blog.Core.Extensions.AOP
{
    public abstract class AOPBase : IInterceptor
    {

        public abstract void Intercept(IInvocation invocation);

        /// <summary>
        /// 自定义缓存的主键
        /// </summary>
        /// <param name="invocation">调用方法</param>
        /// <returns>缓存主键</returns>
        protected string CustomCacheKey(IInvocation invocation)
        {
            string typeName = invocation.TargetType.Name;
            string methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();

            string key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key = $"{key}{param}:";
            }
            return key.TrimEnd(':');
        }

        /// <summary>
        /// 将参数转换为字符串
        /// </summary>
        /// <param name="arg">参数</param>
        /// <returns>字符串</returns>
        protected static string GetArgumentValue(object arg)
        {
            if(arg is DateTime || arg is DateTime?)
            {
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");
            }
            if(arg is string || arg is ValueType || arg is Nullable)
            {
                return arg.ToString();
            }
            if(arg != null)
            {
                if(arg.GetType().IsClass)
                {
                    return MD5Encrypt16(JsonConvert.SerializeObject(arg));
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="content">需要加密的内容</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt16(string content)
        {
            var md5 = new MD5CryptoServiceProvider();
            string result = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(content)), 4, 8);
            result = result.Replace("-", "");
            return result;
        }
    }
}