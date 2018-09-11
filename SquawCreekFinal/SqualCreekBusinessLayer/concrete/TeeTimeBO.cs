using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;

namespace SqualCreekBusinessLayer.concrete
{
    public class TeeTimeBO : ITeeTime
    {
        public async Task<TeeTimesForDay> DailyTeeTimes(DateTime selected)
        {
            TeeTimesForDay ttd = new TeeTimesForDay();
            List<TeeTimesPerSlot> ttpsList = new List<TeeTimesPerSlot>();
            ttd.CurrentDate = selected;
            DateTime holder = new DateTime();
            //loop to create slots 
            for (int i = 0; i < 25; i++)
            {
                TimeSpan slotTime;
                
                if (i < 1)
                {
                    TeeTimesPerSlot ttps = new TeeTimesPerSlot();
                    slotTime = new TimeSpan(7, 0, 0);
                    ttps.ScheduleTime = selected.Date + slotTime;
                    holder = ttps.ScheduleTime;
                    ttps.Id = Guid.Empty;
                    ttps.Paid = false;
                    ttps.BookedBy = string.Empty;
                    ttps.Created = DateTime.Now;
                    ttps.NumberOfPlayers = 1;
                    ttps.Member = false;
                    ttps.Cart = false;
                    ttpsList.Add(ttps);
                    
                }
                else
                {
                    TeeTimesPerSlot ttps = new TeeTimesPerSlot();
                    holder = holder.AddMinutes(30);
                    ttps.ScheduleTime = holder;
                    holder = ttps.ScheduleTime;
                    ttps.Id = Guid.Empty;
                    ttps.Paid = false;
                    ttps.BookedBy = string.Empty;
                    ttps.Created = DateTime.Now;
                    ttps.NumberOfPlayers = 1;
                    ttps.Member = false;
                    ttps.Cart = false;
                    ttpsList.Add(ttps);
                }
            }

                //now you need to loop through and see what slots are taken
            List<TeeTimesPerSlot> FinalPull = new List<TeeTimesPerSlot>();
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                 foreach (TeeTimesPerSlot slot in ttpsList)
                    {
                    if (db.TeeTimes.Any(x => x.ScheduleTime.ToString() == slot.ScheduleTime.ToString()))
                    {
                        var item = db.TeeTimes.Where(x => x.ScheduleTime == slot.ScheduleTime).Select(x => new TeeTimesPerSlot
                        {
                            ScheduleTime = x.ScheduleTime.Value,
                            Id = x.Id,
                            Paid = x.Paid.Value,
                            BookedBy = x.BookedBy,
                            Created = x.Created.Value,
                            NumberOfPlayers = x.NumberOfPlayers.Value,
                            Member = x.Member.Value,
                            Cart = x.Cart.Value
                        }).FirstOrDefault();

                        FinalPull.Add(item);
                    }
                    else
                    {
                        FinalPull.Add(slot);
                    }
                }   
            }
            ttd.SelectableTimes = GatherDates(DateTime.Now);
            ttd.TeeTimesBySlot = FinalPull;
            return ttd;
        }

        public List<DateTime> GatherDates(DateTime Now)
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now.AddDays(10);
            for(var date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }

        public void BookTeeTime(TeeTimeBooking teetime)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                TeeTime tt = new TeeTime();
                tt.BookedBy = teetime.BookedBy;
                tt.Cart = teetime.Cart;
                tt.Id = Guid.NewGuid();
                tt.Member = teetime.Member;
                tt.NumberOfPlayers = teetime.NumberOfPlayers;
                tt.Paid = false;
                tt.ScheduleTime = teetime.ScheduledTime;
                tt.Created = DateTime.Now;

                db.TeeTimes.Add(tt);
                db.SaveChanges();

            }

        }


    }
}
