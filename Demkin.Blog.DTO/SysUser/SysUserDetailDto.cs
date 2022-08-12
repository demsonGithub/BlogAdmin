using System;

namespace Demkin.Blog.DTO.SysUser
{
    public class SysUserDetailDto
    {
        public string Id { get; set; }

        public string NickName { get; set; }

        public string Avatar { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }

        public string[] Roles { get; set; }
    }
}