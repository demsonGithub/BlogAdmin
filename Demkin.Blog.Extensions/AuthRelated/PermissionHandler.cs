using Demkin.Blog.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                                          RequestUrl = item.PermissionId.ToString()
                                      }).ToList();
            }

            // todo
            if (httpContext != null)
            {
                var requestUrl = httpContext.Request.Path.Value.ToLower();
            }
        }
    }
}