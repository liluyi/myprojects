using System;
using System.Collections;
using System.Collections.Generic;

namespace Platform.Core
{
    internal class PluginTree
    {
        private static PluginTree defaultPluginTree;

        private PluginTreeNode rootNode;

        public static PluginTree PluginTreeSingleton
        {
            get
            {
               
                if (defaultPluginTree == null)
                {
                    defaultPluginTree = new PluginTree();
                }
                return defaultPluginTree;
            }
        }

        private PluginTree()
        {
            rootNode = new PluginTreeNode();
        }

        
        public  PluginTreeNode GetTreeNode(string path, bool throwOnNotFound)
        {
            if (path == null || path.Length == 0)
            {
                return rootNode;
            }
            string[] splittedPath = path.Split('/');
            PluginTreeNode curPath = rootNode;
            int i = 0;
            while (i < splittedPath.Length)
            {
                if (!curPath.ChildNodes.TryGetValue(splittedPath[i], out curPath))
                {
                    if (throwOnNotFound)
                        throw new Exception(path);
                    else
                        return null;
                }
                i++;
            }
            return curPath;
        }

        public PluginTreeNode GetTreeNode(string path)
        {
            return GetTreeNode(path, true);
        }

        public bool ExistsTreeNode(string path)
        {
            if (path == null || path.Length == 0)
            {
                return true;
            }

            string[] splittedPath = path.Split('/');
            PluginTreeNode curPath = rootNode;
            int i = 0;
            while (i < splittedPath.Length)
            {
                if (!curPath.ChildNodes.TryGetValue(splittedPath[i], out curPath))
                {
                    return false;
                }
                ++i;
            }
            return true;
        }

        public PluginTreeNode GetRootNode()
        {
            return rootNode;
        }
    }
}
