using System;

namespace Blog.Core.Api.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILoggerHelper
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">调试源</param>
        /// <param name="message">调试消息</param>
        void Debug(object source, string message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">调试源</param>
        /// <param name="message">调试消息</param>
        /// <param name="ps">参数</param>
        void Debug(object source, string message, params object[] ps);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Debug(Type source, string message);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Info(object source, string message);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Info(Type source, string message);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Warn(object source, string message);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Warn(Type source, string message);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Error(object source, string message);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Error(Type source, string message);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Fatal(object source, string message);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        void Fatal(Type source, string message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Debug(object source, object message, Exception exception);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Debug(Type source, object message, Exception exception);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Info(object source, object message, Exception exception);

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Info(Type source, object message, Exception exception);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Warn(object source, object message, Exception exception);

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Warn(Type source, object message, Exception exception);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Error(object source, object message, Exception exception);

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Error(Type source, object message, Exception exception);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Fatal(object source, object message, Exception exception);

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        void Fatal(Type source, object message, Exception exception);
    }
}