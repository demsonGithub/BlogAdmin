using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;

namespace Demkin.Blog.Service
{
    public class UserRoleRelationService : BaseService<UserRoleRelation>, IUserRoleRelationService
    {
        public UserRoleRelationService(IBaseRepository<UserRoleRelation> baseRepository) : base(baseRepository)
        {
        }
    }
}