using AutoMapper;
using Demkin.Blog.DTO.Role;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDetailDto>();

            CreateMap<RoleInsertDto, Role>();
        }
    }
}