using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS
{
    public enum DayOfWeek { Monday, Tuesday, Wednesday, Thursday, Friday }

    public class Scheduler
    {
        public Dictionary<int, List<Engineer>> Schedule { get; set; }
        public Dictionary<Engineer, List<Task>> TaskList { get; set; }
    }
}
