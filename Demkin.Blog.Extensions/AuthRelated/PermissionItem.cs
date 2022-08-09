namespace Demkin.Blog.Extensions.AuthRelated
{
    /// <summary>
    /// 权限凭据
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        /// 请求的Url
        /// </summary>
        public virtual string RequestUrl { get; set; }
    }
}