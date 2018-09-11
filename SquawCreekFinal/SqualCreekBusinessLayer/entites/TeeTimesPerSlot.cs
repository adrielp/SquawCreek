using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class TeeTimesPerSlot
    {
        public Guid Id { get; set; }
        public DateTime ScheduleTime { get; set; }
        public bool Paid { get; set; }
        public string BookedBy { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool Member { get; set; }
        public bool Cart { get; set; }
    }
}
