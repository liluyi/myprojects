using System;
using System.Text;

namespace Platform.Core.Exceptions
{
    public class ServiceNotExsitException : CoreException
    {

    }
    public class ServiceNotInvaildException : CoreException
    {

    }
    public class FileServiceException : CoreException
    {
        public FileServiceException(string msg,Exception innerException):base(msg,innerException)
        {
            this.ModuleName = "FileService";
            this.Level = ExceptionLevel.High;
        }
    }

    public class PropertyException : CoreException
    {
        public PropertyException(string msg, Exception innerException)
            : base(msg, innerException)
        {
            this.ModuleName = "Property";
            this.Level = ExceptionLevel.High;
        }
    }

    public class UserDefinedServiceNotInvaildException : CoreException
    {

    }
}