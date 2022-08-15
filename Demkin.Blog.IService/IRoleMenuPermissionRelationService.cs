using Demkin.Blog.DTO.RoleMenuPermissionRelation;
using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demkin.Blog.IService
{
    public interface IRoleMenuPermissionRelationService : IBaseService<RoleMenuPermissionRelation>
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
        Task<List<RoleMenuPermissionRelationDetailDto>> GetRoleMenuPermissionMap(List<long> roleIds);
    }
}