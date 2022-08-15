using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IRoleMenuPermissionRelationRepository _roleMenuPermissionRelationRepository;

        public RoleMenuPermissionRelationService(
            IMapper mapper,
            IRoleMenuPermissionRelationRepository roleMenuPermissionRelationRepository
            ) : base(roleMenuPermissionRelationRepository)
        {
            _mapper = mapper;
            _roleMenuPermissionRelationRepository = roleMenuPermissionRelationRepository;
        }

        public async Task<List<RoleMenuPermissionRelation>> GetRoleMenuPermissionMap()
        {
            var result = await _roleMenuPermissionRelationRepository.GetRoleMenuPermissionMap();

            return result;
        }

        public async Task<List<RoleMenuPermissionRelationDetailDto>> GetRoleMenuPermissionMap(List<long> roleIds)
        {
            var roleMenuPermissionMapFromDo = await _roleMenuPermissionRelationRepository.GetRoleMenuPermissionMap(roleIds);

            var result = _mapper.Map<List<RoleMenuPermissionRelationDetailDto>>(roleMenuPermissionMapFromDo);

            return result;
        }
    }
}