using AutoMapper;
using Demkin.Blog.DTO.Permission;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionInsertDto, Permission>();

            CreateMap<Permission, PermissionInsertDto>();
        }
    }
}