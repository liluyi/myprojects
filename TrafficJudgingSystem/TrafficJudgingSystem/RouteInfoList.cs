using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TrafficJudgingSystem
{
    public class RouteInfoList
    {
        public List<RouteInfo> infolist = new List<RouteInfo>();
        public void AddRouteInfo(RouteInfo ri)
        {
            infolist.Add(ri);
        }
        public void DeleteRouteInfo(RouteInfo ri)
        {
            infolist.Remove(ri);
        }
    }
}
