using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.entites
{
    public class MgmtDailyPortal
    {
        public DateTime TodaysDate { get; set; }
        public int TeeTimesCount { get; set; }
        public int EventsCount { get; set; }
        public List<CalendarEvent> Events { get; set; }
        

        public List<TeeTimesPerSlot> TeeTimes { get; set; }
        public List<AspNetUser> Users { get; set; }

        public List<EventRequest> EventRequests { get; set; }

        public List<entites.ShopItem> ShopItems { get; set; }
    }
}
