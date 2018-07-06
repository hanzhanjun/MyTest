using Autofac;
using SqlHelpCore.Interception;
using Autofac.Extras.DynamicProxy;
using log4net;

namespace SqlHelpCore
{
    public class Container
    {
        private static IContainer container;

        public static void RegisterAll()
        {
            var builder = new ContainerBuilder();

            #region Intercepter
            builder.Register(o => new ExeceptionInterceptor { Logger = LogManager.GetLogger("DEFAULT_LOGGER") });
            #endregion

            #region Service
            builder.RegisterType<Application.Impl.UserService>()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ExeceptionInterceptor))
               .As<Application.Interface.IUserService>();

            builder.RegisterType<Application.Impl.AdministratorService>()
               .EnableInterfaceInterceptors()
               .InterceptedBy(typeof(ExeceptionInterceptor))
               .As<Application.Interface.IAdministratorService>();

            #endregion

            container = builder.Build();
        }

        public static IContainer Instance
        {
            get
            {
                return container;
            }
        }
    }
}
