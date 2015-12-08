using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;
using WBS.Visitors;

namespace WBS
{
    public class WorkBreakdownStructure
    {
        public Task InitialTask { get; set; }
        public Schedule ProjectSchedule { get; set; }

        public WorkBreakdownStructure()
        {
            InitialTask = null;
        }

        public void Print(string filename) { new TextExporter().WriteToFile(filename, InitialTask); }
        public List<Task> GetTaskList(Engineer e) { return InitialTask.GetTaskList(e); }
        public int EstimatedTotalHours { get { return InitialTask.RevisedEstimatedHours; } }
        public double PercentComplete { get { return InitialTask.PercentComplete; } }
        public int EstimatedRemainingHours { get { return InitialTask.EstimatedRemainingHours; } }
        public int EstimatedDaysToComplete { get { return InitialTask.EstimatedDaysToComplete; } }
        public Schedule CreateEstimatedSchedule() { return new SchedulerVisitor().GetEstimatedSchedule(InitialTask); }
    }
}
