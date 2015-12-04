using System;

using WBS;
using WBS.Tasks;

using NUnit.Framework;

namespace WBSTest
{
    [TestFixture]
    public class LeafTaskTester
    {
        [TestCase]
        public void TestLeafConstructor()
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", 15);

            Assert.That(task.Description, Is.EqualTo("Test Task"));
            Assert.That(task.Label, Is.EqualTo("TASK"));
            Assert.That(task.ID, Is.EqualTo(0));
            Assert.That(task.HoursWorked, Is.EqualTo(0));
            Assert.That(task.OriginalEstimatedHours, Is.EqualTo(15));
            Assert.That(task.RevisedEstimatedHours, Is.EqualTo(15));
            Assert.That(task.EstimatedRemainingHours, Is.EqualTo(15));
            Assert.That(task.PercentComplete, Is.EqualTo(0));
            Assert.That(task.AssignedEngineers.Count, Is.EqualTo(0));
        }

        [TestCase]
        public void TestLeafEngineers()
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", 15);
            Engineer e = new Engineer() { ID = 0, Name = "Test", HoursAvailable = 24 };

            task.AddEngineer(e);
            task.AddEngineer(null);

            Assert.That(task.AssignedEngineers.Count, Is.EqualTo(1));
            Assert.That(task.AssignedEngineers[0], Is.EqualTo(e));

            task.RemoveEngineer(null);
            task.RemoveEngineer(e);

            Assert.That(task.AssignedEngineers.Count, Is.EqualTo(0));
        }

        [TestCase]
        public void TestLeafEstimatedDays()
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", 15);
            Engineer e = new Engineer() { ID = 0, Name = "Test", HoursAvailable = 2 };
            Engineer e2 = new Engineer() { ID = 0, Name = "Test", HoursAvailable = 3 };

            task.AddEngineer(e);
            task.AddEngineer(e2);

            Assert.That(task.EstimatedDaysToComplete, Is.EqualTo(3));

            task.HoursWorked = 8;

            Assert.That(task.EstimatedRemainingHours, Is.EqualTo(7));
            Assert.That(task.EstimatedDaysToComplete, Is.EqualTo(2));

            task.RemoveEngineer(e);
            task.RemoveEngineer(e2);

            Assert.That(task.EstimatedDaysToComplete, Is.EqualTo(-1));
        }

        [TestCase(1, Result = 1),
         TestCase(0, Result = 0),
         TestCase(-1, Result = 0)]
        public int TestLeafHoursWorked(int hoursWorked)
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", 15);
            task.HoursWorked = hoursWorked;

            return task.HoursWorked;
        }

        [TestCase(1),
         TestCase(0),
         TestCase(-1)]
        public void TestLeafEstimatedHours(int hours)
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", hours);

            if (hours > 0)
            {
                Assert.That(task.OriginalEstimatedHours, Is.EqualTo(Math.Abs(hours)));
                Assert.That(task.RevisedEstimatedHours, Is.EqualTo(Math.Abs(hours)));
            }
            else
            {
                Assert.That(task.OriginalEstimatedHours, Is.EqualTo(0));
                Assert.That(task.RevisedEstimatedHours, Is.EqualTo(0));
            }
        }

        [TestCase(10, 1, Result = 9),
         TestCase(1, 10, Result = 0),
         TestCase(-1, -10, Result = 0),
         TestCase(1, -10, Result = 1),
         TestCase(-1, 10, Result = 0)]
        public int TestLeafEstimatedRemaingHours(int totalHours, int hoursWorked)
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", totalHours);
            task.HoursWorked = hoursWorked;

            return task.EstimatedRemainingHours;
        }

        [TestCase(10, 1, Result = .1),
        TestCase(1, 10, Result = 10),
        TestCase(-1, -10, Result = 0),
        TestCase(1, -10, Result = 0),
        TestCase(-1, 10, Result = 1)]
        public double TestLeafPercentComplete(int totalHours, int hoursWorked)
        {
            LeafTask task = new LeafTask(0, "TASK", "Test Task", totalHours);
            task.HoursWorked = hoursWorked;

            return task.PercentComplete;
        }
    }
}
