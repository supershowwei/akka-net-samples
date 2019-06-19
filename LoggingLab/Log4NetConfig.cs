using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace LoggingLab
{
    internal class Log4NetConfig
    {
        public static void Init()
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            GlobalContext.Properties["ApplicationName"] = Assembly.GetExecutingAssembly().GetName().Name;
            GlobalContext.Properties["CurrentDirectory"] = currentDirectory;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(currentDirectory, "log4net.config")));
        }
    }
}