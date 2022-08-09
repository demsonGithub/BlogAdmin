using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Extensions.AuthRelated
{
    /// <summary>
    /// 必要的参数类，继承IAuthorizationRequment，用于设计自定义权限处理器PermissionHandler
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<PermissionItem> PermissionItems { get; set; }

        /// <summary>
        /// 跳转路径
        /// </summary>
        public string RedirectUrl { get; set; } = "/api/login";

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan ExpiresTime { get; set; }

        /// <summary>
        /// 签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        public PermissionRequirement(List<PermissionItem> permissionItems, string redirectUrl, string claimType, string issuer, string audience, TimeSpan expiresTime, SigningCredentials signingCredentials)
        {
            PermissionItems = permissionItems;
            RedirectUrl = redirectUrl;
            ClaimType = claimType;
            Issuer = issuer;
            Audience = audience;
            ExpiresTime = expiresTime;
            SigningCredentials = signingCredentials;
        }
    }
}