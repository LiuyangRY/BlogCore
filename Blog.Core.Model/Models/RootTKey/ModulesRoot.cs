using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class ModulesRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        [SugarColumn(IsNullable = true)]
        public TKey ParentId { get; set; }
    }
}