using SqualCreekBusinessLayer.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqualCreekBusinessLayer.infacing
{
    public interface ITeeTime
    {
        Task<TeeTimesForDay> DailyTeeTimes(DateTime date);
        List<DateTime> GatherDates(DateTime now);
        void BookTeeTime(TeeTimeBooking teetime);
    }
}
