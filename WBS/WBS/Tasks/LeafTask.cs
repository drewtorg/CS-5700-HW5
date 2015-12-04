using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using WBS;

namespace WBS.Tasks
{
    public class LeafTask : Task
    {
        private int hoursWorked;
        private int originalEstimatedHours;
        private int revisedEstimatedHours;
        public int HoursWorked { get { return hoursWorked; } set { hoursWorked = value > 0 ? value : 0; } }
        public override int OriginalEstimatedHours { get { return originalEstimatedHours; } set { originalEstimatedHours = value > 0 ? value : 0; } }
        public override int RevisedEstimatedHours { get { return revisedEstimatedHours; } set { revisedEstimatedHours = value > 0 ? value : 0; } }
        public override List<Engineer> AssignedEngineers { get; }
        public override int EstimatedRemainingHours { get { return RevisedEstimatedHours - HoursWorked < 0 ? 0 : RevisedEstimatedHours - HoursWorked; } }
        public override double PercentComplete
        {
            get
            {
                return HoursWorked == 0 ? 0 
                    : RevisedEstimatedHours == 0 ? 1  
                    : (double) HoursWorked / RevisedEstimatedHours;
            }
        }

        public override int EstimatedDaysToComplete
        {
            get
            {
                int sum = AssignedEngineers.Sum(e => e.HoursAvailable);
                return sum == 0 ? -1 : (int)Math.Ceiling((double)EstimatedRemainingHours / sum);
            }
        }

        public LeafTask()
        {
            AssignedEngineers = new List<Engineer>();
        }

        public LeafTask(int id, string label, string description, int hours) : base(id, label, description)
        {
            OriginalEstimatedHours = RevisedEstimatedHours = hours;
            HoursWorked = 0;
            AssignedEngineers = new List<Engineer>();
            Description = description;
        }

        public void AddEngineer(Engineer e)
        {
            if(e != null) AssignedEngineers.Add(e);
        }

        public void RemoveEngineer(Engineer e)
        {
            if(e != null) AssignedEngineers.Remove(e);
        }
    }
}
