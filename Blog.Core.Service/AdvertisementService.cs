using Blog.Core.IService;
using Blog.Core.Model;
using Blog.Core.Repository.Base;

namespace Blog.Core.Service
{
    public class AdvertisementService : IAdvertisementService
    {
        IBaseRepository<Advertisement> repo;

        public AdvertisementService(IBaseRepository<Advertisement> repository)
        {
            repo = repository;
        }
    }
}
