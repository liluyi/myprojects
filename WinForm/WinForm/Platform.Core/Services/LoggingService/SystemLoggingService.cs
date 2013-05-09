using System.IO;
using System;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using log4net;
using log4net.Config;
namespace Platform.Core.Services
{
    /// <summary>
    /// 系统日志服务
    /// </summary>
    internal class CommonLoggingService : ILoggingService
    {
        private string description = string.Empty;
        private ServiceState state = ServiceState.UnLoad;
        private InitServiceHandler initservice = null;

        #region IService 

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string Name
        {
            get
            {
                return "CommonLoggingService";
            }
        }

        public ServiceLevel Level
        {
            get
            {
                return ServiceLevel.High;
            }
        }

        public ServiceState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public InitServiceHandler InitService
        {
            get
            {
                return initservice;
            }
        }

        public EventHandler LoadingService
        {
            get
            {
                return null;
            }
        }

        #endregion

        private void Init(StartUpSettings sus)
        {
            if (sus == null)
            {
                return;
            }
            Properties p = sus.ServiceProperties["LoggingService"] as Properties;

            if (p == null)
            {
                return;
            }
            description = p["description"] as string;
        }

        public CommonLoggingService()
        {
            this.initservice = new InitServiceHandler(Init);

            //XmlConfigurator.Configure(new FileInfo("Core.xml"));

            //SystemLog = log4net.LogManager.GetLogger(typeof(CommonLoggingService));
        }

        public ICommonLogging GetModuleLogging(string modulename)
        {
            ILog log = log4net.LogManager.GetLogger("CommonLog."+modulename);

            ICommonLogging logging = new ICommonLogging(log);

            return logging;
        }
    }

    internal  class SystemLogging : ILogging
    {
        private ILog SystemLog;
        private static SystemLogging SystemLogger = new SystemLogging();

        public static ILogging SystemLoggingSingleton
        {
            get
            {
                return SystemLogger;
            }
        }
        private SystemLogging()
        {
            XmlConfigurator.Configure(new FileInfo("Core.xml"));
            SystemLog = log4net.LogManager.GetLogger("SystemLog");
        }
        public bool IsDebugEnabled
        {
            get
            {
                return SystemLog.IsDebugEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return SystemLog.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return SystemLog.IsWarnEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return SystemLog.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return SystemLog.IsFatalEnabled;
            }
        }

        public void Debug(object message)
        {
            SystemLog.Debug(message);
        }

        public void DebugFormatted(string format, params object[] args)
        {
            SystemLog.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            SystemLog.Info(message);
        }

        public void InfoFormatted(string format, params object[] args)
        {
            SystemLog.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            SystemLog.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            SystemLog.Warn(message, exception);
        }

        public void WarnFormatted(string format, params object[] args)
        {
            SystemLog.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            SystemLog.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            SystemLog.Error(message, exception);
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            SystemLog.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            SystemLog.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            SystemLog.Fatal(message, exception);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            SystemLog.FatalFormat(format, args);
        }
    }

    public class ICommonLogging : ILogging
    {
        private ILog commonLog;

        public ICommonLogging(ILog log)
        {
            this.commonLog = log;
        }

        public bool IsDebugEnabled
        {
            get
            {
                return commonLog.IsDebugEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return commonLog.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return commonLog.IsWarnEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return commonLog.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return commonLog.IsFatalEnabled;
            }
        }

        public void Debug(object message)
        {
            commonLog.Debug(message);
        }

        public void DebugFormatted(string format, params object[] args)
        {
            commonLog.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            commonLog.Info(message);
        }

        public void InfoFormatted(string format, params object[] args)
        {
            commonLog.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            commonLog.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            commonLog.Warn(message, exception);
        }

        public void WarnFormatted(string format, params object[] args)
        {
            commonLog.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            commonLog.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            commonLog.Error(message, exception);
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            commonLog.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            commonLog.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            commonLog.Fatal(message, exception);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            commonLog.FatalFormat(format, args);
        }
    }
}