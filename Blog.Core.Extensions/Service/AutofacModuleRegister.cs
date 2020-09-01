using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.Core.Extensions.AOP;
using Blog.Core.Extensions.Helper;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Blog.Core.Extensions.Service
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            #region 带有接口的服务注册
                // 注册缓存类型
                builder.RegisterType<Microsoft.Extensions.Caching.Memory.MemoryCache>().As(typeof(IMemoryCache)).SingleInstance();
                builder.RegisterType<Helper.MemoryCache>().As(typeof(ICache)).SingleInstance();
                // 注册拦截器
                builder.RegisterType<BlogLogAOP>();
                // 注册缓存拦截器
                builder.RegisterType<BlogCacheAOP>();
                // 注册泛型仓储
                builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

                // 注册服务
                builder.RegisterType<AdvertisementService>().As<IAdvertisementService>()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(BlogLogAOP), typeof(BlogCacheAOP));
                
            #endregion
        }
    }   
}