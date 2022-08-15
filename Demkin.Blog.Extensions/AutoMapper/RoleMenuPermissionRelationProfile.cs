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
            CreateMap<RoleMenuPermissionRelation, RoleMenuPermissionRelationDetailDto>()
                .ForMember(td => td.RoleName, option => option.MapFrom(ts => ts.Role.RoleName))
                .ForMember(td => td.ParentId, option => option.MapFrom(ts => ts.MenuPermission.ParentId))
                .ForMember(td => td.MenuName, option => option.MapFrom(ts => ts.MenuPermission.Name))
                .ForMember(td => td.LinkUrl, option => option.MapFrom(ts => ts.MenuPermission.LinkUrl))
                .ForMember(td => td.Icon, option => option.MapFrom(ts => ts.MenuPermission.Icon));

            CreateMap<RoleMenuPermissionRelationInsertDto, RoleMenuPermissionRelation>();
        }
    }
}