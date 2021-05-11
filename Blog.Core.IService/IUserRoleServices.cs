using Blog.Core.IService.Base;
using Blog.Core.Model.Models;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    public interface IUserRoleServices : IBaseService<UserRole>
    {
        Task<UserRole> SaveUserRole(int userId, int roleId);

        Task<int> GetRoleIdByUserId(int userId);
    }
}