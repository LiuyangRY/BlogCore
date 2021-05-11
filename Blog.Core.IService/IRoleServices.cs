using Blog.Core.IService.Base;
using Blog.Core.Model.Models;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    public interface IRoleServices : IBaseService<Role>
    {
        Task<Role> SaveRole(string roleName);

        Task<string> GetRoleNameByRid(int rid);
    }
}