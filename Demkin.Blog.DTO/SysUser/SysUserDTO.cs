namespace Demkin.Blog.DTO.SysUser
{
    public class SysUserDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginAccount { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}