using Demkin.Blog.Entity;
using Demkin.Blog.IService.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demkin.Blog.IService
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<List<long>> GetRoleIdListByName(List<string> roleNames);
    }
}