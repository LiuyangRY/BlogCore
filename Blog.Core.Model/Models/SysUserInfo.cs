using SqlSugar;
using System;
using System.Collections.Generic;

namespace Blog.Core.Model.Models
{
    public class SysUserInfo : SysUserInfoRoot<int>
    {
        public SysUserInfo() {}

        public SysUserInfo(string loginName, string loginPwd)
        {
            LoginName = loginName;
            LoginPwd = loginPwd;
            RealName = loginName;
        } 

        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string LoginName { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string LoginPwd { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string RealName { get; set; }

        public int Status { get; set; } = 0;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string Remark { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreatedTime { get; set; } = DateTime.Now;

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; } = DateTime.Now;

        [SugarColumn(IsNullable = true)]
        public DateTime? LastLoginTime { get; set; } = DateTime.Now;

        public int ErrorCount { get; set; } = 0;

        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Name { get; set; }

        [SugarColumn(IsNullable = true)]
        public int Sexual { get; set; }

        [SugarColumn(IsNullable = true)]
        public int Age { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? BirthDay { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Address { get; set; }

        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; } = false;

        [SugarColumn(IsNullable = true)]
        public List<string> RoleNames { get; set; }
    }
}