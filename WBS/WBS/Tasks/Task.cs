using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using WBS.Visitors;

namespace WBS.Tasks
{
    public abstract class Task
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public abstract List<Engineer> AssignedEngineers { get; }
        public abstract int OriginalEstimatedHours { get; set; }
        public abstract int RevisedEstimatedHours { get; set; }
        public abstract double PercentComplete { get; }
        public abstract int EstimatedRemainingHours { get; }
        public abstract int EstimatedDaysToComplete { get; }

        public Task() { }

        public Task(int id, string label, string description)
        {
            ID = id;
            Label = label;
            Description = description;
        }

        public List<Task> GetTaskList(Engineer e) { return new TaskListVisitor().GetAssignedTasks(e, this); }
    }
}
