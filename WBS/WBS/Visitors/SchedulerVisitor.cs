﻿using System;
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

        //Add all tasks that are less than a day into a single day
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

        private int FillInRemainingHours(LeafTask task, int hours)
        {
            int remainingHours = 8 - hoursFilled;
            hours -= remainingHours;
            hoursFilled = 0;

            WorkDay day = schedule.WorkDays[currentDay++];

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

            return hours;
        }

        private int FillInFullDay(LeafTask task, int hours)
        {
            WorkDay day;
            //if (schedule.WorkDays.Count >= currentDay)
                day = new WorkDay(currentDay++);
            //else
            //    day = schedule.WorkDays[currentDay];
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

                hours -= 8;
            }

            //if (schedule.WorkDays.Count > currentDay-1)
                schedule.WorkDays.Add(day);

            return hours;
        }

        private void FillInAllPossible(LeafTask task, int hours)
        {
            WorkDay day;
            //if (schedule.WorkDays.Count >= currentDay)
                day = new WorkDay(currentDay);
            //else
            //    day = schedule.WorkDays[currentDay];
            hoursFilled += hours;
            hours = 0;

            foreach (Engineer e in task.AssignedEngineers)
            {
                List<Task> tasks = new List<Task>();
                tasks.Add(task);
                day.AssignedTasks.Add(e, tasks);
            }
            //if (schedule.WorkDays.Count > currentDay - 1)
                schedule.WorkDays.Add(day);
        }
    }
}
