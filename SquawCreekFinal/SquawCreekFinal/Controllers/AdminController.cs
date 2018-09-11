using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;

namespace SquawCreekFinal.Controllers
{
    public class AdminController : Controller
    {

        private ICommon m_commonBO = null;
        private IEvent m_eventBO = null;
        private IAdmin m_adminBO = null;

        public AdminController(ICommon commonBO, IEvent eventBO, IAdmin adminBO)
        {
            m_commonBO = commonBO;
            m_eventBO = eventBO;
            m_adminBO = adminBO;
        }
        public ActionResult Index()
        {
            MgmtDailyPortal portal = m_adminBO.AdminPortalDisplay();
            return View("ManagementPortal", portal);
        }

        public ActionResult AddEvent()
        {
            EventInfo evt = new EventInfo();
            evt.StartDate = DateTime.Now;
            evt.EndDate = DateTime.Now.AddDays(1);
            return View(evt);
        }

        [HttpPost]
        public ActionResult AddEvent(EventInfo evt)
        {
            if (evt != null)
            {
                m_adminBO.ScheduleEvent(evt);
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditEvent(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                EventInfo evt = new EventInfo();
                evt = m_adminBO.EditEvent(id);
                return View(evt);
            }
            else
            {
                return View("Index");
            }
        }



        [HttpPost]
        public ActionResult EditEvent(EventInfo evt)
        {
            m_adminBO.UpdateEvent(evt);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveEvent(Guid id)
        {
            m_adminBO.RemoveEvent(id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveEventRequest(Guid id)
        {
            m_adminBO.RemoveEventRequest(id);
            return RedirectToAction("Index");

        }
    

        public ActionResult CreateEventFromRequest(Guid id)
        {
            EventInfo ev = m_adminBO.GetRequestInfo(id);
            ev.Approved = true;
            return View("FinalizeEvent", ev);
        }

        public ActionResult FinalizeEvent(EventInfo ev)
        {
            return View(ev);
        }


        public ActionResult ListShopItems
        {
            get
            {
                List<ShopItem> items = new List<ShopItem>();
                items = m_adminBO.GetListOfCurrentShopItems();
                return PartialView(items);
            }
        }

        public ActionResult UpdateShopItem(Guid id)
        {
            ShopItem item = m_adminBO.FindShopItem(id);
            return PartialView("UpdateShopItem", item);
        }

        [HttpPost]
        public ActionResult UpdateShopItem(ShopItem item)
        {
            m_adminBO.UpdateShopItem(item);
            return RedirectToAction("Index");
        }

        public ActionResult AddShopItem()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddShopItem(ShopItem item)
        {
            m_adminBO.AddShopItem(item);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveShopItem(Guid id)
        {
            m_adminBO.RemoveShopItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult CancelTeeTime(Guid id)
        {
            m_adminBO.CancelTeeTime(id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveUser(Guid id)
        {
            m_adminBO.RemoveUser(id);
            return RedirectToAction("Index");
        }

    }
}