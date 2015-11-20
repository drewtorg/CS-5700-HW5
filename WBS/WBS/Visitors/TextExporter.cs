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
            writer.WriteLine($"{tabs}Parallel Task - {task.Label}");
            tabs += "\t";
            base.Visit(task);
            tabs = tabs.Remove(0,1);
        }

        protected override void Visit(SequentialParentTask task)
        {
            writer.WriteLine($"{tabs}Sequential Task - {task.Label}");
            tabs += "\t";
            base.Visit(task);
            tabs = tabs.Remove(0, 1);
        }

        protected override void Visit(LeafTask task)
        {
            writer.WriteLine($"{tabs}Task - {task.Label}");
        }
    }
}
