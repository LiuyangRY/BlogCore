using Blog.Core.Extensions.Helper;
using Castle.DynamicProxy;

namespace Blog.Core.Extensions.AOP
{
    public class BlogCacheAOP : AOPBase
    {
        private ICache _cache;

        public BlogCacheAOP(ICache cache)
        {
            _cache = cache;
        }

        public override void Intercept(IInvocation invocation)
        {
            // 获取自定义缓存主键
            string cacheKey = CustomCacheKey(invocation);
            // 根据Key获取对应的缓存值
            var cacheValue = _cache.Get(cacheKey);
            if(cacheValue != null)
            {
                // 将获取到的缓存值，赋值给当前执行方法
                invocation.ReturnValue = cacheValue;
                return;
            }
            // 没有缓存，执行方法
            invocation.Proceed();
            // 缓存执行结果
            if(!string.IsNullOrWhiteSpace(cacheKey))
            {
                _cache.Set(cacheKey, invocation.ReturnValue);
            }
        }
    }
}