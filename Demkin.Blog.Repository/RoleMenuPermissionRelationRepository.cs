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

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            var result = await Db.Queryable<RoleMenuPermissionRelation>()
                .Includes(r => r.Role)
                .Includes(mp => mp.MenuPermission)
                            .ToListAsync();

            return result;
        }

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap(List<long> roleId)
        {
            var result = await Db.Queryable<RoleMenuPermissionRelation>()
                .Includes(r => r.Role)
                .Includes(mp => mp.MenuPermission)
                .WhereIF(roleId.Count > 0, item => roleId.Contains(item.RoleId))
                .ToListAsync();

            return result;
        }
    }
}