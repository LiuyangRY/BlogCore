using log4net;
using System;
using System.Collections.Concurrent;

namespace Blog.Core.Api.Log
{
    /// <summary>
    /// 日志帮助实现类
    /// </summary>
    public class LogHelper : ILoggerHelper
    {
        private readonly ConcurrentDictionary<Type, ILog> Loggers = new ConcurrentDictionary<Type, ILog>();
    
        /// <summary>
        /// 获取记录器
        /// </summary>
        /// <param name="source">源</param>
        /// <returns>记录器</returns>
        private ILog GetLogger(Type source)
        {
            if(Loggers.ContainsKey(source))
            {
                return Loggers[source];
            }
            else
            {
                log4net.ILog logger = LogManager.GetLogger(Startup.repository.Name, source);
                Loggers.TryAdd(source, logger);
                return logger;
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Debug(object source, string message)
        {
            Debug(source.GetType(), message);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Info(object source, string message)
        {
            Info(source.GetType(), message);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Warn(object source, string message)
        {
            Warn(source.GetType(), message);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Error(object source, string message)
        {
            Error(source.GetType(), message);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Fatal(object source, string message)
        {
            Fatal(source.GetType(), message);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="ps">参数</param>
        public void Debug(object source, string message, params object[] ps)
        {
            Debug(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Debug(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if(logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="ps">参数</param>
        public void Info(object source, string message, params object[] ps)
        {
            Info(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Info(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if(logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="ps">参数</param>
        public void Warn(object source, string message, params object[] ps)
        {
            Warn(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Warn(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if(logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="ps">参数</param>
        public void Error(object source, string message, params object[] ps)
        {
            Error(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Error(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if(logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="ps">参数</param>
        public void Fatal(object source, string message, params object[] ps)
        {
            Fatal(source.GetType(), string.Format(message, ps));
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        public void Fatal(Type source, string message)
        {
            ILog logger = GetLogger(source);
            if(logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Debug(object source, object message, Exception exception)
        {
            Debug(source.GetType(), message, exception);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Debug(Type source, object message, Exception exception)
        {
            GetLogger(source).Debug(message, exception);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Info(object source, object message, Exception exception)
        {
            Info(source.GetType(), message, exception);
        }

        /// <summary>
        /// 关键信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Info(Type source, object message, Exception exception)
        {
            GetLogger(source).Info(message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Warn(object source, object message, Exception exception)
        {
            Warn(source.GetType(), message, exception);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Warn(Type source, object message, Exception exception)
        {
            GetLogger(source).Warn(message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Error(object source, object message, Exception exception)
        {
            Error(source.GetType(), message, exception);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Error(Type source, object message, Exception exception)
        {
            GetLogger(source).Error(message, exception);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Fatal(object source, object message, Exception exception)
        {
            Fatal(source.GetType(), message, exception);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="source">信息源</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public void Fatal(Type source, object message, Exception exception)
        {
            GetLogger(source).Fatal(message, exception);
        }
    }
}