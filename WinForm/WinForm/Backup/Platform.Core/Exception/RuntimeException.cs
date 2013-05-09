using System;


namespace Platform.Core.Exceptions
{
    public class RuntimeConfigInvaildException : CoreException
    {
        public RuntimeConfigInvaildException(string msg, Exception innerException)
            : base(msg, innerException)
        {
        }
    }
}