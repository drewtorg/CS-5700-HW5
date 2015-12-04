using System;
using WBS;
using WBS.Tasks;
using WBS.XML;
using System.IO;
using NUnit.Framework;

namespace WBSTest
{
    [TestFixture]
    public class TaskXMLTester
    {
        TaskImporterExporter ser;
        const string PATH = "test.xml";

        [TestFixtureSetUp]
        public void SetUp()
        {
            ser = new TaskImporterExporter();
        }

        [TestCase]
        public void TestImportExportLeafTask()
        {
            LeafTask leaf = new LeafTask(0, "TSK", "Test", 15);
            leaf.AddEngineer(new Engineer() { ID = 0, HoursAvailable = 10, Name = "Test" });
            ser.Export(leaf, PATH);

            LeafTask newLeaf = ser.Import(PATH) as LeafTask;

            Assert.That(leaf.Description, Is.EqualTo(newLeaf.Description));
            Assert.That(leaf.HoursWorked, Is.EqualTo(newLeaf.HoursWorked));
            Assert.That(leaf.ID, Is.EqualTo(newLeaf.ID));
            Assert.That(leaf.Label, Is.EqualTo(newLeaf.Label));
            Assert.That(leaf.OriginalEstimatedHours, Is.EqualTo(newLeaf.OriginalEstimatedHours));
            Assert.That(leaf.RevisedEstimatedHours, Is.EqualTo(newLeaf.RevisedEstimatedHours));
            CollectionAssert.AreEqual(leaf.AssignedEngineers, newLeaf.AssignedEngineers);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (File.Exists(PATH)) File.Delete(PATH);
        }
    }
}
