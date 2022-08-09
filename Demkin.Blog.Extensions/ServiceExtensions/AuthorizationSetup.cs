using Demkin.Blog.Extensions.AuthRelated;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Demkin.Blog.Extensions.ServiceExtensions
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 四种常见的的授权方式

            // 1. 在Controller上增加特性 [Authorize(Roles="Admin,System")]

            #region 2. 也是增加特性，不好处就是不用写多个Roles [Authorize(Policy="AdminOrCommon")]

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Common", policy => policy.RequireRole("Common").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("AdminOrCommon", policy => policy.RequireRole("Admin", "Common").Build());
            //});

            #endregion 2. 也是增加特性，不好处就是不用写多个Roles [Authorize(Policy="AdminOrCommon")]

            #region 3. 自定义复杂的策略授权

            // 生成签名
            var secretKeyToByte = Encoding.UTF8.GetBytes(ConfigSetting.JwtTokenInfo.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretKeyToByte);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 动态数据库读取
            var permissionItems = new List<PermissionItem>();

            var permissionRequment = new PermissionRequirement(
                permissionItems,
                "api/auth/login",
                ClaimTypes.Role,
                ConfigSetting.JwtTokenInfo.Issuer,
                ConfigSetting.JwtTokenInfo.Audience,
                TimeSpan.FromMinutes(ConfigSetting.JwtTokenInfo.ExpiresTime),
                signingCredentials
                );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(permissionRequment));
            });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(p => permissionRequment);

            #endregion 3. 自定义复杂的策略授权
        }
    }
}