using helpClass;
using System;

namespace SqlHelpCore
{
    public class Startup
    {
        public static void Configuration()
        {
            LogConfig.Config(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            Container.RegisterAll();
        }
    }
}
