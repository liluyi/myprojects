using System;

using Platform.Core;

namespace Platform.Core.Services
{
    /// <summary>
    /// ILogging
    /// </summary>
    public interface ILogging
    {
        void Debug(object message);
        void DebugFormatted(string format, params object[] args);
        void Info(object message);
        void InfoFormatted(string format, params object[] args);
        void Warn(object message);
        void Warn(object message, Exception exception);
        void WarnFormatted(string format, params object[] args);
        void Error(object message);
        void Error(object message, Exception exception);
        void ErrorFormatted(string format, params object[] args);
        void Fatal(object message);
        void Fatal(object message, Exception exception);
        void FatalFormatted(string format, params object[] args);
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
    }
    /// <summary>
    /// 日记服务接口
    /// </summary>
    public interface ILoggingService:IService
    {
        /// <summary>
        /// 通过模块名称获取本模块的日志引擎
        /// </summary>
        /// <param name="modulename">模块名称</param>
        /// <returns>日志引擎</returns>
        ICommonLogging GetModuleLogging(string modulename);
    }


}
