using System;
using System.Text;

namespace Platform.Core.Exceptions
{
    public class PluginException : CoreException
    {

    }

    public class PluginNotExsitException:PluginException
    {

    }

    public class PluginInfoNotExsitException : PluginException
    {

    }
    public class AddinFileNotInvaildException : PluginException
    {

    }

    /// <summary>
    /// 程序集不能加载
    /// </summary>
    public class AssemblyRefusedException : PluginException
    {

    }
    public class PluginBuildErrorException : PluginException
    {

    }

}