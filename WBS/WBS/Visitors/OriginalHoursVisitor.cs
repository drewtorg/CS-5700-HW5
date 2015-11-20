﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS.Visitors
{
    public class OriginalHoursVisitor : Visitor
    {
        private int hours;

        public int GetOriginalHours(Task task)
        {
            hours = 0;
            Visit((dynamic) task);
            return hours;
        }

        protected override void Visit(LeafTask task)
        {
            hours += task.OriginalEstimatedHours;
        }
    }
}
