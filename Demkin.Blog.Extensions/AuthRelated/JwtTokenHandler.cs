using Demkin.Blog.Utils.SystemConfig;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demkin.Blog.Extensions.AuthRelated
{
    /// <summary>
    /// JwtToken 操作
    /// </summary>
    public class JwtTokenHandler
    {
        /// <summary>
        /// 根据用户标识生成token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string BuildJwtToken(List<Claim> claims)
        {
            // 生成签名
            var secretKeyToByte = Encoding.UTF8.GetBytes(JwtTokenInfo.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretKeyToByte);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(issuer: JwtTokenInfo.Issuer,
                audience: JwtTokenInfo.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(JwtTokenInfo.ExpiresTime),
                signingCredentials: signingCredentials
            ));

            return token;
        }
    }
}