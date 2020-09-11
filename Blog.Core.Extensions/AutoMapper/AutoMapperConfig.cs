using AutoMapper;

namespace Blog.Core.Extensions.AutoMapper
{
    /// <summary>
    /// 静态全局AutoMapper配置类
    /// </summary>
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new CustomPrfile());
            });
        }
    }
}