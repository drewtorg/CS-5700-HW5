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
    public class ParentTaskTester
    {

        ParentTask parallelTask;
        ParentTask sequentialTask;
        ParentTask emptyTask;

        LeafTask leaf1;
        LeafTask leaf2;

        Engineer e;
        Engineer e2;

        const int HOURS = 10;

        [SetUp]
        public void SetUp()
        {
            parallelTask = new ParallelParentTask(0, "TASK", "Test");
            sequentialTask = new SequentialParentTask(0, "TASK", "Test");
            emptyTask = new SequentialParentTask(0, "TASK", "Test");


            leaf1 = new LeafTask(1, "LEAF1", "Awesome Task", HOURS);
            leaf2 = new LeafTask(2, "LEAF2", "More Awesome Task", HOURS);

            e = new Engineer() { ID = 0, HoursAvailable = HOURS / 2, Name = "Test" };
            e2 = new Engineer() { ID = 1, HoursAvailable = HOURS / 2, Name = "Test" };

            leaf1.AddEngineer(e);
            leaf2.AddEngineer(e2);

            parallelTask.AddTask(leaf1);
            parallelTask.AddTask(leaf2);

            sequentialTask.AddTask(leaf1);
            sequentialTask.AddTask(leaf1);
        }

        [TestCase]
        public void TestParentEstimatedDays()
        {
            Assert.That(parallelTask.EstimatedDaysToComplete, Is.EqualTo(2));

            Assert.That(sequentialTask.EstimatedDaysToComplete, Is.EqualTo(4));

            Assert.That(emptyTask.EstimatedDaysToComplete, Is.EqualTo(-1));
        }

        [TestCase]
        public void TestParentAddTask()
        {
            Assert.That(parallelTask.Children.Count, Is.EqualTo(2));
            Assert.That(parallelTask.Children[0], Is.EqualTo(leaf1));
            Assert.That(parallelTask.Children[1], Is.EqualTo(leaf2));

            Assert.That(emptyTask.Children.Count, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentEngineers()
        {
            Assert.That(parallelTask.AssignedEngineers.Count, Is.EqualTo(2));
            Assert.That(parallelTask.AssignedEngineers.Contains(e));
            Assert.That(parallelTask.AssignedEngineers.Contains(e2));

            Assert.That(emptyTask.AssignedEngineers.Count, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentOriginalHours()
        {
            Assert.That(parallelTask.OriginalEstimatedHours, Is.EqualTo(HOURS * parallelTask.Children.Count));

            Assert.That(emptyTask.OriginalEstimatedHours, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentRevisedHours()
        {
            Assert.That(parallelTask.RevisedEstimatedHours, Is.EqualTo(HOURS * parallelTask.Children.Count));

            Assert.That(emptyTask.RevisedEstimatedHours, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentRemainingHours()
        {
            foreach (LeafTask t in parallelTask)
                t.HoursWorked = 2;

            Assert.That(parallelTask.EstimatedRemainingHours, Is.EqualTo(HOURS * parallelTask.Children.Count - parallelTask.Children.Count * 2));

            Assert.That(emptyTask.EstimatedRemainingHours, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentPercentComplete()
        {
            (parallelTask.Children[0] as LeafTask).HoursWorked = 2;
            double expectedPercent = .1;

            Assert.That(parallelTask.PercentComplete, Is.EqualTo(expectedPercent));

            Assert.That(emptyTask.PercentComplete, Is.EqualTo(0));
        }

        [TestCase]
        public void TestParentTaskList()
        {
            Assert.That(parallelTask.GetTaskList(e).Count, Is.EqualTo(1));
            Assert.That(parallelTask.GetTaskList(e)[0], Is.EqualTo(leaf1));

            Assert.That(parallelTask.GetTaskList(e2).Count, Is.EqualTo(1));
            Assert.That(parallelTask.GetTaskList(e2)[0], Is.EqualTo(leaf2));

            Assert.That(emptyTask.GetTaskList(e), Is.EquivalentTo(new List<Task>()));

            Assert.That(parallelTask.GetTaskList(null), Is.EquivalentTo(new List<Task>()));
            
        }
    }
}
