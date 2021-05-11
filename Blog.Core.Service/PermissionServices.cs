using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Service.Base;
using Blog.Core.Repository.Base;

namespace Blog.Core.Service
{
    public class PermissionServices : BaseService<Permission>, IPermissionServices
    {
        public PermissionServices(IBaseRepository<Permission> repo)
        {
            base.repository = repo;
        }
    }
}