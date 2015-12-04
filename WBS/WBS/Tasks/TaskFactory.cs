using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS.Tasks
{
    public static class TaskFactory
    {
        private static int count = 0;

        public static ParentTask Create(Type type, string label, string description)
        {
            if (type == typeof(ParallelParentTask))
                return new ParallelParentTask(count++, label, description);

            if (type == typeof(SequentialParentTask))
                return new SequentialParentTask(count++, label, description);

            return null;
        }

        public static LeafTask Create(string label, string description, int estimatedHours)
        {
            return new LeafTask(count++, label, description, estimatedHours);
        }
    }
}
