using System;
namespace Platform.Core.Services
{
    /// <summary>
    /// 服务优先级别，AutoStart服务均为High，UserDefined服务默认为Common
    /// </summary>
    public enum ServiceLevel
    {
        High,
        Common,
        Low,
        UserDefined
    }

    /// <summary>
    /// 服务的状态
    /// </summary>
    public enum ServiceState
    {
        UnLoad,
        Inital,
        Load,
        Stop
    }

    /// <summary>
    /// 系统服务初始化委托
    /// </summary>
    /// <param name="sus"></param>
    public delegate void InitServiceHandler(StartUpSettings sus);

    /// <summary>
    /// 服务的接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// 当前服务的描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 当前服务的名称
        /// </summary>
        string Name { get;  }

        /// <summary>
        /// 当前服务的优先级
        /// </summary>
        ServiceLevel Level { get; }

        /// <summary>
        /// 当前服务的状态
        /// </summary>
        ServiceState State { get; set; }

        /// <summary>
        /// 服务初始化委托,初始化无误的话则State转为ServiceState.Inital状态
        /// 当加载系统服务时，使用系统配置文件,加载插件自定义服务时，传递的参数为null
        /// </summary>
        InitServiceHandler InitService { get; }

        /// <summary>
        /// 服务加载时委托，加载无误时则State转为Services.Load状态
        /// </summary>
        EventHandler LoadingService { get; }
    }

}
