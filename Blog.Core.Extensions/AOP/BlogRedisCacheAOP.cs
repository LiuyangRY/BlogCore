using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Common.Attributes;
using Blog.Core.Common.Redis;
using Castle.DynamicProxy;

namespace Blog.Core.Extensions.AOP
{
    /// <summary>
    /// Redis缓存拦截器
    /// </summary>
    public class BlogRedisCacheAOP : AOPBase
    {
        private readonly IRedisCacheManager _cache;

        public BlogRedisCacheAOP(IRedisCacheManager cache)
        {
            _cache = cache;
        }

        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if(method.ReturnType.Equals(typeof(void)) || method.ReturnType.Equals(typeof(Task)))
            {
                invocation.Proceed();
                return;
            }

            var methodCacheAttribute = method.GetCustomAttributes(true).FirstOrDefault(a => a.GetType().Equals(typeof(MethodCacheAttribute))) as MethodCacheAttribute;
            if(methodCacheAttribute != null)
            {
                var cacheKey = CustomCacheKey(invocation);

                var cacheValue = _cache.GetValue(cacheKey);
                if(cacheValue != null)
                {
                    Type returnType;
                    if(typeof(Task).IsAssignableFrom(method.ReturnType))
                    {
                        returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
                    }
                    else
                    {
                        returnType = method.ReturnType;
                    }

                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(cacheValue, returnType);
                    invocation.ReturnValue = (typeof(Task).IsAssignableFrom(returnType)) ? Task.FromResult(result) : result;
                    return;
                }
                invocation.Proceed();
                if(!string.IsNullOrWhiteSpace(cacheKey))
                {
                    object response;
                    var type = invocation.Method.ReturnType;
                    if(typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if(response == null)
                    {
                        response = string.Empty;
                    }
                    _cache.Set(cacheKey, response, TimeSpan.FromSeconds(methodCacheAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}