using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS.Tasks
{
    public abstract class Task
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public abstract List<Engineer> AssignedEngineers { get; }
        public abstract int OriginalEstimatedHours { get; protected set; }
        public abstract int RevisedEstimatedHours { get; protected set; }
        public abstract int PercentComplete { get; }
        public abstract int EstimatedRemainingHours { get; set; }
        public abstract int EstimatedDaysToComplete { get; }
    }
}
