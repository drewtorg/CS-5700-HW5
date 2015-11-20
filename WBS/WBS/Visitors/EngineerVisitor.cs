using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS;
using WBS.Tasks;

namespace WBS.Visitors
{
    public class EngineerVisitor : Visitor
    {
        private List<Engineer> assignedEngineers;

        public List<Engineer> GetAssignedEngineers(Task t)
        {
            assignedEngineers = new List<Engineer>();
            Visit((dynamic)t);
            return assignedEngineers;
        }

        protected override void Visit(ParallelParentTask task)
        {
            VisitAll(task);
        }

        protected override void Visit(SequentialParentTask task)
        {
            VisitAll(task);
        }

        protected override void Visit(LeafTask task)
        {
            assignedEngineers.AddRange(task.AssignedEngineers);
        }

    }
}
