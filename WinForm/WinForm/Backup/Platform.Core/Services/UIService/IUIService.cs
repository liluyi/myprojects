using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;

namespace Platform.Core.Services
{
    public class UIEventArgs : EventArgs
    {
        public string fullclassname;
        public string uuid;

        public UIEventArgs(string fullclassname, string uuid)
        {
            this.fullclassname = fullclassname;
            this.uuid = uuid;
        }
    }

    public class ProjectUIArgs : UIEventArgs
    {
        public string projectname;
        public string projectpath;

        public ProjectUIArgs(string projectname, string projectpath, string fullclassname, string uuid)
            : base(fullclassname, uuid)
        {
            this.projectname = projectname;
            this.projectpath = projectpath;
        }
    }
    public delegate bool UIHandler(IPlugin plugin, UIEventArgs args);

    public interface IUIService : IService
    {
        /// <summary>
        /// 在平台初始化装载插件UI，主要是负责菜单和工具栏的装载
        /// </summary>
        UIHandler LoadUI_CreatePlugin { get; }

        /// <summary>
        /// 在平台建立某工程时UI的展现，主要是View，Tab，Info等窗体的装载
        /// </summary>
        UIHandler LoadUI_CreateProject { get; }

    }
}