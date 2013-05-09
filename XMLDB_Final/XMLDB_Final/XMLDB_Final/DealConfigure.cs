using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;

namespace XMLDB_Final
{
    class DealConfigure
    {
        #region 私有变量
        private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> Dic = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
        private Dictionary<string, string> DBUUID = new Dictionary<string, string>();
        private XmlDocument doc = new XmlDocument();
        #endregion 

        #region 公有方法
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> GetDic()
        {
            return this.Dic;
        }
        public Dictionary<string, string> GetDBUUID()
        {
            return this.DBUUID;
        }
        public void ReadConfigure(string path)
        {
            doc.Load(path);
            XmlNodeList DBNode = doc.DocumentElement.ChildNodes;

            #region //遍历各个<DataBase></DataBase>,存入Dic
            foreach (XmlElement dbnode in DBNode)
            {
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
                string DBName = null;
                string PluginUUID = null;
                #region //遍历<DataBase>下的节点
                foreach (XmlElement dbattributenode in dbnode)
                {
                    string DataSource = null;
                    string TableName=null;
                    //TableName->tableDic( Dictionary<string, Dictionary<string, string>> )
                    
                    #region 得到PluginUUID，存入PluginUUID
                    if (dbattributenode.Name.Equals("PluginUUID"))
                    {
                        PluginUUID = dbattributenode.InnerText;
                        Dictionary<string, string> temp1 = new Dictionary<string, string>();
                        Dictionary<string, Dictionary<string, string>> temp2 = new Dictionary<string, Dictionary<string, string>>();
                        temp1.Add("PluginUUID", PluginUUID);
                        temp2.Add("PluginUUID", temp1);
                        dbDic.Add("PluginUUID", temp2);
                    }
                    #endregion
                    #region 得到DataSource，存入DataSource
                    else if (dbattributenode.Name.Equals("DataSource"))
                    {
                        DataSource = dbattributenode.InnerText;
                        Dictionary<string, string> temp1 = new Dictionary<string, string>();
                        Dictionary<string, Dictionary<string, string>> temp2 = new Dictionary<string, Dictionary<string, string>>();
                        temp1.Add("DataSource", DataSource);
                        temp2.Add("DataSource", temp1);
                        dbDic.Add("DataSource", temp2);
                    }
                    #endregion
                    #region 得到DBName，存入DBName
                    else if (dbattributenode.Name.Equals("DBName"))
                    {
                        DBName = dbattributenode.InnerText;
                        Dictionary<string, string> temp1 = new Dictionary<string, string>();
                        Dictionary<string, Dictionary<string, string>> temp2 = new Dictionary<string, Dictionary<string, string>>();
                        temp1.Add(DBName,DBName);
                        temp2.Add(DBName,temp1);
                        dbDic.Add("DBName",temp2);
                    }
                    #endregion
                    #region 遍历Table,将TableName,DeskName，<AttributeMap>存入tableDic,tableDic存入dbDic
                    else if (dbattributenode.Name.Equals("Table"))
                    {
                        Dictionary<string, Dictionary<string, string>> tableDic = new Dictionary<string, Dictionary<string, string>>();
                        string DeskName = null;
                        Dictionary<string,string> AttriMapDic = new Dictionary<string, string>();
                        Dictionary<string, string> UnAttriMapDic = new Dictionary<string, string>();
                        //遍历<Table>内的节点
                        foreach (XmlElement tableattributenode in dbattributenode)
                        {
                            #region 得到TableName,存入TableName
                            if (tableattributenode.Name.Equals("TableName"))
                            {
                                TableName = tableattributenode.InnerText;
                            }
                            #endregion
                            #region 得到DeskName，存入DeskName
                            else if (tableattributenode.Name.Equals("DeskName"))
                            {
                                DeskName = tableattributenode.InnerText;
                            }
                            #endregion
                            #region //遍历<AttributeMap>内节点,存入AttriMapDic
                            else if (tableattributenode.Name.Equals("AttributeMap"))
                            {
                                string attribute = null;
                                string deskmap = null;
                                foreach (XmlElement attrinode in tableattributenode)
                                {
                                    if (attrinode.Name.Equals("Attribute"))
                                    {
                                        attribute = attrinode.InnerText;
                                    }
                                    else if (attrinode.Name.Equals("DeskMap"))
                                    {
                                        deskmap = attrinode.InnerText;
                                    }
                                }
                                AttriMapDic.Add(attribute, deskmap);
                                UnAttriMapDic.Add(deskmap,attribute);
                            }
                            #endregion
                        }
                        #region 将TableName,DeskName,<AttributeMap>存入tableDic
                        Dictionary<string, string> tempDic1 = new Dictionary<string, string>();
                        Dictionary<string, string> tempDic2 = new Dictionary<string, string>();
                        tempDic1.Add("TableName",TableName);
                        tempDic2.Add("DeskName",DeskName);
                        tableDic.Add("TableName",tempDic1);
                        tableDic.Add("DeskName",tempDic2);
                        tableDic.Add("AttributeMap",AttriMapDic);
                        tableDic.Add("UnAttributeMap",UnAttriMapDic);
                        #endregion
                        #region 将tableDic存入dbDic中
                        dbDic.Add(TableName,tableDic);
                        #endregion
                    }
                    #endregion
                }
                #endregion
                #region//PluginUUID<----->DBName对应关系存在DBUUID
                DBUUID.Add(PluginUUID,DBName);
                #endregion
                #region //将<DataBase>信息存入Dic
                Dic.Add(DBName,dbDic);
                #endregion
            }
            #endregion
        }
        public List<string> GetAttribute(string dbname, string tablename)
        {
            List<string> list = new List<string>();
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = Dic[dbname];
            Dictionary<string, Dictionary<string, string>> tableDic = dbDic[tablename];
            Dictionary<string, string> AttributeDic = tableDic["AttributeMap"];
            foreach (var obj in AttributeDic)
            {
                string attribute = obj.Key;
                list.Add(attribute);
            }
            return list;
        }
        #endregion
    }
}
