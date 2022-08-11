namespace Demkin.Blog.DTO.RoleMenuPermissionRelation
{
    public class RoleMenuPermissionRelationInsertDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 操作按钮、链接等Id
        /// </summary>
        public long PermissionId { get; set; }
    }
}