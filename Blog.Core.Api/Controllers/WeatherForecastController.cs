using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Blog.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 天气广播控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取5天的天气信息
        /// </summary>
        /// <returns>天气信息集合</returns>
        [HttpGet]
        [Authorize(Policy = "Client")]
        public IEnumerable<WeatherForecastModel> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 获取爱信息
        /// </summary>
        /// <returns>爱信息实体</returns>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [Route("[controller]/[action]")]
        public LoveModel GetLoveInfo()
        {
            return new LoveModel(){
                Id = 25,
                Name = "Liuyang",
                Age = 24
            };
        }

        /// <summary>
        /// 获取客户端令牌
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult<string> GetToken()
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
            
            return token;
        }

        /// <summary>
        /// 获取管理员令牌
        /// </summary>
        /// <returns>客户端令牌</returns>
        [HttpGet]
        [Authorize("Client")]
        [Route("[controller]/[action]")]
        public ActionResult<string> GetAdminToken()
        {
            var claims = new []
            {
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismytesttokensecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("Blog.Core", "Liuyang", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            return token;
        }

        /// <summary>
        /// 不显示方法
        /// </summary>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void IgnoreAction(string test)
        {
            
        }
    }
}
