using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS
{
    public class EngineerFactory
    {
        private static int count = 0;

        public static Engineer Create(string name, int hoursAvailable)
        {
            return new Engineer() { ID = count++, Name = name, HoursAvailable = hoursAvailable };
        }
    }
}
