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
        public long MenuPermissionId { get; set; }
    }
}