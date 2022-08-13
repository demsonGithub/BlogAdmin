using Demkin.Blog.DbAccess.UnitOfWork;
using Demkin.Blog.Entity;
using Demkin.Blog.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Demkin.Blog.Repository
{
    public class RoleMenuPermissionRelationRepository : BaseRepository<RoleMenuPermissionRelation>, IRoleMenuPermissionRelationRepository
    {
        public RoleMenuPermissionRelationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            var result = await Db.Queryable<RoleMenuPermissionRelation>()
                .Includes(r => r.Role)
                .Includes(m => m.Menu)
                .Includes(p => p.Permission)
                            .ToListAsync();

            return result;
        }

        public Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap(long roleId)
        {
            throw new NotImplementedException();
        }
    }
}