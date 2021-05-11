using Blog.Core.Common.Attributes;
using Blog.Core.Common.Helper;
using Blog.Core.Model.Models;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Service
{
    public class UserRoleServices : BaseService<UserRole>, IUserRoleServices
    {
        public UserRoleServices(IBaseRepository<UserRole> repository)
        {
            base.repository = repository;
        }

        public async Task<UserRole> SaveUserRole(int userId, int roleId)
        {
            UserRole model = new UserRole();
            var userList = (await Query(ur => ur.UserId.Equals(userId) && ur.RoleId.Equals(roleId))).ToList();
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await Add(new UserRole(userId, roleId));
                model = await QueryById(id);
            }
            return model;
        }

        [MethodCache(30)]
        public async Task<int> GetRoleIdByUserId(int userId)
        {
            return (await Query(ur => ur.UserId.Equals(userId)))
                .ToList()
                .OrderByDescending(ur => ur.Id)
                .LastOrDefault().RoleId
                .ObjToInt();
        }
    }
}