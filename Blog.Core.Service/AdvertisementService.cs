using Blog.Core.IService;
using Blog.Core.Model;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;

namespace Blog.Core.Service
{
    public class AdvertisementService : BaseService<Advertisement>, IAdvertisementService
    {
        public AdvertisementService(IBaseRepository<Advertisement> repository)
        {
            base.repository = repository;
        }
    }
}
