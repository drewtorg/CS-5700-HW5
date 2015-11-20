using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WBS;
using WBS.Visitors;

namespace WBS.Tasks
{
    public abstract class ParentTask : Task, IEnumerable<Task>
    {
        public List<Task> Children { get; set; }

        public ParentTask()
        {
            Children = new List<Task>();
        }

        public override List<Engineer> AssignedEngineers { get { return new EngineerVisitor().GetAssignedEngineers(this); } }

        public IEnumerator<Task> GetEnumerator()
        { 
            return ((IEnumerable<Task>)Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Task>)Children).GetEnumerator();
        }
    }
}
