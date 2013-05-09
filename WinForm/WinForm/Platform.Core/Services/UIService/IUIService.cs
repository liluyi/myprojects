using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;
using Platform.Core.UI;
using System.Collections.Generic;

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

    public class MenuArgs : EventArgs
    {
        public int x;
        public int y;

        public MenuArgs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class UserUIEventArgs : EventArgs
    {
        public string UIItemID;
        public FormLoc ItemLocation=null;

        public UserUIEventArgs(string uiitemid,FormLoc itemlocation)
        {
            this.UIItemID = uiitemid;
            this.ItemLocation = itemlocation;
        }
        public UserUIEventArgs(string uiitemid)
        {
            this.UIItemID = uiitemid;
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
    public delegate BaseForm UIComHandler(Object sender,UserUIEventArgs args);
    public delegate List<string> UIDataHandler(Object sender, UserUIEventArgs args);
    public delegate ToolStripItem MenuHandler(object sender,MenuArgs args);
    public delegate ToolStrip ToolHandler(object sender,MenuArgs args);

    /// <summary>
    /// UI服务接口
    /// </summary>
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

        /// <summary>
        /// 工具运行时自定义窗体的添加于展现
        /// </summary>
        UIComHandler AddMutableResourceSelf { get; }

        /// <summary>
        /// 根据窗体ID获取当前窗体，方便系统交互
        /// </summary>
        UIComHandler GetUserForm { get; }

        UIDataHandler GetAllFormNames { get; }
        UIComHandler DeleteMutableResource { get; }

        UIComHandler ShowForm { get; }

        EventHandler DisposingUI { get; }
        EventHandler RefreshMainUI { get; }

        MenuHandler GetMenuStrip { get; }
        ToolHandler GetToolStrip { get; }
    }
}