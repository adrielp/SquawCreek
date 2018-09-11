using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class TeeTimeBooking
    {
        public Guid Id { get; set; }
        public int NumberOfPlayers { get; set; }
        public DateTime ScheduledTime { get; set; }
        public bool Paid { get; set; }

        [Required]
        [EmailAddress]
        public string BookedBy { get; set; }
        public bool Member { get; set; }
        public bool Cart { get; set; }
    }
}
