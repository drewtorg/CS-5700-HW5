using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class SchedulerVisitor : Visitor
    {
        private Schedule schedule;
        private int currentDay;
        private int hoursFilled;

        public Schedule GetEstimatedSchedule(Task task)
        {
            schedule = new Schedule() { WorkDays = new List<WorkDay>() };
            currentDay = 0;
            hoursFilled = 0;
            Visit((dynamic)task);
            return schedule;
        }
        
        protected override void Visit(ParallelParentTask task)
        {
            int startDay = currentDay;
            int lastDay = 0;

            foreach (Task t in task.Children)
            {
                Visit((dynamic)t);

                //act as if all tasks are starting on the same day
                if (currentDay > lastDay)
                    lastDay = currentDay;
                currentDay = startDay;
            }

            currentDay = lastDay;
        }

        protected override void Visit(LeafTask task)
        {
            int hours = task.EstimatedRemainingHours;

            if (hoursFilled != 0)
                hours = FillInRemainingHours(task, hours);

            while (hours >= 8)
                hours = FillInFullDay(task, hours);

            if (hours > 0)
                FillInAllPossible(task, hours);
        }

        //used when a task can fill the rest of a day
        private int FillInRemainingHours(LeafTask task, int hours)
        {
            int remainingHours = 8 - hoursFilled;
            hours -= remainingHours;
            hoursFilled = 0;

            WorkDay day = schedule.WorkDays[currentDay++];

            PopulateWorkDayWithEngineers(task, day);

            return hours;
        }

        //used when a task can fill a full day
        private int FillInFullDay(LeafTask task, int hours)
        {
            hours -= 8 * task.AssignedEngineers.Count;

            CreateWorkDayByTask(task);

            currentDay++;

            return hours;
        }

        //used when a task can't fill a day
        private void FillInAllPossible(LeafTask task, int hours)
        {
            hoursFilled += hours;
            hours = 0;

            CreateWorkDayByTask(task);
        }

        //creates a work day if it doesn't already exist and assigns engineers
        private void CreateWorkDayByTask(LeafTask task)
        {
            bool addDay = false;
            WorkDay day;
            day = schedule.WorkDays.Where(wd => wd.Day == currentDay).SingleOrDefault();

            if (day == null)
            {
                day = new WorkDay(currentDay);
                addDay = true;
            }

            PopulateWorkDayWithEngineers(task, day);
            
            if (addDay)
                schedule.WorkDays.Add(day);
        }

        //assigns task to an engineer in the given work day
        private void PopulateWorkDayWithEngineers(LeafTask task, WorkDay day)
        {
            foreach (Engineer e in task.AssignedEngineers)
            {
                if (!day.AssignedTasks.ContainsKey(e))
                {
                    List<Task> tasks = new List<Task>();
                    tasks.Add(task);
                    day.AssignedTasks.Add(e, tasks);
                }
                else
                {
                    day.AssignedTasks[e].Add(task);
                }
            }
        }
    }
}
