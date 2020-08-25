using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Blog.Core.Common.DB;
using Blog.Core.Common.Helper;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Core.Extensions.Helper.Authorization
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发Jwt字符串
        /// </summary>
        /// <param name="tokenModel">Jwt令牌</param>
        /// <returns>Jwt字符串</returns>
        public static string IssueJwt(JwtTokenModel tokenModel)
        {
            string issue = Appsettings.App(new string[] { "Audience", "Issuer"});
            string audience = Appsettings.App(new string[] { "Audience", "Audience"});
            string secret = AppSecretConfig.Audience_Secret_String;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, issue),
                new Claim(JwtRegisteredClaimNames.Aud, audience)
            };
            // 支持单用户多角色
            claims.AddRange(tokenModel.Role.Split(",").Select(r => new Claim(ClaimTypes.Role, r)));

            // 密钥（SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报异常）
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer : issue,
                claims : claims,
                signingCredentials : creds
            );

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodeJwt = jwtHandler.WriteToken(jwt);

            return encodeJwt;
        }

        public static JwtTokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                token.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            var tokenModel = new JwtTokenModel()
            {
                Uid = (token.Id).ObjToInt(),
                Role = role != null ? role.ObjToString() : string.Empty
            };
            return tokenModel;
        }
    }

    /// <summary>
    /// Jwt令牌
    /// </summary>
    public class JwtTokenModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
