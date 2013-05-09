using System;
using System.Collections;
using System.Collections.Generic;

using Platform.Core.UI;
namespace Platform.Core
{
    internal sealed class PluginTreeNode
    {
        private Dictionary<string, PluginTreeNode> childnodes = new Dictionary<string, PluginTreeNode>();

        public Dictionary<string, PluginTreeNode> ChildNodes
        {
            get
            {
                return childnodes;
            }
        }

        private bool isCond=false;

        private IPlugin plugin=null;

        public bool IsCond
        {
            get
            {
                return isCond;
            }
            set
            {
                isCond = value;
            }
        }
        public IPlugin Plugin
        {
            get
            {
                return plugin;
            }
            set
            {
                plugin = value;
            }
        }
        public PluginTreeNode this[string nodename]
        {
            get
            {
                PluginTreeNode o;
                childnodes.TryGetValue(nodename, out o);
                return o;
            }
        }
    }
}