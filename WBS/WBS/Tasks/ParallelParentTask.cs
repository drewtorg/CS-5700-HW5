using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WBS;

namespace WBS.Tasks
{
    public class ParallelParentTask : ParentTask
    {
        public ParallelParentTask(int id, string label, string description) : base(id, label, description) { }
    }
}
