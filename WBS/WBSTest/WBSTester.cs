using System;
using System.IO;

using WBS;
using WBS.Tasks;
using NUnit.Framework;

namespace WBSTest
{
    [TestFixture]
    public class WBSTester
    {
        const string PATH = "test.txt";

        [TestCase]
        public void TestPrintOutline()
        {
            string expected = @"Work Breakdown Structure Outline

Sequential Task - TSK - Test
	Task - Leaf1 - Test
	  Estimated Hours: 4
	  Percent Complete: 0%
	  Hours Remaining: 4
	  Hours Worked: 0
	  Days to Complete: 1
	  Engineers:
		E1
	Task - Leaf2 - Test
	  Estimated Hours: 12
	  Percent Complete: 0%
	  Hours Remaining: 12
	  Hours Worked: 0
	  Days to Complete: 2
	  Engineers:
		E2
		E3
	Task - Leaf3 - Test
	  Estimated Hours: 2
	  Percent Complete: 0%
	  Hours Remaining: 2
	  Hours Worked: 0
	  Days to Complete: 1
	  Engineers:
		E4
".Replace("\n", "").Replace("\r", "");

            WorkBreakdownStructure wbs = new WorkBreakdownStructure();
            ParentTask seq = TaskFactory.Create(typeof(SequentialParentTask), "TSK", "Test");
            LeafTask leaf1 = TaskFactory.Create("Leaf1", "Test", 4);
            LeafTask leaf2 = TaskFactory.Create("Leaf2", "Test", 12);
            LeafTask leaf3 = TaskFactory.Create("Leaf3", "Test", 2);
            Engineer e1 = EngineerFactory.Create("E1", 4);
            Engineer e2 = EngineerFactory.Create("E2", 4);
            Engineer e3 = EngineerFactory.Create("E3", 4);
            Engineer e4 = EngineerFactory.Create("E4", 4);

            leaf1.AddEngineer(e1);
            leaf2.AddEngineer(e2);
            leaf2.AddEngineer(e3);
            leaf3.AddEngineer(e4);

            seq.AddTask(leaf1);
            seq.AddTask(leaf2);
            seq.AddTask(leaf3);

            wbs.InitialTask = seq;

            wbs.Print("test.txt");

            Assert.That(File.Exists(PATH), Is.True);
            Assert.That(File.ReadAllText(PATH).Replace("\n", "").Replace("\r", ""), Is.EqualTo(expected));
            File.Delete(PATH);
        }

        [TestCase]
        public void TestCreateScheduleSequential()
        {
            WorkBreakdownStructure wbs = new WorkBreakdownStructure();
            ParentTask seq = TaskFactory.Create(typeof(SequentialParentTask), "TSK", "Test");
            LeafTask leaf1 = TaskFactory.Create("Leaf1", "Test", 4);
            LeafTask leaf2 = TaskFactory.Create("Leaf2", "Test", 12);
            LeafTask leaf3 = TaskFactory.Create("Leaf3", "Test", 2);
            Engineer e1 = EngineerFactory.Create("E1", 4);
            Engineer e2 = EngineerFactory.Create("E2", 4);
            Engineer e3 = EngineerFactory.Create("E3", 4);
            Engineer e4 = EngineerFactory.Create("E4", 4);

            leaf1.AddEngineer(e1);
            leaf2.AddEngineer(e2);
            leaf2.AddEngineer(e3);
            leaf3.AddEngineer(e4);

            seq.AddTask(leaf1);
            seq.AddTask(leaf2);
            seq.AddTask(leaf3);

            wbs.InitialTask = seq;

            Schedule estimate = wbs.CreateEstimatedSchedule();

            Assert.That(estimate.WorkDays.Count, Is.EqualTo(3));

            Assert.That(estimate.WorkDays[0].Day, Is.EqualTo(0));
            Assert.That(estimate.WorkDays[1].Day, Is.EqualTo(1));
            Assert.That(estimate.WorkDays[2].Day, Is.EqualTo(2));

            Assert.That(estimate.WorkDays[0].ScheduledWorkers.Count, Is.EqualTo(3));
            Assert.That(estimate.WorkDays[1].ScheduledWorkers.Count, Is.EqualTo(2));
            Assert.That(estimate.WorkDays[2].ScheduledWorkers.Count, Is.EqualTo(1));

            Assert.That(estimate.WorkDays[0].AssignedTasks[e1][0], Is.EqualTo(leaf1));
            Assert.That(estimate.WorkDays[0].AssignedTasks[e2][0], Is.EqualTo(leaf2));
            Assert.That(estimate.WorkDays[0].AssignedTasks[e3][0], Is.EqualTo(leaf2));

            Assert.That(estimate.WorkDays[1].AssignedTasks[e2][0], Is.EqualTo(leaf2));
            Assert.That(estimate.WorkDays[1].AssignedTasks[e3][0], Is.EqualTo(leaf2));

            Assert.That(estimate.WorkDays[2].AssignedTasks[e4][0], Is.EqualTo(leaf3));
        }

        [TestCase]
        public void TestCreateScheduleParallel()
        {
            WorkBreakdownStructure wbs = new WorkBreakdownStructure();
            ParentTask seq = TaskFactory.Create(typeof(ParallelParentTask), "TSK", "Test");
            LeafTask leaf1 = TaskFactory.Create("Leaf1", "Test", 4);
            LeafTask leaf2 = TaskFactory.Create("Leaf2", "Test", 12);
            LeafTask leaf3 = TaskFactory.Create("Leaf3", "Test", 2);
            Engineer e1 = EngineerFactory.Create("E1", 4);
            Engineer e2 = EngineerFactory.Create("E2", 4);
            Engineer e3 = EngineerFactory.Create("E3", 4);
            Engineer e4 = EngineerFactory.Create("E4", 4);

            leaf1.AddEngineer(e1);
            leaf2.AddEngineer(e2);
            leaf2.AddEngineer(e3);
            leaf3.AddEngineer(e4);

            seq.AddTask(leaf1);
            seq.AddTask(leaf2);
            seq.AddTask(leaf3);

            wbs.InitialTask = seq;

            Schedule estimate = wbs.CreateEstimatedSchedule();

            Assert.That(estimate.WorkDays.Count, Is.EqualTo(2));

            Assert.That(estimate.WorkDays[0].Day, Is.EqualTo(0));
            Assert.That(estimate.WorkDays[1].Day, Is.EqualTo(1));

            Assert.That(estimate.WorkDays[0].ScheduledWorkers.Count, Is.EqualTo(4));
            Assert.That(estimate.WorkDays[1].ScheduledWorkers.Count, Is.EqualTo(2));

            Assert.That(estimate.WorkDays[0].AssignedTasks[e1][0], Is.EqualTo(leaf1));
            Assert.That(estimate.WorkDays[0].AssignedTasks[e2][0], Is.EqualTo(leaf2));
            Assert.That(estimate.WorkDays[0].AssignedTasks[e3][0], Is.EqualTo(leaf2));
            Assert.That(estimate.WorkDays[0].AssignedTasks[e4][0], Is.EqualTo(leaf3));

            Assert.That(estimate.WorkDays[1].AssignedTasks[e2][0], Is.EqualTo(leaf2));
            Assert.That(estimate.WorkDays[1].AssignedTasks[e3][0], Is.EqualTo(leaf2));
            
        }
    }
}
