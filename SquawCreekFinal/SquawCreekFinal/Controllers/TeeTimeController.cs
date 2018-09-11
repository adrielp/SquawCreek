using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.entites;
using SqualCreekBusinessLayer.infacing;
using System.Threading.Tasks;

namespace SquawCreekFinal.Controllers
{
    public class TeeTimeController : Controller
    {
        private ITeeTime m_teeTimeBO = null;

        public TeeTimeController(ITeeTime teeTimeBO)
        {
            m_teeTimeBO = teeTimeBO;
        }
        public async Task<ActionResult> ScheduleTeeTime()
        {
            //We want to build the model to pass to the data object
            TeeTimesForDay ttd = new TeeTimesForDay();
            ttd = await m_teeTimeBO.DailyTeeTimes(DateTime.Now);
            return View(ttd);
        }

        public ActionResult TeeTimeScheduler()
        {
            TeeTimesForDay ttd = new TeeTimesForDay();
            ttd.SelectableTimes = m_teeTimeBO.GatherDates(DateTime.Now);
            return PartialView(ttd);
        }

        public ActionResult PickDateOfTeeTime()
        {
            return PartialView();
        }

     

        public ActionResult ScheduleATeeTime()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> ScheduleATeeTime(DateTime date)
        {
            TeeTimesForDay ttdy = new TeeTimesForDay();
            ttdy = await m_teeTimeBO.DailyTeeTimes(date);
            return PartialView(ttdy);
        }

        public ActionResult ScheduleSlot(string date)
        {
            TeeTimeBooking ttb = new TeeTimeBooking();
            ttb.ScheduledTime = DateTime.Parse(date);
            return View(ttb);
        }

        [HttpPost]
        public ActionResult ScheduleSlot(TeeTimeBooking teetime)
        {
           
                m_teeTimeBO.BookTeeTime(teetime);
            return View("BookSuccess");
            
            
        }



    }
}