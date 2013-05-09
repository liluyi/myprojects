using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core.Data;
using Platform.Core;
using Platform.Core.UI;
using Platform.Core.Services;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Platform.Core
{
    /// <summary>
    /// 插件的可活动资源
    /// </summary>
    public abstract class MutableResource
    {
        /// <summary>
        /// 浮动资源词典，维护全部用户自定义窗体
        /// </summary>
        public abstract Dictionary<string, Platform.Core.UI.BaseForm> ToolFormDictionary { get; }
        public abstract Dictionary<string, FormLoc> FormLocationDictionary { get; }

        /// <summary>
        /// 插件菜单资源
        /// </summary>
        public abstract System.Windows.Forms.ToolStripMenuItem[] PluginMenus { get; }

        /// <summary>
        /// 插件工具栏资源
        /// </summary>
        public abstract System.Windows.Forms.ToolStrip[] PluginTools { get; }

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

        /// <summary>
        /// 活动资源关联的Server窗体视图
        /// </summary>
        public abstract Platform.Core.UI.BaseForm ServerForm { get; set; }

        /// <summary>
        /// 添加新的窗体
        /// </summary>
        /// <param name="form"></param>
        public void AddResource(string formname, Platform.Core.UI.BaseForm form)
        {
            foreach (string name in ToolFormDictionary.Keys)
                if (name.Equals(formname))
                    return;
            ToolFormDictionary.Add(formname, form);
        }

        public void DeleteResource(string formname)
        {
            foreach (string name in ToolFormDictionary.Keys)
                if (name.Equals(formname))
                {
                    ToolFormDictionary.Remove(formname);
                    FormLocationDictionary.Remove(formname);
                }
        }

        public void AddLocation(string formname, FormLoc location)
        {
            foreach (string name in FormLocationDictionary.Keys)
                if (name.Equals(formname))
                    return;
            FormLocationDictionary.Add(formname, location);
        }

        /// <summary>
        /// 通过窗体名称获取词典中的窗体
        /// </summary>
        /// <param name="formname">窗体名称</param>
        /// <returns></returns>
        public Platform.Core.UI.BaseForm GetResource(string formname)
        {
            Platform.Core.UI.BaseForm form = null;
            foreach (KeyValuePair<string, Platform.Core.UI.BaseForm> pair in ToolFormDictionary)
            {
                if (pair.Key == formname)
                    form = pair.Value;
            }
            if (form == null)
            {
                if (formname == "TreeViewForm")
                    form = ViewForm;
                else if (formname == "TabViewForm")
                    form = TabForm;
                else if (formname == "InfoViewForm")
                    form = InfoForm;
                else if (formname == "ServerViewForm")
                    form = ServerForm;
            }
            return form;
        }

        public List<string> GetResourceNames()
        {
            List<string> names = new List<string>();
            foreach(KeyValuePair<string, Platform.Core.UI.BaseForm> pair in ToolFormDictionary)
                names.Add(pair.Key);
            return names;
        }

        public FormLoc GetLocation(string formname)
        {
            FormLoc location = new FormLoc(DockState.Unknown);
            foreach (KeyValuePair<string, FormLoc> pair in FormLocationDictionary)
            {
                if (pair.Key == formname)
                    location = pair.Value;
            }
            //if (location == null)
            //{
            //    if (formname == "TreeViewForm")
            //        dockstate = DockState.DockLeft;//树状列表至于左侧   
            //    else if (formname == "TabViewForm")
            //        dockstate = DockState.Document;//功能面板置于右侧
            //    else if (formname == "InfoViewForm")
            //        dockstate = DockState.DockBottom;//输出信息面板置于地侧
            //    else if (formname == "ServerViewForm")
            //        dockstate = DockState.DockLeftAutoHide;//服务器面板自动隐藏
            //}
            return location;
        }

        private bool Disposed = false;
        /// <summary>
        /// 在窗体销毁之前会调用此函数来完成工程数据的收集,可以重载
        /// </summary>
        public virtual void CollectInf(string path)
        {
            //在此可以关闭发起的连接等
            //关闭前检测工程是否已保存
            

            //获取当前工程
            AbstractProject project = ProjectManager.ProjectManagerSington.GetCurrentProject();
            //当前工程窗体类型列表
            List<FormInfo>uiinflist=new List<FormInfo>();
            foreach (KeyValuePair<string, BaseForm> form in ToolFormDictionary)
                uiinflist.Add(new FormInfo(form.Key, form.Value.Text, form.Value.GetType()));

            //将类型列表保存至配置文件
            BinaryFormatter bf = new BinaryFormatter();
            string filename;
            if(project.Path.EndsWith("\\"))
                filename = project.Path + project.Name + "\\"+project.Name+".uiinflist";
            else
                filename = project.Path + "\\"+project.Name + "\\" + project.Name + ".uiinflist";
            if (path.Equals(string.Empty))
                path = filename;
            else
                path = path + project.Name + ".uiinflist";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                bf.Serialize(fs, uiinflist);
                fs.Close();
            }
            ServicesManager.ServicesManagerSingleton.UIService.DisposingUI(null,null);
            //MessageBox.Show("界面布局已经保存！！！！！！！！");
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
                CollectInf(string.Empty);

                //依次销毁窗体
                if (this.ViewForm != null)
                {
                    this.ViewForm.Disposed += new EventHandler(ViewForm_Disposed);
                    this.ServerForm.Dispose();
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
                if (this.ServerForm != null)
                {
                    this.ServerForm.Disposed += new EventHandler(ServerForm_Disposed);
                    this.TabForm.Dispose();
                }
                //销毁插件自定义窗体
                foreach (KeyValuePair<string, Platform.Core.UI.BaseForm> pair in ToolFormDictionary)
                {
                    pair.Value.Dispose();
                    string key = pair.Key;
                }
                ToolFormDictionary.Clear();
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

        /// <summary>
        /// 销毁Server窗体资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void ServerForm_Disposed(object sender, EventArgs args)
        {
            ServerForm = null;
        }
    }
}
