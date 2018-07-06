using System.IO;

namespace helpClass
{
    public class LogConfig
    {
        public static void Config()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Config(string path)
        {
            FileInfo config = new FileInfo(path);
            log4net.Config.XmlConfigurator.Configure(config);
        }

        public static void Config(FileInfo config)
        {
            log4net.Config.XmlConfigurator.Configure(config);
        }

    }
}
