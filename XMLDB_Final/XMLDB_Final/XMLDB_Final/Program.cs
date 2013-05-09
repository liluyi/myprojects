using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLDB_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            DealDB db = new DealDB("D:\\Projects\\XMLDB_Final\\XMLDB_Final\\XMLDB_Final\\Configure.xml");
            DealXml dx = new DealXml("D:\\Projects\\XMLDB_Final\\XMLDB_Final\\XMLDB_Final\\Input.xml", "D:\\Projects\\XMLDB_Final\\XMLDB_Final\\XMLDB_Final\\Configure.xml");
            dx.XMLToDB();
            dx.DBToXML("CASREE_SFTA_DATABASE", "E:\\result");
        }
    }
}
