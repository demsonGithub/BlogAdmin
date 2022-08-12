using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.Entity;
using Demkin.Blog.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demkin.Blog.Repository
{
    public class RoleMenuPermissionRelationRepository : BaseRepository<RoleMenuPermissionRelation>, IRoleMenuPermissionRelationRepository
    {
        public RoleMenuPermissionRelationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap(long roleId)
        {
        }
    }
}