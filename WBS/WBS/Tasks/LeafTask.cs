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
        public override int EstimatedRemainingHours { get { return RevisedEstimatedHours - HoursWorked; } }
        public override int OriginalEstimatedHours { get; }
        public override double PercentComplete { get { return (double)EstimatedRemainingHours / RevisedEstimatedHours; } }
        public override int RevisedEstimatedHours { get { return revisedHours; } }
        public int HoursWorked { get; set; }

        int revisedHours;

        public LeafTask(int hours)
        {
            OriginalEstimatedHours = revisedHours = hours;
            HoursWorked = 0;
            AssignedEngineers = new List<Engineer>();
        }

        public void ChangeEstimatedHours(int newHours)
        {
            revisedHours = newHours;
        }

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
