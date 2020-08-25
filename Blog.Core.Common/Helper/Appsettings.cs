using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Blog.Core.Common.Helper
{
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }
        public Appsettings(string contentPath)
        {
            string path = "appsettings.json";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })
                .Build();
        }

        public Appsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置主键</param>
        /// <returns>节点配置值</returns>
        public static string App(params string[] sections)
        {
            try
            {
                if(sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        /// <summary>
        /// 递归获取配置信息集合
        /// </summary>
        /// <param name="sections">节点配置主键</param>
        /// <typeparam name="T">配置信息类型</typeparam>
        /// <returns>配置信息集合</returns>
        public static List<T> App<T>(params string[] sections)
        {
            List<T> list = new List<T>();
            Configuration.Bind(string.Join(":", sections), list);
            return list;
        }
    }
}
