using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using WBS.Tasks;
using WBS;

namespace WBSTest
{
    [TestFixture]
    public class TaskFactoryTester
    { 
        [TestCase]
        public void TestCreateParent()
        {
            ParentTask parallelTask = TaskFactory.Create(typeof(ParallelParentTask), "TSK", "Test Test");
            ParentTask sequentialTask = TaskFactory.Create(typeof(SequentialParentTask), "TSK", "Test Test");

            Assert.That(parallelTask.GetType(), Is.EqualTo(typeof(ParallelParentTask)));
            Assert.That(sequentialTask.GetType(), Is.EqualTo(typeof(SequentialParentTask)));

            Assert.That(parallelTask.ID, Is.EqualTo(sequentialTask.ID - 1));
        }

        [TestCase]
        public void TestCreateLeaf()
        {
            LeafTask leaf = TaskFactory.Create("TSK", "Test Test", 10);

            Assert.That(leaf.GetType(), Is.EqualTo(typeof(LeafTask)));
        }

    }
}
