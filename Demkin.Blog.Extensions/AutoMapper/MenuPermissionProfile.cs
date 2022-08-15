using AutoMapper;
using Demkin.Blog.DTO.MenuPermission;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class MenuPermissionProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public MenuPermissionProfile()
        {
            CreateMap<MenuPermission, MenuDetailDto>().ForMember(td => td.MenuName, opt => opt.MapFrom(ts => ts.Name));
            CreateMap<MenuPermission, MenuPermissionInsertDto>();

            CreateMap<MenuPermissionInsertDto, MenuPermission>();
            CreateMap<MenuDetailDto, MenuPermission>();
        }
    }
}