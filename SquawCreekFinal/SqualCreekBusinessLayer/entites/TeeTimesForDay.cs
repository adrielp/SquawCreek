using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class TeeTimesForDay
    {
        public int DayId { get; set; }
        public DateTime CurrentDate { get; set; }
        public List<TeeTimesPerSlot> TeeTimesBySlot { get; set; }
        public List<DateTime> SelectableTimes { get; set; }
    }
}
