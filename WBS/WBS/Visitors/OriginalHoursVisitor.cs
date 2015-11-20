using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class OriginalHoursVisitor : Visitor
    {
        private int hours;

        public int GetOriginalHours(Task t)
        {
            hours = 0;
            Visit((dynamic)t);
            return hours;
        }

        protected override void Visit(LeafTask task)
        {
            hours += task.OriginalEstimatedHours;
        }

        protected override void Visit(ParallelParentTask task)
        {
            VisitAll(task);
        }

        protected override void Visit(SequentialParentTask task)
        {
            VisitAll(task);
        }
    }
}
