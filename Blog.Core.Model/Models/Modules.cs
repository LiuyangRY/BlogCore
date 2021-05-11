using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class Modules : ModulesRoot<int>
    {
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; } = false;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string Name { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string LinkUrl { get; set; }
        
        [SugarColumn(ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string Area { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string Controller { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string Action { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Icon { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
        public string Code { get; set; }

        public int OrderSort { get; set; } = 1;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Description { get; set; }

        public bool IsMenu { get; set; }

        public bool IsEnabled { get; set; } = true;

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
    }
}