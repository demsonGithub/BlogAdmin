using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    /// <summary>
    /// Jwt 认证
    /// </summary>
    public static class Authentication_JwtSetup
    {
        public static void AddAuthentication_JwtSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var secretKeyToByte = Encoding.UTF8.GetBytes(ConfigSetting.JwtTokenInfo.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretKeyToByte);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 生成令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = ConfigSetting.JwtTokenInfo.Issuer,
                ValidateAudience = true,
                ValidAudience = ConfigSetting.JwtTokenInfo.Audience,
                // 是否需要token必须设置过期时间Expires
                RequireExpirationTime = true,
                // 允许服务器时间偏移量
                ClockSkew = TimeSpan.FromMinutes(3),
                // 是否需要验证Token有效期，当前时间在Claim中的notBefore，expires之间
                ValidateLifetime = true
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //option.DefaultChallengeScheme = nameof(ApiResponseHandler);
                //option.DefaultForbidScheme = nameof(ApiResponseHandler);
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;

                o.Events = new JwtBearerEvents
                {
                    // 首次收到协议消息时调用
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    // 在安全令牌已通过验证
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },
                    // 验证失败 后执行
                    OnChallenge = context =>
                    {
                        context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    // 认证成功，但权限不够
                    OnForbidden = context =>
                    {
                        context.Response.Headers.Add("Token-Msg", "Unauthorized");
                        return Task.CompletedTask;
                    },
                    // 在请求处理过程中身份验证失败
                    OnAuthenticationFailed = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
                        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                        if (jwtToken.Issuer != ConfigSetting.JwtTokenInfo.Issuer)
                        {
                            context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                        }
                        if (jwtToken.Audiences.FirstOrDefault() != ConfigSetting.JwtTokenInfo.Audience)
                        {
                            context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                        }
                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}