namespace Demkin.Blog.DTO.RoleMenuPermissionRelation
{
    public class RoleMenuPermissionRelationInsertDto
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuPermissionId { get; set; }
    }
}