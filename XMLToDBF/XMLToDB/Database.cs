using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XMLToDB
{
    class Database
    {
        private SqlConnection dataConnection = new SqlConnection();
        private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public Database()
        {
        }
        public Boolean ConnectionDatabase(string database)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "ZGC-20121108UAA";  // 数据库所在机器的名字
                builder.InitialCatalog = database;//数据库名字
                builder.IntegratedSecurity = true;
                dataConnection.ConnectionString = builder.ConnectionString;
                dataConnection.Open();
                return true;
            }
            catch (Exception e)
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
            catch (SqlException e)
            {
                if (e.Number == 2627)
                {
                    Console.WriteLine("插入的记录已经存在");
                    return;
                }
                Console.WriteLine(e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("其它异常："+e.ToString());
            }
        }
        public Int32 Search(string sql)
        {
            Int32 result=-2;
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;
                dataCommand.CommandType = CommandType.Text;
                dataCommand.CommandText = sql;

                SqlDataReader dataReader = dataCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    result = dataReader.GetInt32(0);
                }
                dataReader.Close();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return -1;
            }
        }
        public SqlDataReader GetSftaInfo(string sql)
        {
            SqlDataReader result = null;
            try
            {
                SqlCommand dataCommand = new SqlCommand();
                dataCommand.Connection = dataConnection;
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
    }
}
