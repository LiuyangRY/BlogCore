using SqlSugar;
using System;
using System.Collections.Generic;

namespace Blog.Core.Model.Models
{
    public class PermissionRoot<TKey> : RootEntityTKey<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 上一级菜单（0表示上一级无菜单）
        /// </summary>
        /// <value></value>
        public TKey Pid { get; set; }
        
        /// <summary>
        /// 接口api
        /// </summary>
        /// <value></value>
        public TKey Mid { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<TKey> PidArr { get; set; }
    }
}