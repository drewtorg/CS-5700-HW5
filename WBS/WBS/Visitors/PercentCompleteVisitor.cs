using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class PercentCompleteVisitor : Visitor
    {
        private double sumPercent;
        private int numOfPercents;

        public double GetPercentComplete(Task task)
        {
            sumPercent = 0;
            numOfPercents = 0;
            Visit((dynamic) task);
            return numOfPercents == 0 ? 0 : sumPercent / numOfPercents;
        }

        protected override void Visit(LeafTask task)
        {
            sumPercent += task.PercentComplete;
            numOfPercents++;
        }
    }
}
