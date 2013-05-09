using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerBase.database
{
    class Solution
    {
        public string solutionName = string.Empty;
        public int predict = 0;
        public int assign = 0;
        public int analysis = 0;
        public int design = 0;
        public int test = 0;
        public int assess = 0;

        public Solution() 
        {
        
        }


        public Solution(string solutionName,int predict, int assign, int analysis, int design, int test, int assess)
        {
            this.solutionName = solutionName;
            this.predict = predict;
            this.assign = assign;
            this.analysis = analysis;
            this.design = design;
            this.test = test;
            this.assess = assess;
        }

        public Solution(int predict, int assign, int analysis, int design, int test, int assess)
        {
            this.predict = predict;
            this.assign = assign;
            this.analysis = analysis;
            this.design = design;
            this.test = test;
            this.assess = assess; 
        }
    }
}
