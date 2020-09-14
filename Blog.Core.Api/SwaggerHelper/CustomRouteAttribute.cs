using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Blog.Core.Api.SwaggerHelper
{
    /// <summary>
    /// 自定义路由
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        public CustomRouteAttribute(CustomApiVersion version, string actionName = "[action]") : base($"api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }

        /// <summary>
        /// API分组名称
        /// </summary>
        /// <value></value>
        public string GroupName { get; set; }
    }
}