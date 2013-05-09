using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace XMLDB_Final
{
    class DealDB
    {
        #region  私有变量
        private DealConfigure dc = new DealConfigure();
        private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> Dic = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>();
        private Dictionary<string, string> DBUUID = new Dictionary<string, string>();
        #endregion

        #region 公有方法
        public DealDB()
        { 
        }
        public DealDB(string configurexmlpath)
        {
            dc = new DealConfigure();
            dc.ReadConfigure(configurexmlpath);
            Dic = dc.GetDic();
            DBUUID = dc.GetDBUUID();
            this.CheckXMLDB();
        }
        public Boolean StoreTableData(string dbname, string cmd,Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> dDic)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = dDic[dbname];
            Dictionary<string, Dictionary<string, string>> temp1 = dbDic["DataSource"];
            Dictionary<string, string> temp2 = temp1["DataSource"];
            string datasource = temp2["DataSource"];

            SqlConnection dataConnection = this.ConnectionDB(datasource,dbname);
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;
                dataCommand.CommandType = CommandType.Text;
                dataCommand.CommandText = cmd;
                dataCommand.ExecuteNonQuery();
                this.CloseDB(dataConnection);
                return true;
            }
            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    Console.WriteLine("插入的记录已经存在");
                }
                Console.WriteLine(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("其它异常：" + e.ToString());
                return false;
            }
        }
        //根据数据库名字得到所有数据表的名称
        public List<string> GetTableNameList(string dbname,Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> dDic)
        {
            List<string> tablelist = new List<string>();
            string datasource = this.GetDataSource(dbname,dDic);
            SqlConnection sqlcn = this.ConnectionDB(datasource, dbname);
            //使用信息架构视图
            SqlCommand sqlcmd = new SqlCommand("SELECT OBJECT_NAME (id) FROM sysobjects WHERE xtype = 'U' AND OBJECTPROPERTY (id, 'IsMSShipped') = 0", sqlcn);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            while (dr.Read())
            {
                tablelist.Add(dr.GetString(0));
            }
            this.CloseDB(sqlcn);
            return tablelist;
        }
        public SqlDataReader GetDBData(string dbname, string sql,Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> dDic)
        {
            string datasource = this.GetDataSource(dbname,dDic);
            SqlDataReader result = null;
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = this.ConnectionDB(datasource,dbname);
                dataCommand.CommandType = CommandType.Text;
                dataCommand.CommandText = sql;

                result = dataCommand.ExecuteReader();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        #endregion

        #region 私有方法
        private SqlConnection ConnectionDB(string DataSource,string DBName)
        {
            SqlConnection dataConnection = new SqlConnection();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = DataSource;  // 数据库所在机器的名字
                builder.InitialCatalog = DBName;//数据库名字
                builder.IntegratedSecurity = true;
                dataConnection.ConnectionString = builder.ConnectionString;
                dataConnection.Open();
                return dataConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        private Boolean CloseDB(SqlConnection dataConnection)
        {
            try
            {
                dataConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        private Boolean DBExist(string dbname)
        {
            SqlConnection myCon = this.ConnectionDB("LILUYI-PC\\SQLEXPRESS", "master");//这里DataSource硬编码，需要修改
            string sql = "select * from sys.databases where name=\'" + dbname + "\'";
            SqlCommand myCmd = new SqlCommand(sql, myCon);
            object n = myCmd.ExecuteScalar();
            if (n != null)
            {
                this.CloseDB(myCon);
                return true;
            }
            else
            {
                this.CloseDB(myCon);
                return false;
            }
        }
        private Boolean DBCreate(string dbname)
        {
            SqlConnection myCon = this.ConnectionDB("LILUYI-PC\\SQLEXPRESS", "master");//这里DataSource硬编码，需要修改
            string sql = "create database " + dbname;
            SqlCommand myCmd = new SqlCommand(sql, myCon);
            myCmd.Connection = myCon;
            myCmd.CommandType = CommandType.Text;
            myCmd.CommandText = sql;

            try
            {
                myCmd.ExecuteNonQuery();
                this.CloseDB(myCon);
                return true;
            }
            catch (Exception e)
            {
                this.CloseDB(myCon);
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        private Boolean TableExist(string dbname,string tablename)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic=Dic[dbname];
            Dictionary<string, Dictionary<string, string>> temp1 =dbDic["DataSource"];
            Dictionary<string, string> temp2=temp1["DataSource"];
            string datasource=temp2["DataSource"];
            string sqlStr = "if objectproperty(object_id(+\'" + tablename + "\'),'IsUserTable')=1 select 1 else select 0";
            SqlConnection dataConnection = this.ConnectionDB(datasource,dbname);
            SqlCommand cmd = new SqlCommand(sqlStr,dataConnection);
            object c = cmd.ExecuteScalar();
            if (c.ToString() == "0")
            {
                this.CloseDB(dataConnection);
                return false;
            }
            else
            {
                this.CloseDB(dataConnection);
                return true;
            }
        }
        #region  //Dictionary<string,string> datasource,dbname,cmd
        private Boolean TableCreate(Dictionary<string,string> tablecreateoper)
        {
            string datasource=tablecreateoper["DataSource"];
            string dbname=tablecreateoper["DBName"];
            string cmd=tablecreateoper["CMD"];
            SqlConnection dataConnection = this.ConnectionDB(datasource, dbname);
            SqlCommand dataCommand = new SqlCommand();
            dataCommand.Connection = dataConnection;
            dataCommand.CommandType = CommandType.Text;
            dataCommand.CommandText = cmd;

            try
            {
                dataCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        #endregion
        private Dictionary<string,string> TableCreateOper(string dbname, string tablename)
        {
            Dictionary<string, string> value = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = Dic[dbname];
            Dictionary<string, Dictionary<string, string>> temp1 = dbDic["DataSource"];
            Dictionary<string, string> temp2 = temp1["DataSource"];
            string datasource = temp2["DataSource"];
            value.Add("DataSource",datasource);
            value.Add("DBName",dbname);

            //得到xml描述中table中属性值
            Dictionary<string, Dictionary<string, string>> temp3=dbDic[tablename];
            Dictionary<string, string> temp4=temp3["AttributeMap"];

            string AttributeString = null;
            bool index = false;
            foreach (var Attribute in temp4)
            {
                string s = Attribute.Key;
                if (index == false)
                {
                    AttributeString += (s + " nvarchar(50)");
                    index = true;
                }
                else
                {
                    AttributeString += ("," + s + " nvarchar(50)");
                }
            }
            string cmd = "CREATE TABLE " + tablename + "(" + AttributeString + ")";
            value.Add("CMD",cmd);
          
            return value;
        }
        #region 在读入Configure.xml后，检查里面描述的数据库，表是否存在，否则新建
        private Boolean CheckXMLDB() 
        {
            foreach (var obj in Dic)
            {
                string dbname = obj.Key;
                if (this.DBExist(dbname) == false)
                {
                    this.DBCreate(dbname);
                }
                Dictionary<string, Dictionary<string, Dictionary<string, string>>> temp2=Dic[dbname];
                foreach (var pbj in temp2)
                {
                    string skey = pbj.Key;
                    if( (!skey.Equals("PluginUUID")) && (!skey.Equals("DataSource")) && (!skey.Equals("DBName")) )
                    {
                        if (this.TableExist(dbname, skey) == false)
                        {
                            this.TableCreate(this.TableCreateOper(dbname,skey));
                        }
                    }
                }
            }
            return true;
        }
        private string GetDataSource(string dbname,Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> dDic)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dbDic = dDic[dbname];
            Dictionary<string, Dictionary<string, string>> temp1 = dbDic["DataSource"];
            Dictionary<string, string> temp2 = temp1["DataSource"];
            string datasource = temp2["DataSource"];
            return datasource;
        }
        #endregion
        #endregion
    }
}
