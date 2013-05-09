using System;
using System.Xml;
using System.Configuration;

using Platform.Core.Exceptions;
namespace Platform.Core
{
    /// <summary>
    /// 配置分析类，辅导导入配置信息
    /// </summary>
    internal class PropertyAnalyse 
    {
        /// <summary>
        /// 从配置文件导入相应的配置信息
        /// </summary>
        /// <param name="xmlFile">平台xml配置文件</param>
        /// <param name="xmlNodeName">节点名字，按Xpath的路径方式寻找</param>
        /// <returns></returns>
        public static Properties GetProperties(string xmlFile,string xmlNodeName)
        {
            Properties properties = new Properties();

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(xmlFile);
            
                XmlNode node = xmlDoc.SelectSingleNode(xmlNodeName);

                foreach (XmlAttribute attribute in node.Attributes)
                {
                    properties.Set(attribute.LocalName, attribute.Value);
                }

            }
            catch(Exception ex)
            {
                string msg = "加载配置文件:" + xmlFile + "出错";

                throw new CoreException("PropertyAnalyse:",msg,ex, ExceptionLevel.High);
            }

            return properties; 
        }
    }

}