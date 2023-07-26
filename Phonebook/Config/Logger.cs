using log4net;
using log4net.Config;

namespace Phonebook.Config
{
    public static class Logger
    {
        private static readonly ILog log;

        static Logger()
        {
            if (log != null)
                return;

            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(Logger));
        }

        public static void Info(string message, params object[] args)
        {
            log.Info(FormatMessage(message, args));
        }

        public static void Debug(string message, params object[] args)
        {
            log.Debug(FormatMessage(message, args));
        }

        public static void Warn(string message, params object[] args)
        {
            log.Warn(FormatMessage(message, args));
        }

        public static void Error(string message, params object[] args)
           => log.Error($"[ERROR]: {FormatMessage(message, args)}");

        private static string FormatMessage(string message, params object[] args)
         => args.Length > 0 ? string.Format(message, args) : message;
    }
}
