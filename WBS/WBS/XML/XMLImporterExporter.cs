using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WBS.XML
{
    public interface XMLImporterExporter<T>
    {
        void Export(T wbs, string path);
        T Import(string path);
    }
}
