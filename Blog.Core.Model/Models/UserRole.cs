using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    public class UserRole : UserRoleRoot<int>
    {
        public UserRole() {}
        
        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
            CreateId = userId;
        }
        
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

        [SugarColumn(IsNullable = true)]
        public string ModifiedBy { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public DateTime? ModifiedTime { get; set; } = DateTime.Now;
    }
}