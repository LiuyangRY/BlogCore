using Blog.Core.Common.Attributes;
using Blog.Core.Model.Models;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Service
{
    public class RoleServices : BaseService<Role>, IRoleServices
    {
        public RoleServices(IBaseRepository<Role> repository)
        {
            base.repository = repository;
        }

        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = (await Query(u => u.Name.Equals(role.Name) && u.Enabled)).ToList();
            if(userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await Add(role);
                model = await QueryById(id);
            }
            return model;
        }

        [MethodCache(30)]
        public async Task<string> GetRoleNameByRid(int roleId)
        {
            return (await QueryById(roleId))?.Name;
        }
    }
}