using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBS
{
    public class Engineer
    {
        private int hoursAvailable;

        public int ID { get; set; }
        public string Name { get; set; }
        public int HoursAvailable
        {
            get { return hoursAvailable; }
            set { hoursAvailable = Math.Min(Math.Max(value, 0), 24); }
        }

        public override bool Equals(object obj)
        {
            Engineer other = obj as Engineer;
            return ID == other.ID && Name.Equals(other.Name) && HoursAvailable == other.HoursAvailable;
        }
    }
}
