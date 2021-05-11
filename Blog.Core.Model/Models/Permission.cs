using SqlSugar;
using System;
using System.Collections.Generic;

namespace Blog.Core.Model.Models
{
    public class Permission : PermissionRoot<int>
    {
        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string Code { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string Name { get; set; }

        [SugarColumn(IsNullable = true)]
        public bool? IsButton { get; set; } = false;

        [SugarColumn(IsNullable = true)]
        public bool? IsHide { get; set; } = false;

        [SugarColumn(IsNullable = true)]
        public bool? IsKeepAlive { get; set; } = false;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Func { get; set; }

        public int OrderSort { get; set; } = 1;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Icon { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Description { get; set; }

        [SugarColumn(IsNullable = true)]
        public bool? IsEnabled { get; set; } = true;

        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string CreatedBy { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreatedTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string ModifiedBy { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? ModifiedTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; } = false;

        [SugarColumn(IsIgnore = true)]
        public List<string> PNameArr { get; set; } = new List<string>();

        [SugarColumn(IsIgnore = true)]
        public List<string> PCodeArr { get; set; } = new List<string>();

        [SugarColumn(IsIgnore = true)]
        public string MName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public bool? HasChildren { get; set; } = true;
    }
}