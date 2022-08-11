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
    }
}