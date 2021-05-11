using SqlSugar;
using System;
using System.Collections.Generic;

namespace Blog.Core.Model.Models
{
    public class SysUserInfoRoot<TKey> where TKey : IEquatable<TKey>
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public TKey UId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<TKey> RIds { get; set; }
    }
}