using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using Platform.Core.Data;

namespace Platform.Core
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件菜单资源
        /// </summary>
        System.Windows.Forms.ToolStripMenuItem[] PluginMenus { get; }

        /// <summary>
        /// 插件工具栏资源
        /// </summary>
        System.Windows.Forms.ToolStrip[] PluginTools { get; }

        /// <summary>
        /// 插件本身的Token标识，这个标识是唯一的（可对此Token添加检查，防止没授权的插件添加至平台中来）
        /// </summary>
        string Token { get;set; }

        /// <summary>
        /// 其它可活动资源的类全名，用于反射时构造这些资源时使用
        /// </summary>
        string MutableResourceClassFullName { get; set; }

        /// <summary>
        /// 插件关联的工程类全名，用于反射时构造工程资源时使用
        /// </summary>
        string ProjectClassFullName { get; set; }

        /// <summary>
        /// 插件关联的工程文件后缀
        /// </summary>
        string ProjectSuffix { get; set; }

        /// <summary>
        /// 从工程文件反序列化得到对应的工程
        /// </summary>
        /// <param name="path">工程文件路径</param>
        /// <returns>工程</returns>
        AbstractProject GetProjectFromPath(string path);

        /// <summary>
        /// 建立一个默认的工程
        /// </summary>
        /// <returns>工程</returns>
        AbstractProject GetDefaultProject();            

    }
   

    /// <summary>
    /// 插件的可活动资源
    /// </summary>
    public abstract class MutableResource
    {
        /// <summary>
        /// 活动资源关联的TreeView视图
        /// </summary>
        public abstract Platform.Core.UI.BaseForm ViewForm { get; set; }

        /// <summary>
        /// 活动资源关联的Tab窗体视图
        /// </summary>
        public abstract Platform.Core.UI.BaseForm TabForm { get; set; }

        /// <summary>
        /// 活动资源关联的Info窗体视图
        /// </summary>
        public abstract Platform.Core.UI.BaseForm InfoForm { get; set; }

        private bool Disposed = false;
        /// <summary>
        /// 在窗体销毁之前会调用此函数来完成工程数据的收集,可以重载
        /// </summary>
        protected virtual void CollectInf()
        {
            //在此可以关闭发起的连接等
        }
        /// <summary>
        /// 所关联的Project的ID
        /// </summary>
        public string UUID;

        /// <summary>
        /// 当关闭工程时，窗体销毁自身，销毁之前允许用户保存工程数据
        /// </summary>
        internal void DisposeSelf()
        {
            if (!Disposed)
            {
                Disposed = true;
                //先收集信息
                CollectInf();

                //依次销毁窗体
                if (this.ViewForm != null)
                {
                    this.ViewForm.Disposed += new EventHandler(ViewForm_Disposed);
                    this.TabForm.Dispose();
                }
                if (this.InfoForm != null)
                {
                    this.InfoForm.Disposed += new EventHandler(InfoForm_Disposed);
                    this.ViewForm.Dispose();
                }
                if (this.TabForm != null)
                {
                    this.TabForm.Disposed += new EventHandler(TabForm_Disposed);
                    this.InfoForm.Dispose();
                }
            }
        }
        /// <summary>
        /// 销毁View窗体资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void ViewForm_Disposed(object sender, EventArgs args)
        {
            ViewForm = null;
        }

        /// <summary>
        /// 销毁Tab窗体资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void TabForm_Disposed(object sender, EventArgs args)
        {
            TabForm = null;
        }

        /// <summary>
        /// 销毁Info窗体资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void InfoForm_Disposed(object sender, EventArgs args)
        {
            InfoForm = null;
        }
    }
}