using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class EventInfo
    {
        public Guid Id { get; set; }
        public bool Approved { get; set; }
        public string EventDescription { get; set; }
        public int NumberOfGuests { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }

        public Guid EventRequestID { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublic { get; set; }
        public string EventTitle { get; set; }
        public string BookedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
