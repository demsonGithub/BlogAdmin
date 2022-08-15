using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Demkin.Blog.Utils.SystemConfig;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demkin.Blog.Extensions.AuthRelated
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthenticationSchemeProvider _authenticationScheme;
        private readonly IRoleMenuPermissionRelationService _roleMenuPermissionRelationService;

        public PermissionHandler(IHttpContextAccessor contextAccessor, IAuthenticationSchemeProvider authenticationScheme, IRoleMenuPermissionRelationService roleMenuPermissionRelationService)
        {
            _contextAccessor = contextAccessor;
            _authenticationScheme = authenticationScheme;
            _roleMenuPermissionRelationService = roleMenuPermissionRelationService;
        }

        /// <summary>
        /// 重写异步处理程序
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = _contextAccessor.HttpContext;

            if (!requirement.PermissionItems.Any())
            {
                // 获取系统中所有的角色和菜单的关系集合
                var permissionDatas = await _roleMenuPermissionRelationService.GetRoleMenuPermissionMap();
                var permissionItemList = new List<PermissionItem>();
                // todo 从数据库中添加权限
                permissionItemList = (from item in permissionDatas
                                      select new PermissionItem
                                      {
                                          Role = item.RoleId.ToString(),
                                          RequestUrl = item.MenuPermission.LinkUrl
                                      }).ToList();
            }

            if (httpContext != null)
            {
                var requestUrl = httpContext.Request.Path.Value.ToLower();
                httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });
                // 判断当前是否需要进行远程验证
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await _authenticationScheme.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }

                // 判断请求是否有凭据
                var defaultAuthenticate = await _authenticationScheme.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    // 是否开启测试环境
                    var isTestCurrent = ConfigSetting.SiteInfo.IsOpenTest;

                    if (isTestCurrent)
                    {
                        context.Succeed(requirement);
                        return;
                    }

                    // result?.Principal不为空即已经登录有凭据
                    if (result?.Principal != null)
                    {
                        // 非测试，User赋值
                        httpContext.User = result?.Principal;

                        // 获取当前用户的角色信息
                        var currentUserRoles = new List<string>();

                        currentUserRoles = (from item in httpContext.User.Claims
                                            where item.Type == requirement.ClaimType
                                            select item.Value).ToList();

                        var isMatchRole = false;
                        var permissionRoles = requirement.PermissionItems.Where(i => currentUserRoles.Contains(i.Role));
                        foreach (var item in permissionRoles)
                        {
                            try
                            {
                                // 请求地址是以正斜杠 "/" 开头
                                if (Regex.Match(requestUrl, item.RequestUrl?.ObjToString().ToLower())?.Value == requestUrl)
                                {
                                    isMatchRole = true;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        // 验证权限
                        if (currentUserRoles.Count <= 0 || !isMatchRole)
                        {
                            context.Fail();
                            return;
                        }
                        // 判断是否在有效期内
                        var isExpiration = false;
                        isExpiration = (httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now;

                        if (isExpiration)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        return;
                    }
                    // 判断没有登录时，是否访问的是登录，并且是post，且form表单，那么视为失败
                    if (!(requestUrl.Equals(requirement.RedirectUrl.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)))
                    {
                        context.Fail();
                        return;
                    }
                }
            }
        }
    }
}