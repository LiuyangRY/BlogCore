using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public TKey Id { get; set; }
    }
}