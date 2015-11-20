using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS
{
    public class Engineer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Dictionary<DayOfWeek, int> Availability { get; set; }

    }
}
