using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

using Platform.Core.UI;
using Platform.Core.Services;
using Platform.Core.Data;
using Platform.Core.Exceptions;

namespace Platform.Core
{
    
    /// <summary>
    /// 引擎的状态
    /// </summary>
    internal enum EngineState
    {
        UnInit,  //未初始化
        Active,  //激活状态
        Working, //正常工作状态
        Stop     //停止工作
    }

    /// <summary>
    /// 运行时引擎，负责处理与WinForm的交互
    /// </summary>
    internal sealed class Engine
    {
        /// <summary>
        /// engine寄宿的主窗体
        /// </summary>
        private Form MainForm;

        /// <summary>
        /// 引擎启动时的设置
        /// </summary>
        public StartUpSettings StartupSettings;

        /// <summary>
        /// 服务维护对象
        /// </summary>
        private ServicesManager servicesmanager;

        /// <summary>
        /// 插件维护对象
        /// </summary>
        private PluginsManager pluginmanager;

        /// <summary>
        /// 工程维护对象
        /// </summary>
        private ProjectManager projectmanager;

        /// <summary>
        /// 引擎的状态
        /// </summary>
        public EngineState state = EngineState.UnInit;

        /// <summary>
        /// 状态属性
        /// </summary>
        private EngineState State
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form">寄宿的窗体</param>
        /// <param name="sus">启动设置</param>
        public Engine(Form form,StartUpSettings sus)
        {
            this.MainForm = form;
            this.StartupSettings = sus;

            //状态设为被激活状态
            this.state = EngineState.Active;
        }

        /// <summary>
        /// 加载root路径下所有的插件（包括Main插件）
        /// </summary>
        /// <param name="root"></param>
        private void CoreCreatePlugins(string root)
        {
            //获取所有插件
            List<string> AllAddins = servicesmanager.FileService.CollectPluginsAddin(root);

            string mainAddin=string.Empty;

            //先加载root下的Main插件，但并不将插件插入至插件树中
            foreach (string s in AllAddins)
            {
                if (s.ToLower().EndsWith("main.addin"))
                {
                    mainAddin = s;
                    break;
                }
            }

            if (mainAddin == string.Empty) //主插件不存在,则抛出异常
            {
                throw new PluginNotExsitException();
            }
            
            try
            {
                CreatePlugin(mainAddin, false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("加载main插件失败\n" + ex.StackTrace);
                SystemLogging.SystemLoggingSingleton.Debug("加载Main插件失败");
                return;
            }
            SystemLogging.SystemLoggingSingleton.Debug("加载Main插件成功");
            //加载其余的插件
            foreach (string s in AllAddins)
            {
                if (s != mainAddin)
                {
                    try
                    {
                        CreatePlugin(s, true);
                        SystemLogging.SystemLoggingSingleton.Debug("加载" + s + "插件成功");
                    }
                    catch (AddinFileNotInvaildException ex)
                    {
                        SystemLogging.SystemLoggingSingleton.Error("AddinFile error:" + s);
                    }
                    catch (AssemblyRefusedException ex)
                    {
                        SystemLogging.SystemLoggingSingleton.Error("Load Assembly error:" + s);
                    }
                    catch (PluginBuildErrorException ex)
                    {
                        SystemLogging.SystemLoggingSingleton.Error("Build Plugin error:(检查plugin构造函数)" + s);
                    }
                    catch (CoreException ex)
                    {
                        SystemLogging.SystemLoggingSingleton.Error("出现未知错误");
                    }
                    catch (Exception ex)
                    {
                        SystemLogging.SystemLoggingSingleton.Debug("加载" + s.Substring(0, s.LastIndexOf('.')) + "插件失败");
                    }
                    
                }
            }
        }

        /// <summary>
        /// 构建插件，并加载UI(仅在Core启动阶段加载用)
        /// </summary>
        /// <param name="pluginpath">插件的Addin文件路径</param>
        /// <param name="insert">是否将Plugin插入插件树中</param>
        private void CreatePlugin(string pluginpath, bool insert)
        {

            CreatePluginArgs args = new CreatePluginArgs(pluginpath);


            //生成插件，如果有自定义服务，则此函数一并负责添加自定义服务
            IPlugin plugin = servicesmanager.PluginsService.CreatePlugin(args);

            bool suc = true;
            if (insert) //先将插件放入插件集合中，防止放入失败时仍然加载并显示UI插件
            {
                suc = servicesmanager.PluginsService.InsertPlugin(new InsertPluginArgs(plugin, args.Info));
            }
            if (suc)
            {
                servicesmanager.UIService.LoadUI_CreatePlugin(plugin, null);
            }
        }

        /// <summary>
        /// 运行平台，在Engine开始工作时需首先加载Core
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void ExcutePlatform(object sender, RuntimeEventArgs args)
        {
            
            //底层core加载一次即可
            if (this.state == EngineState.Working)
            {
                return;
            }
            SystemLogging.SystemLoggingSingleton.Info("Engine状态转至Working,启动平台...");

            SystemLogging.SystemLoggingSingleton.Info("Engine生成Core配置");
            CoreStartUp csu = new CoreStartUp(StartupSettings);
            try
            {
               
                //服务优先获取，因为pluginmanager及projectmanager
                //有可能会调度这些服务，如果服务对象获取正确的话，各服务的状态会被设置成Load状态
                csu.InitServicesManager(out servicesmanager,MainForm);
                SystemLogging.SystemLoggingSingleton.Info("Engine初始化服务管理，加载自启动服务成功...");
                
                //初始化插件维护者
                csu.InitPluginsManager(out pluginmanager);
                SystemLogging.SystemLoggingSingleton.Info("Engine初始化插件管理，成功");
                
                //初始化工程维护者
                csu.InitProjectManager(out projectmanager);
                SystemLogging.SystemLoggingSingleton.Info("Engine初始化工程管理，成功");
                //core加载正常，Engine状态转为工作状态
                this.state = EngineState.Working;

                //插件根目录
                string pluginroot= Application.StartupPath +"\\" + servicesmanager.PluginsService.PluginsRoot;

                SystemLogging.SystemLoggingSingleton.Info("Engine加载插件集合...");
                //加载Core插件
                CoreCreatePlugins(pluginroot);
            }
            catch(Exception ex)
            {
                //初始化失败的话则引擎停止工作
                this.state = EngineState.Stop;

                Debug.WriteLine("Core 加载失败"+ ex.StackTrace);
            }            
        }

        /// <summary>
        /// 与运行时通讯，该方法完成打开某工程文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OpenProject(object sender, RuntimeEventArgs args)
        {
            //引擎已启动失败，退出
            if (this.state == EngineState.Stop)
            {
                return;
            }
            else if (this.state != EngineState.Working)//如果Core没启动，则先启动Core
            {
                ExcutePlatform(sender,args);
            }

            AbstractProject project = null;
            IPlugin plugin = null;

            try
            {
                project = servicesmanager.ProjectService.OpenProject(args.projectpath);
            }
            catch(Exception ex)
            {
                SystemLogging.SystemLoggingSingleton.Error("打开工程文件:" + args.projectpath + "失败");
                return;
            }
            
            try
            {
                plugin = pluginmanager.GetPlugin(project.PluginUUID);
                SystemLogging.SystemLoggingSingleton.Error("解析工程对应的插件，成功");
                projectmanager.InsertProject(project);
                SystemLogging.SystemLoggingSingleton.Error("工程放入工程管理库，成功");
                servicesmanager.UIService.LoadUI_CreateProject(plugin,new UIEventArgs(plugin.MutableResourceClassFullName,project.UUID));
                SystemLogging.SystemLoggingSingleton.Error("加载工作区试图，成功");
            }
            catch(Exception ex)
            {
                SystemLogging.SystemLoggingSingleton.Error("创建工程失败");
            }
        }
    }
    
}