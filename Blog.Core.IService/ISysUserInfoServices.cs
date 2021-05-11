using Blog.Core.IService.Base;
using Blog.Core.Model.Models;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    public interface ISysUserInfoServices : IBaseService<SysUserInfo>
    {
        Task<SysUserInfo> SaveUserInfo(string loginName, string loginPwd);

        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}