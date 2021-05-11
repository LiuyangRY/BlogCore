using Blog.Core.Common.Attributes;
using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Service.Base;
using Blog.Core.Repository;
using Blog.Core.Repository.Base;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Service
{
    public class RoleModulePermissionServices : BaseService<RoleModulePermission>, IRoleModulePermissionServices
    {
        readonly IRoleModulePermissionRepository rmprepository;
        readonly IBaseRepository<Modules> moduleRepository;
        readonly IBaseRepository<Role> roleRepository;

        public RoleModulePermissionServices(IRoleModulePermissionRepository repo, IBaseRepository<Modules> moduleRepo, IBaseRepository<Role> roleRepo)
        {
            rmprepository = repo;
            moduleRepository = moduleRepo;
            roleRepository = roleRepo;
        }

        [MethodCache(10)]
        public async Task<IEnumerable<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = (await Query(a => a.IsDeleted.Equals(false))).ToList();
            var roles = await roleRepository.Query(r => r.IsDeleted.Equals(false));
            var Modules = await moduleRepository.Query(m => m.IsDeleted.Equals(false));

            if(roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(r => r.Id.Equals(item.Role.Id));
                    item.Module = Modules.FirstOrDefault(m => m.Id.Equals(item.Module.Id));
                }
            }
            return roleModulePermissions;
        }

        public async Task<IEnumerable<RoleModulePermission>> RoleModuleMaps()
        {
            return await rmprepository.RoleModuleMaps();
        }

        public async Task<IEnumerable<RoleModulePermission>> GetRMPMaps()
        {
            return await rmprepository.GetRMPMaps();
        }

        public async Task UpdateModuleId(int permissionId, int ModuleId)
        {
            await rmprepository.UpdateModuleId(permissionId, ModuleId);
        }
    }
}