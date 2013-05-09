using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficJudgingSystem
{
    public class RouteInfo
    {
        int year;
        string routename;
        string routetype;
        string src, dst;

        public int Year { 
            get
            {
                return year;
            }
            set
            {
                if (value > 0)
                    year = value;
                else
                    year = 2012;
            }
        }
        public string RouteName {
            get
            {
                return routename;
            }
            set
            {
                routename = value;
            }
        }
        public string RouteType
        {
            get
            { 
                return routetype; 
            }
            set
            { 
                routetype = value; 
            }
        }
        
        public string Source
        {
            get
            {
                return src;
            }
            set
            {
                src = value;
            }
        }
        public string Destination
        {
            get
            {
                return dst;
            }
            set
            {
                dst = value;
            }
        }
    }
}
