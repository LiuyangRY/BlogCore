using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Repository
{
    public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>
    {
        Task<IEnumerable<RoleModulePermission>> RoleModuleMaps();

        Task<IEnumerable<RoleModulePermission>> GetRMPMaps();

        Task UpdateModuleId(int permissionId, int roleId);
    }
}