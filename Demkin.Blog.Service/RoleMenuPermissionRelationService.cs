using Demkin.Blog.DTO.RoleMenuPermissionRelation;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
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
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IBaseRepository<Menu> _menuRepository;
        private readonly IBaseRepository<Permission> _permissionRepository;

        public RoleMenuPermissionRelationService(
            IBaseRepository<RoleMenuPermissionRelation> baseRepository,
            IBaseRepository<Role> roleRepository,
            IBaseRepository<Menu> menuRepository,
            IBaseRepository<Permission> permissionRepository
            ) : base(baseRepository)
        {
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            var roleMenuPermissionListFromDo = await GetEntityListAsync();
            var roleListFromDo = await _roleRepository.GetEntityListAsync();
            var menuListFromDo = await _menuRepository.GetEntityListAsync();
            var permissionListFromDo = await _permissionRepository.GetEntityListAsync();

            if (roleMenuPermissionListFromDo.Count > 0)
            {
                foreach (var item in roleMenuPermissionListFromDo)
                {
                    item.Role = roleListFromDo.FirstOrDefault(r => r.Id == item.RoleId);
                    item.Menu = menuListFromDo.FirstOrDefault(m => m.Id == item.MenuId);
                    item.Permission = permissionListFromDo.FirstOrDefault(p => p.Id == item.PermissionId);
                }
            }

            return roleMenuPermissionListFromDo;
        }

        public async Task<List<RoleMenuPermissionRelationDetailDto>> GetRoleMenuPermissionMap(long roleId)
        {
            var result = await Db
            return result;
        }

        //public async Task<List<RoleMenuPermissionRelationDetailDto>> GetRoleMenuPermissionMap(long roleId)
        //{
        //    var roleMenuPermissionListFromDo = await GetEntityListAsync(item => item.RoleId == roleId);
        //    var roleListFromDo = await _roleRepository.GetEntityListAsync();
        //    var menuListFromDo = await _menuRepository.GetEntityListAsync();
        //    var permissionListFromDo = await _permissionRepository.GetEntityListAsync();

        // List<RoleMenuPermissionRelationDetailDto> result = new List<RoleMenuPermissionRelationDetailDto>();

        // if (roleMenuPermissionListFromDo.Count > 0) { foreach (var item in
        // roleMenuPermissionListFromDo) { item.Role = roleListFromDo.FirstOrDefault(r => r.Id ==
        // item.RoleId); item.Menu = menuListFromDo.FirstOrDefault(m => m.Id == item.MenuId);
        // item.Permission = permissionListFromDo.FirstOrDefault(p => p.Id == item.PermissionId);

        // var detailEntity = new RoleMenuPermissionRelationDetailDto { RoleName =
        // item.Role.RoleName, MenuName = item.Menu.MenuName, LinkUrl = item.Menu.LinkUrl };

        //            result.Add(detailEntity);
        //        }
        //    }
        //    return result;
        //}
    }
}