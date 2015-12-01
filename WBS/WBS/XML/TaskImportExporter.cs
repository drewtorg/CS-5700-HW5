using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using WBS.Tasks;

namespace WBS.XML
{
    public class TaskImportExporter : XMLImporterExporter<Task>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(Task));

        public void Export(Task wbs, string path)
        {
            using (var stream = File.OpenWrite(path))
            {
                serializer.Serialize(stream, wbs);
            }
        }

        public Task Import(string path)
        {
            Task t = null;
            using (var stream = File.OpenWrite(path))
            {
                t = serializer.Deserialize(stream) as Task;
            }
            return t;
        }
    }
}
