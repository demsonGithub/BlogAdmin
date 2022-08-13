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
            CreateMap<RoleMenuPermissionRelation, RoleMenuPermissionRelationDetailDto>().ForMember(td => td.RoleName, option => option.MapFrom(ts => ts.Role.RoleName))
                .ForMember(td => td.MenuName, option => option.MapFrom(ts => ts.Menu.MenuName))
                .ForMember(td => td.LinkUrl, option => option.MapFrom(ts => ts.Menu.LinkUrl));

            CreateMap<RoleMenuPermissionRelationInsertDto, RoleMenuPermissionRelation>();
        }
    }
}