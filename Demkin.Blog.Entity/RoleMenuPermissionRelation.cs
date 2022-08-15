using Demkin.Blog.Entity.Root;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_RoleMenuPermissionRelation")]
    public class RoleMenuPermissionRelation : EntityRecordBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单权限表Id
        /// </summary>
        public long MenuPermissionId { get; set; }

        // 做传参作用
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(RoleId))]
        public Role Role { get; set; }

        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(MenuPermissionId))]
        public MenuPermission MenuPermission { get; set; }
    }
}