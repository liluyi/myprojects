using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstLINQtoDatabaseQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            NORTHWNDEntities northWindEntities = new NORTHWNDEntities();
            
            Customers cus = new Customers();
            cus.CustomerID = "liluyi";
            cus.CompanyName = "buaa";
            cus.City = "beijing";
            cus.Country = "CHN";
            cus.Region = "beijing";
            northWindEntities.Customers.AddObject(cus);

            var queryResult = from c in northWindEntities.Customers
                              where c.Country == "CHN"
                              select new
                              {
                                  ID = c.CustomerID,
                                  Name = c.CompanyName,
                                  City = c.City,
                                  State = c.Region
                              };
            foreach (var item in queryResult)
                Console.WriteLine(item);

            Console.WriteLine("Press Enter/Return to continue..");
            Console.ReadLine();

        }
    }
}
