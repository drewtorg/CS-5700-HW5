using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS;
using WBS.Tasks;

namespace WBS.Visitors
{
    public abstract class Visitor
    {
        protected void VisitAll(IEnumerable<Task> tasks)
        {
            foreach (Task t in tasks)
                Visit((dynamic) t);
        }

        protected virtual void Visit(SequentialParentTask task)
        {
            VisitAll(task);
        }

        protected virtual void Visit(ParallelParentTask task)
        {
            VisitAll(task);
        }

        protected abstract void Visit(LeafTask task);
    }
}
