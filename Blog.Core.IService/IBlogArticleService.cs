using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.IService.Base;
using Blog.Core.Model.Models;

namespace Blog.Core.IService
{
    public interface IBlogArticleService : IBaseService<BlogArticleModel>
    {
        Task<IEnumerable<BlogArticleModel>> GetBlogs();

        Task<BlogArticleModel> GetBlogDetails(int id);
    }
}