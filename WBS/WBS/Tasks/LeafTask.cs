using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WBS;

namespace WBS.Tasks
{
    public class LeafTask : Task
    {
        public override List<Engineer> AssignedEngineers { get; }
        public override int EstimatedDaysToComplete { get { return (EstimatedRemainingHours / 24) + 1; } }
        public override int EstimatedRemainingHours { get; }
        public override int OriginalEstimatedHours { get; }
        public override int PercentComplete { get { return (int)((((double)EstimatedRemainingHours) / RevisedEstimatedHours) * 100); } }
        public override int RevisedEstimatedHours { get { throw new NotImplementedException(); } }

        public void AddEngineer(Engineer e)
        {
            AssignedEngineers.Add(e);
        }
        public void RemoveEngineer(Engineer e)
        {
            AssignedEngineers.Remove(e);
        }
    }
}
