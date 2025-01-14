﻿namespace Demkin.Blog.DTO.UserRoleRelation
{
    public class UserRoleRelationInsertDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}