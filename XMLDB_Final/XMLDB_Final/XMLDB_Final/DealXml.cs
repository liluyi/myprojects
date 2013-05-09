using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Data;


namespace XMLDB_Final
{
    class DealXml
    {
        #region 私有变量
        private XmlDocument doc=new XmlDocument();
        private DealConfigure dc = new DealConfigure();
        private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> Dic = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
        private Dictionary<string, string> DBUUID = new Dictionary<string, string>();
        private DealDB db = new DealDB();
        private XmlDocument xmldoc = new XmlDocument();
        #endregion

        #region 公共方法
        public DealXml(string xmlpath,string configurexmlpath)
        {
            doc.Load(xmlpath);
            dc = new DealConfigure();
            dc.ReadConfigure(configurexmlpath);
            Dic = dc.GetDic();
            DBUUID = dc.GetDBUUID();
        }
        public void XMLToDB()
        {
            this.XmlToDB();
        }
        #region 根据数据库的名称导出里面所有table的数据
        public void DBToXML(string dbname,string savepath)
        {
            this.DBToXml(dbname,savepath);
        }
        #endregion
        #endregion

        #region 私有方法
        #region Dictionary<"DBName","CMD">  dbname,cmd
        private string GetTNameFromXmlName(Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic, string xmltalename)
        {
            string TableName = null;
            string DeskName=null;
            foreach (var obj in dbDic)
            {
                string skey = obj.Key;
                if ((!skey.Equals("PluginUUID")) && (!skey.Equals("DataSource")) && (!skey.Equals("DBName")))
                {
                    Dictionary<string, Dictionary<string, string>> temp1 = obj.Value;
                    Dictionary<string, string> temp2=temp1["DeskName"];
                    DeskName=temp2["DeskName"];
                    if (DeskName.Equals(xmltalename))
                    {
                        Dictionary<string,string> temp3=temp1["TableName"];
                        TableName=temp3["TableName"];
                        return TableName;
                    }
                }
            }
            return null;
        }
        #endregion
        private void XmlToDB()
        {
            XmlNodeList DBNode = doc.DocumentElement.ChildNodes;
            string PluginUUID = null;
            string DBName = null;
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            foreach (XmlElement dbnode in DBNode)
            {
                string ds=dbnode.Name;
                if (ds.Equals("PluginUUID"))
                {
                    PluginUUID = dbnode.InnerText;
                    DBName=DBUUID[PluginUUID];
                    dbDic = Dic[DBName];
                }
                #region 暂时不实行
                else if (dbnode.Name.Equals("Solution"))
                { }
                else if (dbnode.Name.Equals("Name"))
                { }
                else if (dbnode.Name.Equals("Path"))
                { }
                #endregion
                else  if( (!ds.Equals("DataSource")) && (!ds.Equals("DBName")))//处理Table
                {
                    XmlNodeList multitablenodes = dbnode.ChildNodes;
                    foreach (XmlElement onetablenode in multitablenodes)
                    {
                        string xmltablename = onetablenode.Name;
                        string tablename = this.GetTNameFromXmlName(dbDic,xmltablename);
                        XmlNodeList tablenode = onetablenode.ChildNodes;
                        Dictionary<string, Dictionary<string, string>> temp = dbDic[tablename];
                        Dictionary<string, string> temp2 = temp["UnAttributeMap"];
                        string sqla = "(";
                        string sqlb = " VALUES (";
                        bool index = false;
                        string cmd = null;
                        foreach (XmlElement tableattribute in tablenode)   ///没有考虑到xml文件中节点值为空的情况，如siblingid，导致插入数据库时存在空值null，从而爆出异常
                        {
                            string key = tableattribute.Name;
                            string value = tableattribute.InnerText;

                            //找到key对应在数据库table中的字段attribute
                            string attribute = temp2[key];

                            if (index == false)
                            {
                                sqla += attribute;
                                sqlb += ("\'" + value + "\'");
                                index = true;
                            }
                            else
                            {
                                sqla+=(","+attribute);
                                sqlb += ("," + "\'" + value + "\'");
                            }
                        }
                        cmd = "INSERT INTO " + tablename + " " + sqla + ")" + sqlb + ")";
                        db.StoreTableData(DBName, cmd,Dic);
                    }
                }
            }
        }
        private void DBToXml(string dbname,string path)
        {
            List<string> tablenamelist = db.GetTableNameList(dbname,Dic);
            string tablename = null;
         
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration("1.0", null, null));
            XmlNode rootNode = xmldoc.CreateElement(dbname, "xsi", "http://www.w3.org/2001/XMLSchema-instance");

            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = Dic[dbname];
            Dictionary<string, Dictionary<string, string>> tableDic = new Dictionary<string, Dictionary<string, string>>();

            foreach(string tablenamen in tablenamelist)
            {
                tablename = tablenamen;
                tableDic=dbDic[tablename];
                Dictionary<string, string> temp1=tableDic["AttributeMap"];
                Dictionary<string,string> temp2=tableDic["DeskName"];
                string DeskName=temp2["DeskName"];

                XmlNode treenodes = xmldoc.CreateElement(DeskName);
                //插入节点
                string sqlGet = "select * from " + tablename;
                SqlDataReader reader = db.GetDBData(dbname,sqlGet,Dic);
                if (reader.HasRows == true)
                {
                    List<string> attribuelist = dc.GetAttribute(dbname, tablename);
                    int length = 0;
                    foreach (string s in attribuelist)
                    {
                        length++;
                    }
                    while (reader.Read())
                    {
                        XmlNode treenode = xmldoc.CreateElement(DeskName);
                        for (int index = 0;index<length ; index++)
                        {
                            string nodedata;
                            try
                            {
                                nodedata = reader.GetString(index);
                            }
                            catch (Exception e)
                            {
                                nodedata = null;
                            }
                            XmlNode nodexml = xmldoc.CreateElement(temp1[attribuelist[index]]);
                            nodexml.InnerText = nodedata;
                            treenode.AppendChild(nodexml);
                        }
                        treenodes.AppendChild(treenode);
                    }
                }
                rootNode.AppendChild(treenodes);
                reader.Close();
            }
            xmldoc.AppendChild(rootNode);
            try
            {
                xmldoc.Save(path);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee+"\n",xmldoc.ToString());
            }
            Console.WriteLine("xml文件生成");
        }
        #endregion
    }
}
