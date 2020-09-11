using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.IService;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Repository.Base;
using Blog.Core.Service.Base;

namespace Blog.Core.Service
{
    public class BlogArticleService : BaseService<BlogArticleModel>, IBlogArticleService
    {
        IMapper mapper;

        public BlogArticleService(IBaseRepository<BlogArticleModel> repo, IMapper mapper)
        {
            base.repository = repo;
            this.mapper = mapper;
        }

        public async Task<BlogArticleModel> GetBlogDetails(int id)
        {
            var blogList = await repository.Query(a => a.ID > 0, a => a.ID);
            var blogArticle = await repository.QueryByID(id);
            BlogArticleViewModel blogViewModel = null;
            if(blogArticle != null)
            {
                BlogArticleModel preBlog, nextBlog;
            }
        }

        public async Task<IEnumerable<BlogArticleModel>> GetBlogs()
        {
            return await base.Query(b => b.ID > 0, b => b.ID);
        }
    }
}