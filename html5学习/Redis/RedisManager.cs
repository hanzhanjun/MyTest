using StackExchange.Redis;
using helpClass;

namespace Redis
{
    public class RedisManager
    {
        private static string configString = RedisConfig.Config();
        private static readonly object locker = new object();
        private static ConnectionMultiplexer instance;

        /// <summary>
        /// 单例模式获取redis连接实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                            instance = GetManager();
                    }
                }
                return instance;
            }
        }

        private static ConnectionMultiplexer GetManager(string constr = null)
        {
            constr = constr ?? configString;
            var connect = ConnectionMultiplexer.Connect(constr);

            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return connect;
        }


        #region Redis事件

        /// <summary>
        /// 内部异常
        /// </summary>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            LogHelper.Error("内部异常：" + e.Exception.Message, e.Exception);
        }

        /// <summary>
        /// 集群更改
        /// </summary>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            LogHelper.Info("新集群：" + e.NewEndPoint + "旧集群：" + e.OldEndPoint);
        }

        /// <summary>
        /// 配置更改事件
        /// </summary>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            LogHelper.Info("配置更改：" + e.EndPoint);
        }

        /// <summary>
        /// 错误事件
        /// </summary>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            LogHelper.Error("异常信息：" + e.Message);
        }

        /// <summary>
        /// 重连错误事件
        /// </summary>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            LogHelper.Error("重连错误：" + e.EndPoint);
        }

        /// <summary>
        /// 连接失败事件
        /// </summary>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            LogHelper.Error("连接异常：" + e.EndPoint + "，类型：" + e.FailureType + (e.Exception == null ? "" : ("，异常信息：" + e.Exception.Message)));
        }

        #endregion
    }
}
