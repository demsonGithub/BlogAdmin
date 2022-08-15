using System.Collections.Generic;

namespace Demkin.Blog.DTO.RoleMenuPermissionRelation
{
    public class RoleMenuPermissionRelationDetailDto
    {
        public long Id { get; set; }

        public long ParentId { get; set; }

        public string RoleName { get; set; }

        public string MenuName { get; set; }

        public string LinkUrl { get; set; }

        public string Icon { get; set; }

        public List<RoleMenuPermissionRelationDetailDto> Children { get; set; }
    }
}