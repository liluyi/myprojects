using System;
namespace Platform.Core
{
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public delegate void PropertyLoadEventHandler(object sender,PropertyLoadEventArgs e);

    public delegate void PropertySaveEventHandler(object sender,PropertySaveEventArgs e);

    public class PropertyChangedEventArgs : EventArgs
    {
        Properties properties;
        string key;
        object newValue;
        object oldValue;

        /// <returns>
        /// returns the changed property object
        /// </returns>
        public Properties Properties
        {
            get
            {
                return properties;
            }
        }

        /// <returns>
        /// The key of the changed property
        /// </returns>
        public string Key
        {
            get
            {
                return key;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object NewValue
        {
            get
            {
                return newValue;
            }
        }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object OldValue
        {
            get
            {
                return oldValue;
            }
        }

        public PropertyChangedEventArgs(Properties properties, string key, object oldValue, object newValue)
        {
            this.properties = properties;
            this.key = key;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }

    public class PropertyLoadEventArgs : EventArgs
    {
        Properties properties;
        string xmlFilePath;

        public PropertyLoadEventArgs(string xmlFilePath, Properties properties)
        {
            this.properties = properties;
            this.xmlFilePath = xmlFilePath;
        }
    }

    public class PropertySaveEventArgs : EventArgs
    {
        Properties properties;
        string xmlFilePath;

        public PropertySaveEventArgs(string xmlFilePath, Properties properties)
        {
            this.properties = properties;
            this.xmlFilePath = xmlFilePath;
        }
    }
}
