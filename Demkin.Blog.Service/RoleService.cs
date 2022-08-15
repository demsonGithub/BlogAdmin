using Demkin.Blog.DTO.Role;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demkin.Blog.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseRepository<Role> baseRepository) : base(baseRepository)
        {
        }

        public async Task<List<long>> GetRoleIdListByName(List<string> roleNames)
        {
            var roleFromDo = await GetEntityListAsync(item => roleNames.Contains(item.RoleName));

            var result = roleFromDo.Select(x => x.Id).ToList();
            return result;
        }
    }
}