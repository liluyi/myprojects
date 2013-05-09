using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace WindowsFormsApplication2
{
    [Serializable]
    public class TreeInfoCollection:System.Collections.CollectionBase,ISerializable
    {
        public void Add(TreeNodeInfo tni)
        {
            List.Add(tni);
        }

        public TreeInfoCollection()
        {

        }

        public TreeInfoCollection(SerializationInfo info, StreamingContext context)
        {
            for (int i = 0; i < info.MemberCount; i++)
                List.Add((TreeNodeInfo)info.GetValue("TreeNodeInfo " + i.ToString(), typeof(TreeNodeInfo)));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            for (int i = 0; i < Count; i++)
                info.AddValue("TreeNodeInfo " + i.ToString(), List[i]);
        }
    }
}

