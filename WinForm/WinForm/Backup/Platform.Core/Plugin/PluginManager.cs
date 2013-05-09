using System;
using System.Collections;
using System.Collections.Generic;

using Platform.Core.Exceptions;
namespace Platform.Core
{
    public class PluginsManager
    {
        /// <summary>
        /// 插件根目录，默认为可执行文件路径下的plugin目录
        /// </summary>
        private string pluginroot = string.Empty;

        ///// <summary>
        ///// 插件树（后期考虑扩展）
        ///// </summary>
        //private PluginTree plugintree;

        /// <summary>
        /// 插件词典
        /// </summary>
        private Dictionary<string, IPlugin> plugindictionary=new Dictionary<string,IPlugin>();

        /// <summary>
        /// 插件信息词典
        /// </summary>
        private Dictionary<string, PluginInfo> plugininfodictionary = new Dictionary<string, PluginInfo>();

        private List<MutableResource> mutableResources = new List<MutableResource>();
        #region 单例模式

        /// <summary>
        /// 私有构造函数，供内部静态变量defaultpluginsmanager初始化用
        /// </summary>
        private PluginsManager()
        {
            //plugintree = PluginTree.PluginTreeSingleton;
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
        /// 根据token从插件词典中获取插件,为防止其他plugin修改此plugin的行为，方法设为internal
        /// </summary>
        /// <param name="token">token标识</param>
        /// <returns>token返回的插件实例</returns>
        internal IPlugin GetPlugin(string token)
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

        internal void insertMutableResource(MutableResource res)
        {
            if (res != null)
            {
                mutableResources.Add(res);
            }
        }

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

    }
}