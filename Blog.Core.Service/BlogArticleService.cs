using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;

namespace Blog.Core.Service
{
    public class BlogArticleService : BaseService<BlogArticleModel>, IBlogArticleService
    {

        public BlogArticleService(IBaseRepository<BlogArticleModel> repo)
        {
            base.repository = repo;
        }

        public Task<BlogArticleModel> GetBlogDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BlogArticleModel>> GetBlogs()
        {
            return await base.Query(b => b.ID > 0, b => b.ID);
        }
    }
}