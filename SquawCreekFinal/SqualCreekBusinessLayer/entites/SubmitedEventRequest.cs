using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class SubmitedEventRequest
    {
        public System.Guid Id { get; set; }
        public DateTime RequestedTime { get; set; }
        public string EventDescription { get; set; }
        public int NumberOfGuests { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public decimal Budget { get; set; }

    }
}
