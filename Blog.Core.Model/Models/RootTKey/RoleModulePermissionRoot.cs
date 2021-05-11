using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class RoleModulePermissionRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        public TKey RoleId { get; set; }

        public TKey ModuleId { get; set; }

        [SugarColumn(IsNullable = true)]
        public TKey PermissionId { get; set; }
    }   
}