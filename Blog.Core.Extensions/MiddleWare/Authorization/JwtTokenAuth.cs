using System.Threading.Tasks;
using Blog.Core.Extensions.Helper.Authorization;
using Microsoft.AspNetCore.Http;

namespace Blog.Core.Extensions.MiddleWare.Authorization
{
    /// <summary>
    /// 自定义授权中间件
    /// </summary>
    public class JwtTokenAuth
    {
        private readonly RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        private void PreProceed(HttpContext httpContext)
        {
            System.Console.WriteLine($"{System.DateTime.Now} middleware invoke preproceed");
        }

        private void PostProceed(HttpContext httpContext)
        {
            System.Console.WriteLine($"{System.DateTime.Now} middleware invoke postproceed");
        }

        public Task Invoke(HttpContext httpContext)
        {
            PreProceed(httpContext);

            // 检测是否包含"Authorization"请求头
            if(!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                PostProceed(httpContext);
                return _next(httpContext);
            }

            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                if(tokenHeader.Length >= 128)
                {
                    System.Console.WriteLine($"{System.DateTime.Now} token :{tokenHeader}");
                    JwtTokenModel tokenModel = JwtHelper.SerializeJwt(tokenHeader);
                    // 授权
                    var claimList = new System.Collections.Generic.List<System.Security.Claims.Claim>();
                    var claim = new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, tokenModel.Role);
                    claimList.Add(claim);
                    var identity = new System.Security.Claims.ClaimsIdentity(claimList);
                    var principal = new System.Security.Claims.ClaimsPrincipal(identity);
                    httpContext.User = principal;
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"{System.DateTime.Now} middleware wrong:{ex.Message}");
                throw ex;
            }
            PostProceed(httpContext);

            return _next(httpContext);
        }
    }
}