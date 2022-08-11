using AutoMapper;
using Demkin.Blog.DTO.RoleMenuPermissionRelation;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class RoleMenuPermissionRelationProfile : Profile
    {
        public RoleMenuPermissionRelationProfile()
        {
            CreateMap<RoleMenuPermissionRelation, RoleMenuPermissionRelationInsertDto>();
            CreateMap<RoleMenuPermissionRelationInsertDto, RoleMenuPermissionRelation>();
        }
    }
}