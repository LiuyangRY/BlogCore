using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.IService.Base;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;

namespace Blog.Core.IService
{
    public interface IBlogArticleService : IBaseService<BlogArticleModel>
    {
        Task<IEnumerable<BlogArticleViewModel>> GetBlogs();

        Task<BlogArticleViewModel> GetBlogDetails(int id);
    }
}