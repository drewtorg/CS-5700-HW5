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
        private Task root;
        private Scheduler schedule;

        public WorkBreakdownStructure()
        {
            root = null;
        }

        public List<Task> GetAssignedTasks(Engineer e)
        {
            return schedule.TaskList[e];
        }

        public void Print(string filename)
        {
            new TextExporter().WriteToFile(filename, root);
        }
    }
}
