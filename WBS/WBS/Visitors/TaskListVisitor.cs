using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class TaskListVisitor : Visitor
    {
        private List<Task> assignedTasks;
        private Engineer target;

        public List<Task> GetAssignedTasks(Engineer e, Task task)
        {
            target = e;
            assignedTasks = new List<Task>();
            Visit((dynamic)task);
            return assignedTasks;
        }

        protected override void Visit(LeafTask task)
        {
            if (task.AssignedEngineers.Contains(target))
                assignedTasks.Add(task);
        }
    }
}
