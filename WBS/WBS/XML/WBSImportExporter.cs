using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using WBS.Tasks;

namespace WBS.XML
{
    public class WBSImportExporter : XMLImporterExporter<WorkBreakdownStructure>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(WorkBreakdownStructure));

        public void Export(WorkBreakdownStructure wbs, string path)
        {
            using (var stream = File.OpenWrite(path))
            {
                serializer.Serialize(stream, wbs);
            }
        }

        public WorkBreakdownStructure Import(string path)
        {
            WorkBreakdownStructure wbs = null;
            using (var stream = File.OpenWrite(path))
            {
                wbs = serializer.Deserialize(stream) as WorkBreakdownStructure;
            }
            return wbs;
        }
    }
}
