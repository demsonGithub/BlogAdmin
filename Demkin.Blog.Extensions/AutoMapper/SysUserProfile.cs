using AutoMapper;
using Demkin.Blog.DTO.SysUser;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class SysUserProfile : Profile
    {
        public SysUserProfile()
        {
            CreateMap<SysUser, SysUserDetailDto>();
            CreateMap<SysUserDetailDto, SysUser>();
        }
    }
}