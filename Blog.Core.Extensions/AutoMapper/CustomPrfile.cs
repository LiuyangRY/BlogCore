using AutoMapper;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;

namespace Blog.Core.Extensions.AutoMapper
{
    /// <summary>
    /// 根据IMapperTo<>接口，自动初始化AutoMapper
    /// </summary>
    public class CustomPrfile : Profile
    {
        public CustomPrfile()
        {
            CreateMap<BlogArticleModel, BlogArticleViewModel>();
            CreateMap<BlogArticleViewModel, BlogArticleModel>();
        }
    }
}