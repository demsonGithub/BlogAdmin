using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            services.AddAuthorization(options =>
            {
                //options.AddPolicy
            });

            #endregion 3. 自定义复杂的策略授权
        }
    }
}