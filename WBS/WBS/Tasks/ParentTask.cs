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
        public override int OriginalEstimatedHours { get { return new OriginalHoursVisitor().GetOriginalHours(this); } }
        public override int RevisedEstimatedHours { get { return new RevisedHoursVisitor().GetRevisedHours(this); } }
        public override double PercentComplete { get { return new PercentCompleteVisitor().GetPercentComplete(this); } }
        public override int EstimatedRemainingHours { get { return new RemainingHoursVisitor().GetRemainingHours(this); } }
        public override int EstimatedDaysToComplete { get { return new EstimatedDaysVisitor().GetEstimatedDays(this); } }

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
