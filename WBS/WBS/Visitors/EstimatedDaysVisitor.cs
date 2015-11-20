using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class EstimatedDaysVisitor : Visitor
    {
        private int days;

        public int GetEstimatedDays(Task task)
        {
            days = 0;
            Visit((dynamic) task);
            return days;
        }

        protected override void Visit(ParallelParentTask task)
        {
            int maxDays = 0;
            foreach(Task t in task)
            {
                int estimatedDays = t.EstimatedDaysToComplete;

                if (estimatedDays > maxDays)
                    maxDays = estimatedDays;
            }
            days += maxDays;
        }

        protected override void Visit(LeafTask task)
        {
            days += task.EstimatedDaysToComplete;
        }
    }
}
