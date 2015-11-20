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
                Visit((dynamic)t);
        }
        protected abstract void Visit(SequentialParentTask task);
        protected abstract void Visit(ParallelParentTask task);
        protected abstract void Visit(LeafTask task);
    }
}
