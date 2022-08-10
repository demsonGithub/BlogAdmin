using Demkin.Blog.DTO.Role;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using System.Threading.Tasks;

namespace Demkin.Blog.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IBaseRepository<Role> baseRepository) : base(baseRepository)
        {
        }

        public async Task<Role> AddRole(RoleInsertDto entityDto)
        {
            Role entity = new Role
            {
                RoleName = entityDto.RoleName,
                Description = entityDto.Description,
                SortNumber = entityDto.SortNumber
            };

            var result = await AddAsync(entity);

            return result;
        }
    }
}