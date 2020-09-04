using Blog.Core.IService.Base;
using Blog.Core.Model.Models;

namespace Blog.Core.IService
{
    public interface IAdvertisementService : IBaseService<AdvertisementModel>
    {
        int Sum(int first, int second);
    }
}
