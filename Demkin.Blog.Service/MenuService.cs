using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;

namespace Demkin.Blog.Service
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService(IBaseRepository<Menu> baseRepository) : base(baseRepository)
        {
        }
    }
}