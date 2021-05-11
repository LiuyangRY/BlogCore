using System;

namespace Blog.Core.Model.Models
{
    public class UserRoleRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public TKey UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public TKey RoleId { get; set; }
    }
}