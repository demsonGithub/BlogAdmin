using Demkin.Blog.Entity;
using Demkin.Blog.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Blog.Repository
{
    public interface IRoleMenuPermissionRelationRepository : IBaseRepository<RoleMenuPermissionRelation>
    {
        /// <summary>
        /// 角色、菜单、接口 映射关系
        /// </summary>
        /// <returns></returns>
        Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap();

        /// <summary>
        /// 根据角色Id获取 菜单、接口 映射关系
        /// </summary>
        /// <returns></returns>
        Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap(long roleId);
    }
}