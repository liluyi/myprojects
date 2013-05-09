using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace XMLToDB
{
    struct location
    {
        public string database;
        public string table;
        public string attribute;
    };
    class Configure
    {
        private Dictionary<string, location> dic=new Dictionary<string,location>();
        private XmlDocument doc = new XmlDocument();
        private string key;
        private location value;
        public Configure()
        {
            //E:\Project\Visual Studio 2010\XMLToDB\XMLToDB
            doc.Load("E:\\Project\\Visual Studio 2010\\XMLToDB\\XMLToDB\\config.xml");
            XmlNodeList nodelist = doc.DocumentElement.ChildNodes;
            foreach (XmlElement node in nodelist)
            {
                key = node.Name;
                XmlNodeList childlist = node.ChildNodes;
                foreach (XmlElement childnode in childlist)
                {
                    if (childnode.Name.Equals("database"))
                    {
                        value.database = childnode.InnerText;
                    }
                    else if (childnode.Name.Equals("table"))
                    {
                        value.table = childnode.InnerText;
                    }
                    else if (childnode.Name.Equals("attribute"))
                    {
                        value.attribute = childnode.InnerText;
                    }
                }
                dic.Add(key,value);
            }
        }
        public location search(string key)
        {
            location loc;
            try
            {
                loc = dic[key];
            }
            catch (Exception e)
            {
                loc.database = "";
                loc.table = "";
                loc.attribute = "";
            }
            return loc;
        }
    }
    class Database
    {
        private SqlConnection dataConnection = new SqlConnection();
        private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public Database()
        {
        }
        public Boolean ConnectionDatabase(string database)
        {
            try{
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "ZGC-20121108UAA";  // 数据库所在机器的名字
                builder.InitialCatalog = database;//数据库名字
                builder.IntegratedSecurity = true;
                dataConnection.ConnectionString = builder.ConnectionString;
                dataConnection.Open();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public Boolean CloseDatabase()
        {
            dataConnection.Close();
            return true;
        }
        public void StoreData(string sql)
        {
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;
                dataCommand.CommandType = CommandType.Text;
                dataCommand.CommandText = sql;
                dataCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            Configure conf = new Configure();
            Database database = new Database();
            //E:\Project\Visual Studio 2010\XMLToDB\XMLToDB
            doc.Load("E:\\Project\\Visual Studio 2010\\XMLToDB\\XMLToDB\\input.xml");
            XmlNodeList nodes = doc.DocumentElement.ChildNodes;


            int i = 1;
            string sql1 = "insert into table values('value1','value2','value3','value4','value5','value6','value7','value8')";//sftatreenodes
            string sql2 = "insert into table values('value1','value2','value3')";//sftanoderelation
            location loc;
            loc.database = "";
            foreach (XmlElement node in nodes)
            {
                string nodename = node.Name;
                string value = node.InnerText;
                loc = conf.search(nodename);
                Console.WriteLine("nodename: " + nodename);
                Console.WriteLine("database: " + loc.database);
                Console.WriteLine("tablename: " + loc.table);
                Console.WriteLine("attribute: " + loc.attribute);
               // sql = sql.Replace("table", loc.table);
                //sql = sql.Replace("value" + i.ToString(), value);
                i++;
            }
           // Console.WriteLine("sql 语句为:" + sql);
            database.ConnectionDatabase(loc.database);
           // database.StoreData(sql.Trim());
            database.CloseDatabase();


            /*int i = 1;
            string sql = "insert into table values('value1','value2')";
            location loc;
            loc.database = "";
            foreach (XmlElement node in nodes)
            {
                string nodename = node.Name;
                string value = node.InnerText;
                loc = conf.search(nodename);
                Console.WriteLine("nodename: " + nodename);
                Console.WriteLine("database: " + loc.database);
                Console.WriteLine("tablename: " + loc.table);
                Console.WriteLine("attribute: " + loc.attribute);
                sql=sql.Replace("table", loc.table);
                sql=sql.Replace("value" + i.ToString(), value);
                i++;
            }
            Console.WriteLine("sql 语句为:"+sql);
            database.ConnectionDatabase(loc.database);
            database.StoreData(sql.Trim());
            database.CloseDatabase();
             */
        }
    }
}
