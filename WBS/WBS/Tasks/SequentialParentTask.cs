﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WBS;

namespace WBS.Tasks
{
    public class SequentialParentTask : ParentTask
    {
        public SequentialParentTask(int id, string label, string description) : base(id, label, description) { }
    }
}
