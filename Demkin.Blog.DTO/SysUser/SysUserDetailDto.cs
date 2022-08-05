using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.DTO.SysUser
{
    public class SysUserDetailDto
    {
        public string Id { get; set; }

        public string NickName { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }
    }
}