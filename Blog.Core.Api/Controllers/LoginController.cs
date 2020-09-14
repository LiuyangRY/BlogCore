using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Core.Api.SwaggerHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            Response.WriteAsync("This is Login Get Method.");
        }

        /// <summary>
        /// 获取客户端令牌
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpGet]
        [CustomRoute(CustomApiVersion.V1, "GetJsonpV1")]
        public void GetJsonp(string callback)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, "Liuyang"),
                new Claim(ClaimTypes.Role, "Client")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            string response = string.Format("\"value\":\"{0}\"", token);
            string call = callback + "({" + response + "})";
            Response.WriteAsync(call);
        }
        
        /// <summary>
        /// 获取客户端令牌
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpGet]
        [CustomRoute(CustomApiVersion.V1, "GetTokenV1")]
        public void GetToken(string callback)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, "Liuyang"),
                new Claim(ClaimTypes.Role, "Client")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            string response = string.Format("\"value\":\"{0}\"", token);
            string call = callback + "({" + response + "})";
            Response.WriteAsync(call);
        }

        /// <summary>
        /// 获取客户端令牌
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpPost]
        [CustomRoute(CustomApiVersion.V1, "PostTokenV1")]
        public void PostToken(object data)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, "Liuyang"),
                new Claim(ClaimTypes.Role, "Client")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            string response = string.Format("\"value\":\"{0}\"", token);
            string call = "({" + response + "})";
            Response.WriteAsync(call);
        }

        /// <summary>
        /// 获取客户端令牌（V2）
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpGet]
        [CustomRoute(CustomApiVersion.V2, "GetTokenV2")]
        public void GetTokenV2(string callback)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, "Liuyang"),
                new Claim(ClaimTypes.Role, "Client")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            string response = string.Format("\"value\":\"{0}\"", token);
            string call = callback + "({" + response + "})";
            Response.WriteAsync(call);
        }

        /// <summary>
        /// 获取客户端令牌（V2）
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpPost]
        [CustomRoute(CustomApiVersion.V2, "PostTokenV2")]
        public void PostTokenV2(object data)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Name, "Liuyang"),
                new Claim(ClaimTypes.Role, "Client")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            string response = string.Format("\"value\":\"{0}\"", token);
            string call = "({" + response + "})";
            Response.WriteAsync(call);
        }
    }
}