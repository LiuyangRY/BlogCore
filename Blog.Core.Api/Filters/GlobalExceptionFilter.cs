using System;
using Blog.Core.Api.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace Blog.Core.Api.Filters
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        private ILoggerHelper _logger;

        public GlobalExceptionFilter(IHostEnvironment environment, ILoggerHelper logger)
        {
            _env = environment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            JsonErrorResponse json = new JsonErrorResponse();
            json.Message = context.Exception.Message;
            if(_env.IsDevelopment())
            {
                json.DevelopmentMessage = context.Exception.StackTrace;     // 堆栈信息
            }

            context.Result = new InternalServerErrorObjectResult(json);

            // 日志记录错误信息
            _logger.Error(json.Message, WriteLog(json.Message, context.Exception));
        }

        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}",
                new object[] { throwMsg, ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }

    /// <summary>
    /// 内部服务器错误对象结果
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

    /// <summary>
    /// Json 格式错误响应信息
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 正式环境错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 开发环境错误信息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }
}