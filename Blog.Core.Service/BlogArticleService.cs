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

        public async Task<BlogArticleViewModel> GetBlogDetails(int id)
        {
            var blogArticle = await repository.QueryByID(id);
            BlogArticleViewModel blogViewModel = null;

            if(blogArticle != null)
            {
                blogViewModel = mapper.Map<BlogArticleViewModel>(blogArticle);
                var nextBlog = (await Query(b => b.ID >= id && b.IsDeleted == false, 2, "ID")).ToList();
                if(nextBlog.Count == 2)
                {
                    blogViewModel.NextID = nextBlog[1].ID;
                    blogViewModel.Next = nextBlog[1].Title;
                }
                var prevBlog = (await Query(b => b.ID <= id && b.IsDeleted == false, 2, "ID DESC")).ToList();
                if(prevBlog.Count == 2)
                {
                    blogViewModel.PreviousID = prevBlog[1].ID;
                    blogViewModel.Previous = prevBlog[1].Title;
                }
                blogArticle.Traffic += 1;
                await Update(blogArticle, new List<string>{ "Traffic" });
            }
            return blogViewModel;
        }

        public async Task<IEnumerable<BlogArticleViewModel>> GetBlogs()
        {
            var blogArticles = await base.Query(b => b.ID > 0, b => b.ID);
            List<BlogArticleViewModel> result = new List<BlogArticleViewModel>();
            foreach (var blog in blogArticles)
            {
                result.Add(mapper.Map<BlogArticleViewModel>(blog));
            }
            return result;
        }
    }
}