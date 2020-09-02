using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.Core.Common.Helper;
using Blog.Core.Common.Redis;
using Blog.Core.Extensions.AOP;
using Blog.Core.Extensions.Helper;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Core.Extensions.Service
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            #region 带有接口的服务注册
                #region AOP
                    List<Type> aopList = new List<Type>();
                    if(Appsettings.App( new string[] { "AppSettings", "RedisCacheAOP", "Enabled" }).ObjToBool())
                    {
                        builder.RegisterType<RedisCacheManager>().As(typeof(IRedisCacheManager)).SingleInstance();
                        builder.RegisterType<BlogRedisCacheAOP>();
                        aopList.Add(typeof(BlogRedisCacheAOP));
                    }
                    if(Appsettings.App( new string[] { "AppSettings", "MemoryCacheAOP", "Enabled" }).ObjToBool())
                    {
                        builder.RegisterType<Microsoft.Extensions.Caching.Memory.MemoryCache>().As(typeof(IMemoryCache)).SingleInstance();
                        builder.RegisterType<Helper.MemoryCache>().As(typeof(ICache)).SingleInstance();
                        builder.RegisterType<BlogCacheAOP>();
                        aopList.Add(typeof(BlogCacheAOP));
                    }
                    if(Appsettings.App( new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                    {
                        builder.RegisterType<BlogLogAOP>();
                        aopList.Add(typeof(BlogLogAOP));
                    }
                #endregion
                
                // 注册泛型仓储
                builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

                // 注册服务
                builder.RegisterType<AdvertisementService>().As<IAdvertisementService>()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(aopList.ToArray());
                
            #endregion
        }
    }   
}