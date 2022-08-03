using Demkin.Blog.Entity.Root;
using Demkin.Blog.Enum;
using SqlSugar;

namespace Demkin.Blog.Entity
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [SugarTable("Auth_SysUser")]
    public class SysUser : EntityRecordBase
    {
        public SysUser()
        {
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 60)]
        public string LoginAccount { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 60)]
        public string LoginPwd { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 60)]
        public string NickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 60)]
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 11)]
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 255)]
        public string Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, Length = int.MaxValue)]
        public string Remark { get; set; }
    }
}