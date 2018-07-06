using Castle.DynamicProxy;
using helpClass;
using log4net;
using System;

namespace SqlHelpCore.Interception
{
    public class ExeceptionInterceptor : IInterceptor
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        public ILog Logger { get; set; }

        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (HelpException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                LogHelper.Error(Logger, e.Message, e);
            }
        }
    }
}
