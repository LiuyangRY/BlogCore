using Blog.Core.IService.Base;
using Blog.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.IService
{
    public interface IRoleModulePermissionServices : IBaseService<RoleModulePermission>
    {
        Task<IEnumerable<RoleModulePermission>> GetRoleModule();

        Task<IEnumerable<RoleModulePermission>> RoleModuleMaps();

        Task<IEnumerable<RoleModulePermission>> GetRMPMaps();

        Task UpdateModuleId(int PermissionId, int ModuleId);
    }
}