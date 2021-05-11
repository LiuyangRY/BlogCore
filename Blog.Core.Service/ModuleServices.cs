using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Service.Base;
using Blog.Core.Repository.Base;

namespace Blog.Core.Service
{
    public class ModuleServices : BaseService<Modules>, IModuleServices
    {
        public ModuleServices(IBaseRepository<Modules> repo)
        {
            base.repository = repo;
        }
    }
}