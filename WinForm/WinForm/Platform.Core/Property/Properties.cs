
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;


namespace Platform.Core
{
    /// <summary>
    /// 配置文件接口
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// 在内部维护一个以string为key的字典
        /// </summary>
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        public object this[string property]
        {
            get
            {
                return Get(property);
            }
            set
            {
                Set(property, value);
            }
        }

        public string[] Elements
        {
            get
            {
                lock (properties)
                {
                    List<string> elements = new List<string>();
                    foreach (KeyValuePair<string, object> property in properties)
                    {
                        elements.Add(property.Key);
                    }
                    return elements.ToArray();
                }
            }
        }

        public object Get(string property)
        {
            lock (properties)
            {
                object val;
                properties.TryGetValue(property, out val);
                return val;
            }
        }

        public void Set(string property, object value)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            lock (properties)
            {
                if (!properties.ContainsKey(property))
                {
                    properties.Add(property, value);
                }
                else
                {
                    if (value != properties[property])
                    {
                        properties[property] = value;
                    }
                }
            }

        }

        public bool Contains(string property)
        {
            lock (properties)
            {
                return properties.ContainsKey(property);
            }
        }

        public int Count
        {
            get
            {
                lock (properties)
                {
                    return properties.Count;
                }
            }
        }

        public bool Remove(string property)
        {
            lock (properties)
            {
                return properties.Remove(property);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Properties:{");
            foreach (KeyValuePair<string, object> entry in properties)
            {
                sb.Append(entry.Key);
                sb.Append("=");
                sb.Append(entry.Value);
                sb.Append(",");
            }
            sb.Append("}]");
            return sb.ToString();
        }
    }
}
