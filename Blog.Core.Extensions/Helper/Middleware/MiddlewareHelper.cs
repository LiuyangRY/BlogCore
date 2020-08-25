using Blog.Core.Extensions.MiddleWare.Authorization;
using Microsoft.AspNetCore.Builder;

namespace Blog.Core.Extensions.Helper.Middleware
{
    /// <summary>
    /// 中间件帮助类
    /// </summary>
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}