using AutoMapper;
using Demkin.Blog.DTO.Menu;
using Demkin.Blog.Entity;

namespace Demkin.Blog.Extensions.AutoMapper
{
    public class MenuProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public MenuProfile()
        {
            CreateMap<Menu, MenuInsertDto>();
            CreateMap<Menu, MenuDetailDto>();

            CreateMap<MenuInsertDto, Menu>();
        }
    }
}