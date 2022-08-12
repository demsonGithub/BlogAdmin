using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using System.Threading.Tasks;

namespace Demkin.Blog.IService
{
    public interface ISysUserService : IBaseService<SysUser>
    {
        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<string> GetUserRoles(long userId);
    }
}