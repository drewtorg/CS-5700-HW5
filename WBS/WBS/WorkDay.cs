using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS
{
    public class WorkDay
    {
        public int Day { get; set; }
        public Dictionary<Engineer, List<Task>> AssignedTasks { get; set; }
        public List<Engineer> ScheduledWorkers { get { return AssignedTasks.Keys.ToList(); } }

        public WorkDay(int day)
        {
            Day = day;
            AssignedTasks = new Dictionary<Engineer, List<Task>>();
        }
    }
}
