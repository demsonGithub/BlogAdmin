using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;

namespace Demkin.Blog.Service
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        public SysUserService(IBaseRepository<SysUser> baseRepository) : base(baseRepository)
        {
        }
    }
}