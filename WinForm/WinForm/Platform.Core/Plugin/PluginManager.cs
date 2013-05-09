using System;
using System.Collections;
using System.Collections.Generic;
using Platform.Core.Exceptions;
using Platform.Core.UI;
using Platform.Core.Services;
namespace Platform.Core
{
    public class PluginsManager
    {
        /// <summary>
        /// 插件根目录，默认为可执行文件路径下的plugin目录
        /// </summary>
        private string pluginroot = string.Empty;

        /// <summary>
        /// 插件词典
        /// </summary>
        private Dictionary<string, IPlugin> plugindictionary=new Dictionary<string,IPlugin>();

        /// <summary>
        /// 插件信息词典
        /// </summary>
        private Dictionary<string, PluginInfo> plugininfodictionary = new Dictionary<string, PluginInfo>();

        /// <summary>
        /// 浮动资源列表，维护当前插件工具的全部工作窗体
        /// </summary>
        private List<MutableResource> mutableResources = new List<MutableResource>();

        #region 单例模式

        /// <summary>
        /// 私有构造函数，供内部静态变量defaultpluginsmanager初始化用
        /// </summary>
        private PluginsManager()
        {
            
        }

        /// <summary>
        /// 静态变量，所有PluginManager均是此对象（单例模式）
        /// </summary>
        private static PluginsManager defaultpluginsmanager = new PluginsManager();

        /// <summary>
        /// 静态属性，获取PluginManager对象
        /// </summary>
        public static PluginsManager PluginsManagerSington
        {
            get
            {
                return defaultpluginsmanager;
            }
        }

        #endregion

        /// <summary>
        /// 根据token从插件词典中获取插件//为防止其他plugin修改此plugin的行为，方法设为internal
        /// </summary>
        /// <param name="token">token标识</param>
        /// <returns>token返回的插件实例</returns>
        public IPlugin GetPlugin(string token)
        {
            IPlugin plugin;
            plugindictionary.TryGetValue(token, out plugin);
            return plugin;
        }

        /// <summary>
        /// 获取插件词典中所有的插件，不包含Main插件
        /// </summary>
        /// <returns></returns>
        internal List<IPlugin> GetAllPlugins()
        {
            List<IPlugin> plugins = new List<IPlugin>();

            foreach (KeyValuePair<string, IPlugin> plugin in plugindictionary)
            {
                if (plugin.Key != "Main")
                {
                    plugins.Add(plugin.Value as IPlugin);
                }
            }
            return plugins;
        }

        /// <summary>
        /// 根据token获取插件信息，其它插件可以通过方法获取其它插件信息（当然在知道Token的前提下）
        /// </summary>
        /// <param name="token">插件token标识</param>
        /// <returns>插件信息</returns>
        public PluginInfo GetPluginInfo(string token)
        {
            PluginInfo plugininfo;
            plugininfodictionary.TryGetValue(token, out plugininfo);
            return plugininfo;
        }

        /// <summary>
        /// 获取所有插件信息（不包含main插件）
        /// </summary>
        /// <returns></returns>
        public List<PluginInfo> GetAllPluginInfo()
        {
            List<PluginInfo> plugininfos = new List<PluginInfo>();

            foreach (KeyValuePair<string, PluginInfo> info in plugininfodictionary)
            {
                if (info.Key != "Main")
                {
                    plugininfos.Add(info.Value as PluginInfo);
                }
            }
            return plugininfos;
        }

        /// <summary>
        /// 利用后缀名获取插件
        /// </summary>
        /// <param name="suffix">文件后缀</param>
        /// <returns>插件</returns>
        internal IPlugin GetPluginFromSuffix(string suffix)
        {
            foreach (KeyValuePair<string, IPlugin> pair in plugindictionary)
            {
                if (pair.Value.ProjectSuffix == suffix)
                {
                    return pair.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// 将插件及插件信息放入词典（树）方便进行管理
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="info">插件信息</param>
        /// <returns>Insert是否成功</returns>
        internal bool InsertPlugin(IPlugin plugin,PluginInfo info)
        {
            if(plugin==null)
            {
                throw new PluginNotExsitException();
            }
            if(info==null)
            {
                throw new PluginInfoNotExsitException();
            }
            if (plugindictionary.ContainsKey(plugin.Token))
            {
                return false;
            }
            else
            {
                //放入插件词典，token为标识
                plugindictionary.Add(plugin.Token, plugin);

                //放入插件信息词典，token为标识
                plugininfodictionary.Add(plugin.Token, info);

                return true;
            }
        }

        /// <summary>
        /// 插入浮动资源
        /// </summary>
        /// <param name="res">浮动资源实例</param>
        internal void insertMutableResource(MutableResource res)
        {
            if (res != null)
            {
                mutableResources.Add(res);
            }
        }

        /// <summary>
        /// 删除浮动资源，销毁工作窗体
        /// </summary>
        /// <param name="projectUUID">工程ID</param>
        public void RemoveMutableResource(string projectUUID)
        {
            foreach (MutableResource r in mutableResources)
            {
                if (r.UUID == projectUUID)
                {
                    mutableResources.Remove(r);
                    r.DisposeSelf();
                    break;
                }
            }
        }

        public void SaveMutableResource(string projectUUID)
        {
            foreach (MutableResource r in mutableResources)
            {
                if (r.UUID == projectUUID)
                {
                    r.CollectInf(string.Empty);
                    break;
                }
            }
        }
        public void SaveMutableResource(string projectUUID,string path)
        {
            foreach (MutableResource r in mutableResources)
            {
                if (r.UUID == projectUUID)
                {
                    r.CollectInf(path);
                    break;
                }
            }
        }

        /// <summary>
        /// 向现有浮动资源中插入新窗体
        /// </summary>
        /// <param name="form">窗体资源</param>
        /// <returns>无</returns>
        public void AddFormInMutableResource(string formname,BaseForm form)
        {
            foreach (MutableResource r in mutableResources)
            {
                    r.AddResource(formname,form);
                    break;
            }
        }

        public void AddLocationInMutableResource(string formname, FormLoc location)
        {
            foreach (MutableResource r in mutableResources)
            {
                r.AddLocation(formname, location);
                break;
            }
        }

        public void RemoveFormInMutableResource(string formname)
        {
            foreach (MutableResource r in mutableResources)
            {
                r.DeleteResource(formname);
            }
        }

        /// <summary>
        /// 根据窗体名称，从工程的浮动资源中获取窗体
        /// </summary>
        /// <param name="formname"></param>
        /// <returns></returns>
        public BaseForm GetFormFromMutableResource(string formname)
        {
            BaseForm form = null;
            foreach (MutableResource r in mutableResources)
            {
                form=r.GetResource(formname);
                break;
            }
            return form;       
        }

        public List<string> GetAllFormNames()
        {
            List<string> names=new List<string>();
            foreach (MutableResource r in mutableResources)
            {
                names=r.GetResourceNames();
                break;
            }
            return names;
        }


        public FormLoc GetLocationFromMutableResource(string formname)
        {
            FormLoc location = null;
            foreach (MutableResource r in mutableResources)
            {
                location = r.GetLocation(formname);
                break;
            }
            return location;
        }

        public MutableResource GetMutableResource()
        {
            MutableResource mutableresource=null;
            foreach (MutableResource r in mutableResources)
            {
                mutableresource = r;
                break;
            }
            return mutableresource;
        }

        #region 没必要
        /// <summary>
        /// 获取当前ViewForm面板
        /// </summary>
        /// <returns>ViewForm</returns>
        public Platform.Core.UI.BaseForm GetViewForm()
        {
            Platform.Core.UI.BaseForm form = new BaseForm();
            foreach (MutableResource r in mutableResources)
            {
                form = r.ViewForm;
            }
            return form;
        }

        /// <summary>
        /// 获取当前InfoForm面板
        /// </summary>
        /// <returns>InfoForm</returns>
        public Platform.Core.UI.BaseForm GetInfoForm()
        {
            Platform.Core.UI.BaseForm form = new BaseForm();
            foreach (MutableResource r in mutableResources)
            {
                form = r.InfoForm;
            }
            return form;
        }
        /// <summary>
        /// 获取当前TabForm面板
        /// </summary>
        /// <returns>TabForm</returns>
        public Platform.Core.UI.BaseForm GetTabForm()
        {
            Platform.Core.UI.BaseForm form = new BaseForm();
            foreach (MutableResource r in mutableResources)
            {
                form = r.TabForm;
            }
            return form;
        }
        /// <summary>
        /// 获取当前ServerForm面板
        /// </summary>
        /// <returns>ServerForm</returns>
        public Platform.Core.UI.BaseForm GetServerForm()
        {
            Platform.Core.UI.BaseForm form = new BaseForm();
            foreach (MutableResource r in mutableResources)
            {
                form = r.ServerForm;
            }
            return form;
        }
    }
        #endregion
}