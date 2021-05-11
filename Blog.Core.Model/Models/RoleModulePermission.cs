using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class RoleModulePermission
    {
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; } = false;

        [SugarColumn(IsNullable = true)]
        public int? CreateId { get; set; }

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

        [SugarColumn(IsIgnore = true)]
        public Role Role { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Modules Module { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Permission Permission { get; set; }
    }
}