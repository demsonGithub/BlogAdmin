using Demkin.Blog.DTO.RoleMenuPermissionRelation;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Repository;
using Demkin.Blog.Repository.Base;
using Demkin.Blog.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Blog.Service
{
    public class RoleMenuPermissionRelationService : BaseService<RoleMenuPermissionRelation>, IRoleMenuPermissionRelationService
    {
        private readonly IRoleMenuPermissionRelationRepository _roleMenuPermissionRelationRepository;

        public RoleMenuPermissionRelationService(
            IRoleMenuPermissionRelationRepository roleMenuPermissionRelationRepository
            ) : base(roleMenuPermissionRelationRepository)
        {
            _roleMenuPermissionRelationRepository = roleMenuPermissionRelationRepository;
        }

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            var result = await _roleMenuPermissionRelationRepository.GetRoleMenuPermissionMap();

            return result;
        }

        public async Task<List<RoleMenuPermissionRelationDetailDto>> GetRoleMenuPermissionMap(long roleId)
        {
            throw new NotImplementedException();
            //var result = await Db
            //return result;
        }
    }
}