using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
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
        private const string CustomPermission = "CustomPermission";

        /// <summary>
        /// 根据用户标识生成token,基于Roles或者Name
        /// </summary>
        /// <param name="claims">登录的时候配置</param>
        /// <returns></returns>
        public static string BuildJwtToken(Claim[] claims)
        {
            // 生成签名
            var secretKeyToByte = Encoding.UTF8.GetBytes(ConfigSetting.JwtTokenInfo.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretKeyToByte);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: ConfigSetting.JwtTokenInfo.Issuer,
                audience: ConfigSetting.JwtTokenInfo.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(ConfigSetting.JwtTokenInfo.ExpiresTime),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        /// <summary>
        /// 基于策略生成Token
        /// </summary>
        /// <param name="claims">登录的时候配置</param>
        /// <param name="permissionRequirement">初始化的时候需要配置</param>
        /// <returns></returns>
        public static string BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.Now;
            // 实例化JwtSecurityToken
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.ExpiresTime),
                signingCredentials: permissionRequirement.SigningCredentials
            );
            // 生成 token
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        /// <summary>
        /// 解析JwtToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TokenDetailModel SerializeJwtToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            TokenDetailModel result = new TokenDetailModel();
            // token校验
            if (!string.IsNullOrEmpty(token) && jwtHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out object role);

                if (role.GetType().Name == "JArray")
                {
                    var roles = JsonConvert.DeserializeObject<string[]>(role.ObjToString());
                    result = new TokenDetailModel
                    {
                        JwtId = (jwtToken.Id),
                        Role = roles
                    };
                }
                else
                {
                    var roles = new string[] { role != null ? role.ObjToString() : "" };
                    result = new TokenDetailModel
                    {
                        JwtId = (jwtToken.Id),
                        Role = roles
                    };
                }
            }

            return result;
        }
    }

    public class TokenDetailModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string JwtId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string[] Role { get; set; }
    }
}