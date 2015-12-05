using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WBS.Tasks;

namespace WBS
{
    public class Schedule : IEnumerable<WorkDay>
    {
        public List<WorkDay> WorkDays { get; set; }

        public IEnumerator<WorkDay> GetEnumerator()
        {
            return ((IEnumerable<WorkDay>)WorkDays).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<WorkDay>)WorkDays).GetEnumerator();
        }
    }
}
