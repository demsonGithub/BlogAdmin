using Demkin.Blog.DTO.Role;
using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using System.Threading.Tasks;

namespace Demkin.Blog.IService
{
    public interface IRoleService : IBaseService<Role>
    {
        /// <summary>
        /// 增加角色
        /// </summary>
        /// <returns></returns>
        Task<Role> AddRole(RoleInsertDto entityDto);
    }
}