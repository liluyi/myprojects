using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace XMLToDB
{
    struct Infortable
    {
        public string nodeID;
        public string nodedataID;
        public string nodename;
        public string description;
        public string NodeType;
        public string parentID;
        public string siblingID;
        public string FMAInfo;
        public int ID;
    }
    struct Relation
    {
        public string nodeID;
        public string ChildID;
    }
    class DealXML
    {
        // string sql = "insert into table values('value1','value2')";
        private string InsertInfotable;
        private string InsertRelation;
        private Infortable infotable;
        private Relation relation;
        private XmlDocument doc;
        private XmlDocument xmldoc;
        private Database database;
        private string pluginUUID;
        private string solution;
        private string name;
        private  void initInfo()
        {
            infotable.nodeID = "";
            infotable.nodedataID = "";
            infotable.nodename = "";
            infotable.description = "";
            infotable.NodeType = "";
            infotable.parentID = "";
            infotable.siblingID = "";
            infotable.FMAInfo = "";
        }
        private void initRela() 
        {
            relation.nodeID = "";
            relation.ChildID = "";
        }

        //无参构造函数
        public DealXML() 
        {
            xmldoc = new XmlDocument();
            database = new Database();
            database.ConnectionDatabase("casere_sfta_database");
        }
        //构造函数,path:xml路径名
        public DealXML(string path)
        {
            database = new Database();
            database.ConnectionDatabase("casere_sfta_database");
            doc = new XmlDocument();
            doc.Load(path);
        }
        //将xml文件存入数据库
        public void XmlToDatabase()
        {
            XmlNodeList root = doc.DocumentElement.ChildNodes;
            foreach (XmlElement node in root)
            {
                string nodename = node.Name;
                if (nodename.Equals("SFTATreeNodes"))
                {
                    this.DealTreeNodes(node);
                }
                else if (nodename.Equals("SFTARelations"))
                {
                    this.DealRelations(node);
                }
                else if (nodename.Equals("PluginUUID"))
                {
                    pluginUUID = node.InnerText;
                }
                else if (nodename.Equals("Solution"))
                {
                    solution = node.InnerText;
                }
                else if (nodename.Equals("Name"))
                {
                    name = node.InnerText;
                }
            }
            database.CloseDatabase();
        }
        //根据数据库生成xml文件,path:xml路径名
        public void DatabaseToXml(string path)
        {
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0",null,null));
            XmlNode rootNode = xmldoc.CreateElement("SFTAProjectData", "xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlNode sftatreenodes = xmldoc.CreateElement("SFTATreeNodes");
            //插入节点
            string sql1 = "select * from SFTATreeInfo";
            SqlDataReader reader=database.GetSftaInfo(sql1);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    XmlNode sftatreenode=xmldoc.CreateElement("SFTATreeInfoTable");

                    string nodeID = reader.GetString(0);
                    XmlNode nodeIDxml = xmldoc.CreateElement("nodeid");
                    nodeIDxml.InnerText = nodeID;
                    sftatreenode.AppendChild(nodeIDxml);

                    string nodedataID = reader.GetString(1);
                    XmlNode nodedataIDxml = xmldoc.CreateElement("nodedataid");
                    nodedataIDxml.InnerText = nodedataID;
                    sftatreenode.AppendChild(nodedataIDxml);

                    string nodename = reader.GetString(2);
                    XmlNode nodenamexml = xmldoc.CreateElement("nodename");
                    nodenamexml.InnerText = nodename;
                    sftatreenode.AppendChild(nodenamexml);

                    string description = reader.GetString(3);
                    XmlNode descriptionxml = xmldoc.CreateElement("description");
                    descriptionxml.InnerText = description;
                    sftatreenode.AppendChild(descriptionxml);

                    string nodetype = reader.GetString(4);
                    XmlNode nodetypexml = xmldoc.CreateElement("nodetype");
                    nodetypexml.InnerText = nodetype;
                    sftatreenode.AppendChild(nodetypexml);

                    string parentid = reader.GetString(5);
                    XmlNode parentidxml = xmldoc.CreateElement("parentid");
                    parentidxml.InnerText = parentid;
                    sftatreenode.AppendChild(parentidxml);

                    string siblingid = reader.GetString(6);
                    XmlNode siblingidxml = xmldoc.CreateElement("siblingid");
                    siblingidxml.InnerText = siblingid;
                    sftatreenode.AppendChild(siblingidxml);

                    string fmeainfo = reader.GetString(7);
                    XmlNode fmeainfoxml = xmldoc.CreateElement("fmeainfo");
                    fmeainfoxml.InnerText = fmeainfo;
                    sftatreenode.AppendChild(fmeainfoxml);

                    sftatreenodes.AppendChild(sftatreenode);
                }
            }
            rootNode.AppendChild(sftatreenodes);
            reader.Close();

            XmlNode sftarelanodes = xmldoc.CreateElement("SFTARelations"); 
            // 插入节点
            string sql2 = "select * from SFTANodeRelation";
            SqlDataReader reader2 = database.GetSftaInfo(sql2);
            if (reader2.HasRows == true)
            {
                while (reader2.Read())
                {
                    XmlNode sftarelanode = xmldoc.CreateElement("SFTANodeRelationTable");

                    string nodeID = reader2.GetString(1);
                    XmlNode nodeIDxml = xmldoc.CreateElement("nodeid");
                    nodeIDxml.InnerText = nodeID;
                    sftarelanode.AppendChild(nodeIDxml);

                    string childID = reader2.GetString(2);
                    XmlNode childIDxml = xmldoc.CreateElement("childid");
                    childIDxml.InnerText = childID;
                    sftarelanode.AppendChild(childIDxml);

                    sftarelanodes.AppendChild(sftarelanode);
                }
            }

            rootNode.AppendChild(sftarelanodes);
            reader2.Close();

           // xmldoc.Save(path);
            xmldoc.AppendChild(rootNode);
            xmldoc.Save(path);
            Console.WriteLine("xml文件生成");
        }

        private void DealTreeNodes(XmlElement parentnode)
        {
            XmlNodeList infotablenodes = parentnode.ChildNodes;
            foreach (XmlElement infotablenodelists in infotablenodes)
            {
                this.initInfo();
                InsertInfotable = "insert into SFTATreeInfo values('value1','value2','value3','value4','value5','value6','value7','value8')";
                XmlNodeList infotablenodelist = infotablenodelists.ChildNodes;
                foreach (XmlElement infotablenode in infotablenodelist)
                {
                    string name = infotablenode.Name;
                    if (name.Equals("nodeid"))
                    {
                        infotable.nodeID = infotablenode.InnerText;
                    }
                    else if (name.Equals("nodedataid"))
                    {
                        infotable.nodedataID = infotablenode.InnerText;
                    }
                    else if (name.Equals("nodename"))
                    {
                        infotable.nodename = infotablenode.InnerText;
                    }
                    else if (name.Equals("description"))
                    {
                        infotable.description = infotablenode.InnerText;
                    }
                    else if (name.Equals("nodetype"))
                    {
                        infotable.NodeType = infotablenode.InnerText;   
                    }
                    else if (name.Equals("parentid"))
                    {
                        infotable.parentID = infotablenode.InnerText;
                    }
                    else if (name.Equals("siblingid"))
                    {
                        infotable.siblingID = infotablenode.InnerText;
                    }
                    else if (name.Equals("fmeainfo"))
                    {
                        infotable.FMAInfo = infotablenode.InnerText;
                    }
                }
                //Console.WriteLine(infotablenodelists.Name + "\n" + infotablenodelists.InnerText + "\n\n");
               this.StoreIntoInfotable();
            }
        }
        private void DealRelations(XmlElement parentnode)
        {
            XmlNodeList infotablenodes = parentnode.ChildNodes;
            foreach (XmlElement infotablenodelists in infotablenodes)
            {
                this.initRela();
                InsertRelation = "insert into SFTANodeRelation(nodeID,childID) values('value1','value2')";
                XmlNodeList infotablenodelist = infotablenodelists.ChildNodes;
                foreach (XmlElement infotablenode in infotablenodelist)
                {
                    string name = infotablenode.Name;
                    if (name.Equals("nodeid"))
                    {
                        relation.nodeID = infotablenode.InnerText;
                    }
                    else if (name.Equals("childid"))
                    {
                        relation.ChildID = infotablenode.InnerText;
                    }
                }
                //Console.WriteLine(infotablenodelists.Name + "\n" + infotablenodelists.InnerText + "\n\n");
                this.StoreIntoRelatable();
            }
        }
        private void StoreIntoInfotable()
        {
            InsertInfotable=InsertInfotable.Replace("value1", infotable.nodeID);
            InsertInfotable=InsertInfotable.Replace("value2", infotable.nodedataID);
            InsertInfotable=InsertInfotable.Replace("value3", infotable.nodename);
            InsertInfotable=InsertInfotable.Replace("value4", infotable.description);
            InsertInfotable=InsertInfotable.Replace("value5", infotable.NodeType);
            InsertInfotable=InsertInfotable.Replace("value6", infotable.parentID);
            InsertInfotable=InsertInfotable.Replace("value7", infotable.siblingID);
            InsertInfotable=InsertInfotable.Replace("value8", infotable.FMAInfo);
            database.StoreData(InsertInfotable);
        }
        private void StoreIntoRelatable() 
        {
            InsertRelation = InsertRelation.Replace("value1", relation.nodeID);
            InsertRelation = InsertRelation.Replace("value2", relation.ChildID);
            string sql = "select ID from SFTANodeRelation where nodeID=" +"\'" +relation.nodeID+"\'"+" and "+"childID="+"\'"+relation.ChildID+"\'";


            Int32 getResult = database.Search(sql);
          
            if(getResult>0)
            {
                Console.WriteLine("该记录已经存在,插值失败");
            }
            else if (getResult == -1)
            {
                Console.WriteLine("异常，插值失败");
            }
           
            else if (getResult==-2)
            {
                database.StoreData(InsertRelation);
            }
            
        }
        private XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument docd = node.OwnerDocument;
                XmlAttribute attr = null;
                attr = docd.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}