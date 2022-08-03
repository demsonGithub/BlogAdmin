using Demkin.Blog.Entity.Root;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_RoleMenuPermissionRelation")]
    public class RoleMenuPermissionRelation : EntityBase
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