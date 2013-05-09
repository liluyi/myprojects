using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficJudgingSystem
{
    public class JudgeInfo
    {
        public JudgeInfo(string route, int year, double j, int j0,string suggestion)
        {
            this.routesection = route;
            this.year = year;
            this.j = j;
            this.j0 = j0;
            this.suggestion = suggestion;
        }
        private string routesection=string.Empty;
        private int year = 2012;
        private double j = 0;
        private int j0 = 0;
        private string suggestion = string.Empty;
        public string RouteSection
        {
            get { return this.routesection; }
            set { this.routesection = value; }
        }
        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }
        public double J
        {
            get { return this.j; }
            set { this.j = value; }
        }
        public int J0
        {
            get { return this.j0; }
            set { this.j0 = value; }
        }
        public string Suggestion
        {
            get { return this.suggestion; }
            set { this.suggestion = value; }
        }
    }
}
