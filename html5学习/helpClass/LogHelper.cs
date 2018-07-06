using log4net;
using System;

namespace helpClass
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 默认的LOGGER实例
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger("DEFAULT_LOGGER");

        /// <summary>
        /// 记录普通文件记录
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 记录普通文件记录
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        public static void Info(ILog logger, string message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug(string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        ///记录调试信息
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        public static void Debug(ILog logger, string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        ///记录警告信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Warn(string message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        ///记录警告信息
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        public static void Warn(ILog logger, string message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        public static void Error(ILog logger, string message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(string message, Exception ex)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, ex);
            }
        }


        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(ILog logger, string message, Exception ex)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, ex);
            }
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="message">消息</param>
        public static void Fatal(string message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        public static void Fatal(ILog logger, string message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(string message, Exception ex)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, ex);
            }
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="logger">Logger实例</param>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(ILog logger, string message, Exception ex)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, ex);
            }
        }

    }
}
