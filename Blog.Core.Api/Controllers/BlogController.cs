using System.Collections.Generic;
using Blog.Core.IService;
using Blog.Core.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 博客控制器
    /// </summary>
    [ApiController]
    public class BlogController : Controller
    {
        IBlogArticleService service;

        public BlogController(IBlogArticleService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public BlogArticleViewModel GetBlogById(int blogId)
        {
            return service.GetBlogDetails(blogId).Result;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IEnumerable<BlogArticleViewModel> GetBlogs()
        {
            return service.GetBlogs().Result;
        }
    }
}