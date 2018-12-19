using Microsoft.Extensions.Logging;

namespace ProjectTemplate.Core
{
    public class AppLogger
    {
        private static ILoggerFactory loggerFactory = null;

        public static ILogger Logger { get; private set; }

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                return loggerFactory ?? new LoggerFactory();
            }
            
        }

        public static void Configure(ILoggerFactory factory)
        {
           
            Logger = factory.CreateLogger("AppLogger");

            Logger.LogWarning("======================== Application logger configured =====================");

            loggerFactory = factory;
        }
    }
}