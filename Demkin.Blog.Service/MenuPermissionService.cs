using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;

namespace Demkin.Blog.Service
{
    public class MenuPermissionService : BaseService<MenuPermission>, IMenuPermissionService
    {
        public MenuPermissionService(IBaseRepository<MenuPermission> baseRepository) : base(baseRepository)
        {
        }
    }
}