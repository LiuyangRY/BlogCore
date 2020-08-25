using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.Core.IService;
using Blog.Core.Repository.Base;
using Blog.Core.Service;
using Microsoft.Extensions.Configuration;

namespace Blog.Core.Extensions.Service
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            #region 带有接口的服务注册

                // 注册泛型仓储
                builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

                // 注册服务
                builder.RegisterType<AdvertisementService>().As<IAdvertisementService>();
                
            #endregion
        }
    }   
}