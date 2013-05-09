using System;
using System.Text;

namespace Platform.Core.Exceptions
{
    /// <summary>
    /// 异常级别
    /// </summary>
    public enum ExceptionLevel
    {
        High,
        Common,
        Low
    }

    /// <summary>
    /// 应用程序框架异常Exception基类
    /// </summary>
    public class CoreException : System.ApplicationException
    {
        /// <summary>
        /// 引发异常的模块名称
        /// </summary>
        private string m_ModuleName;

        /// <summary>
        /// 是否引用日志记录，默认值为true
        /// </summary>
        private bool m_LogException = true;

        /// <summary>
        /// 异常的级别,默认为低
        /// </summary>
        private ExceptionLevel m_Level = ExceptionLevel.Low;

        /// <summary>
        /// 引发异常模块属性
        /// </summary>
        public string ModuleName
        {
            get
            {
                return m_ModuleName;
            }
            set
            {
                m_ModuleName = value;
            }
        }

        /// <summary>
        /// 异常级别
        /// </summary>
        public ExceptionLevel Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
            }
        }

        /// <summary>
        /// 记录日志属性
        /// </summary>
        public bool LogException
        {
            get
            {
                return m_LogException;
            }
        }

        public CoreException() : base() { }

        public CoreException(string message)
            : base(message)
        {
            
        }

        public CoreException(string message, bool log)
            : base(message)
        {
            this.m_LogException = log;
        }

        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CoreException(string message, Exception innerException,bool log)
            : base(message, innerException)
        {
            this.m_LogException = log;
        }

        public CoreException(object source, string message, Exception innerException):base(message,innerException)
        {
            this.m_ModuleName = source.ToString();
        }

        public CoreException(object source, string message, Exception innerException, ExceptionLevel level)
            :base(message,innerException)
        {
            this.m_ModuleName = source.ToString();

            this.m_Level = level;
        }

        public CoreException(object source, string message, ExceptionLevel level)
            : base(message)
        {
            this.m_ModuleName = source.ToString();

            this.m_Level = level;
        }

        public CoreException(object source, string message, Exception innerException,bool log)
            : base(message, innerException)
        {
            this.m_ModuleName = source.ToString();
            this.m_LogException = log;
        }
        public override string Message
        {
            get
            {
                string basemsg = base.Message;
                StringBuilder sb = new StringBuilder();
                sb.Append("Module:" + m_ModuleName + "\n")
                    .Append(" Level:" + m_Level.ToString() + "\n")
                    .Append("  Info:" + basemsg);
                return sb.ToString();
            }
        }
    }

   
}
