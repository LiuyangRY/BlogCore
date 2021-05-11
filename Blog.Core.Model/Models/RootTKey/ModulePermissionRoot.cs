using System;

namespace Blog.Core.Model.Models
{
    public class ModulePermissionRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        public TKey ModuleId { get; set; }

        public TKey PermissionId { get; set; }
    }
}