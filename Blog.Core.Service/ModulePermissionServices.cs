using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Service.Base;
using Blog.Core.Repository.Base;

namespace Blog.Core.Service
{
    public class ModulePermissionServices : BaseService<ModulePermission>, IModulePermissionServices
    {
        public ModulePermissionServices(IBaseRepository<ModulePermission> repo)
        {
            base.repository = repo;
        }
    }
}