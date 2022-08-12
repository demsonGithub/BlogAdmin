using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Demkin.Blog.Service
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<UserRoleRelation> _userRoleRelationRepository;

        public SysUserService(IBaseRepository<SysUser> baseRepository,
            IBaseRepository<Role> roleRepository,
            IBaseRepository<UserRoleRelation> userRoleRelationRepository
            ) : base(baseRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRelationRepository = userRoleRelationRepository;
        }

        public async Task<string> GetUserRoles(long userId)
        {
            string result = "";
            var roleList = await _roleRepository.GetEntityListAsync(item => item.Status == Enum.Status.Enable);

            var userRoles = await _userRoleRelationRepository.GetEntityListAsync(item => item.UserId == userId);

            if (userRoles.Count > 0)
            {
                var roleIdList = userRoles.Select(x => x.RoleId).ToList();

                var roleNames = roleList.Where(item => roleIdList.Contains(item.Id));

                result = string.Join(",", roleNames.Select(o => o.RoleName));
            }

            return result;
        }
    }
}