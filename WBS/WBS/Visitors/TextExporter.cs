using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class TextExporter : Visitor
    {
        private StreamWriter writer;
        string tabs;

        public void WriteToFile(string filename, Task task)
        {
            tabs = "";
            using (writer = new StreamWriter(File.OpenWrite(filename)))
            {
                writer.WriteLine("Work Breakdown Structure Outline\n");
                Visit((dynamic) task);
            }
        }

        protected override void Visit(ParallelParentTask task)
        {
            writer.WriteLine($"{tabs}Parallel Task - {task.Label} - {task.Description}");
            base.Visit(task);
        }

        protected override void Visit(SequentialParentTask task)
        {
            writer.WriteLine($"{tabs}Sequential Task - {task.Label} - {task.Description}");
            base.Visit(task);
        }

        protected override void Visit(LeafTask task)
        {
            tabs += "\t";
            
            writer.WriteLine($"{tabs}Task - {task.Label} - {task.Description}");
            writer.WriteLine($"{tabs}  Estimated Hours: {task.RevisedEstimatedHours}");
            writer.WriteLine($"{tabs}  Percent Complete: {task.PercentComplete:0%}");
            writer.WriteLine($"{tabs}  Hours Remaining: {task.EstimatedRemainingHours}");
            writer.WriteLine($"{tabs}  Hours Worked: {task.HoursWorked}");
            writer.WriteLine($"{tabs}  Days to Complete: {task.EstimatedDaysToComplete}");
            writer.WriteLine($"{tabs}  Engineers:");
            foreach (Engineer e in task.AssignedEngineers)
                writer.WriteLine($"{tabs}\t{e.Name}");

            tabs = tabs.Remove(0, 1);
        }
    }
}
