using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using WBS.Tasks;

namespace WBS.XML
{
    public class TaskImporterExporter : XMLImporterExporter<Task>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(Task), new Type[] {
                                                        typeof(LeafTask), typeof(ParentTask),
                                                        typeof(ParallelParentTask), typeof(SequentialParentTask)
                                                    });

        public void Export(Task t, string path)
        {
            using (var stream = File.OpenWrite(path))
            {
                serializer.Serialize(stream, t);
            }
        }

        public Task Import(string path)
        {
            Task t = null;
            using (var stream = File.OpenRead(path))
            {
                t = serializer.Deserialize(stream) as Task;
            }
            return t;
        }
    }
}
