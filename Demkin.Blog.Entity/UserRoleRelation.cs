using Demkin.Blog.Entity.Root;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    [SugarTable("Auth_UserRoleRelation")]
    public class UserRoleRelation : EntityBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long RoleId { get; set; }
    }
}