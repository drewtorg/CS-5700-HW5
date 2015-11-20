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

        public List<Engineer> GetAssignedEngineers(Task task)
        {
            assignedEngineers = new List<Engineer>();
            Visit((dynamic) task);
            return assignedEngineers;
        }

        protected override void Visit(LeafTask task)
        {
            assignedEngineers.AddRange(task.AssignedEngineers);
        }
    }
}
