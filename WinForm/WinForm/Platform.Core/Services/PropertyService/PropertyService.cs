using System;
using System.Xml;
using System.Configuration;

namespace Platform.Core.Services
{
    internal class PropertyService //: IService
    {

        #region IService Members

        public string Description
        {
            get 
            {
                return "PropertyService";
            }
        }

        public string Name
        {
            get 
            {
                return "PropertyService";
            }
        }

        public ServiceLevel Level
        {
            get 
            { 
                return ServiceLevel.Common; 
            }
        }

        public ServiceState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        #endregion

        private ServiceState state;

        public static Properties GetProperties(string platformXmlFile,string xmlNodeName)
        {
            Properties properties = new Properties();

            XmlDocument xmlDoc=new XmlDocument();

            xmlDoc.Load(platformXmlFile);

            
            XmlNode node = xmlDoc.SelectSingleNode(xmlNodeName);

            foreach (XmlAttribute attribute in node.Attributes)
            {
                properties.Set(attribute.LocalName, attribute.Value);
            }
            return properties;
        }

    }

}