using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.DTO.Role
{
    public class RoleInsertDto
    {
        public string RoleName { get; set; }

        public string Description { get; set; }

        public int SortNumber { get; set; }
    }
}