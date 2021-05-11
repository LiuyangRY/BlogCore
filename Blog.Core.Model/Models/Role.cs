using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class Role
    {
        public Role(string name)
        {
            Name = name;
        }

        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; } = false;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string Name { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string Description { get; set; }

        public int OrderSort { get; set; } = 1;

        public bool Enabled { get; set; } = true;

        [SugarColumn(IsNullable = true)]
        public int?  CreateId { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string CreatedBy { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        [SugarColumn(IsNullable = true)]
        public int? ModifyId { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string ModifiedBy { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? ModifiedTime { get; set; } = DateTime.Now;
    }
}