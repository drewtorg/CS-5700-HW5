using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using WBS;
using WBS.Visitors;

namespace WBS.Tasks
{
    public abstract class ParentTask : Task, IEnumerable<Task>
    {
        public List<Task> Children { get; }

        public ParentTask(int id, string label, string description) : base(id, label, description)
        {
            Children = new List<Task>();
        }

        public void AddTask(Task t)
        {
            if(t != null)   Children.Add(t);
        }

        public void RemoveTask(Task t)
        {
            if(t != null)   Children.Remove(t);
        }

        public override List<Engineer> AssignedEngineers { get { return new EngineerVisitor().GetAssignedEngineers(this); } }
        public override int OriginalEstimatedHours { get { return new OriginalHoursVisitor().GetOriginalHours(this); } set { throw new InvalidOperationException("Cannot set Original Hour Estimate"); } }
        public override int RevisedEstimatedHours { get { return new RevisedHoursVisitor().GetRevisedHours(this); } set { throw new InvalidOperationException("Cannot set Revised Hour Estimate"); } }
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

        //Necessary for XML Serialization
        public void Add(object o)
        {
            Children.Add(o as Task);
        }
    }
}
