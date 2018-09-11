using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class CalendarEvent
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublic { get; set; }
        public string EventTitle { get; set; }
        public string EventDiscription { get; set; }
        public string BookedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
