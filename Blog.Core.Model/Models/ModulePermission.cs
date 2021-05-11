using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class ModulePermission : ModulePermissionRoot<int>
    {
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }

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