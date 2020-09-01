namespace Blog.Core.Extensions.Helper
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue);
    }
}