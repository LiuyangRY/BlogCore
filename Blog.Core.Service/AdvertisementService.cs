using Blog.Core.Common.Attributes;
using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;

namespace Blog.Core.Service
{
    public class AdvertisementService : BaseService<AdvertisementModel>, IAdvertisementService
    {
        public AdvertisementService(IBaseRepository<AdvertisementModel> repository)
        {
            base.repository = repository;
        }

        [MethodCacheAttribute(20)]
        public int Sum(int first, int second)
        {
            return first + second;
        }
    }
}
