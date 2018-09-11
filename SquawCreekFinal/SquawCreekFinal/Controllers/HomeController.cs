using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.entites;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.concrete;

namespace SquawCreekFinal.Controllers
{
    public class HomeController : Controller
    {

        private ICommon m_commonBO = null;
        private IEvent m_eventBO = null;
        private IAdmin m_adminBO = null;

        public HomeController(ICommon commonBO, IEvent eventBO, IAdmin adminBO)
        {
            m_commonBO = commonBO;
            m_eventBO = eventBO;
            m_adminBO = adminBO;
        }


        public ActionResult Index()
        {
            if (!User.IsInRole("Member") && !User.IsInRole("Admin"))
            {
                PublicModel model = new PublicModel();
                model.ShopItems = m_adminBO.GetListOfCurrentShopItems();
                return View(model);
            }
            else if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index", "Member");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            //if we got this far just take them to the home page
            return View();
        }

        public ActionResult ManagementPortal()
        {
            return RedirectToAction("Admin", "Index");
        }

        public ActionResult ClubHouse()
        {
            return View();
        }

        public ActionResult MailAddition(string email)
        {
            Common process = new Common();
            process.EmailAddition(email);
            return RedirectToAction("EmailAdded");
        }

        public ActionResult EmailAdded()
        {
            return View();
        }

        public ActionResult Public()
        {
            //public landing page  you will need to gather the partial views 
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactPartial()
        {
            return PartialView();
        }

        public ActionResult Tournament()
        {
            return PartialView();
        }

        public ActionResult GolfCoursePartial()
        {
            return PartialView();
        }

        public ActionResult GolfCourseRatesPartial()
        {
            return PartialView();
        }

        public ActionResult PhotoGalleryPartial()
        {
            return PartialView();
        }

        public ActionResult EventsViewPartial()
        {
            return PartialView();
        }

        public ActionResult EventsRequestPartialForm()
        {
            //perhaps you should make this a whole view for now but will leave as is till week 3
            return PartialView();
        }

        public ActionResult ShopArea()
        {
            List<ShopItem> model = m_adminBO.GetListOfCurrentShopItems();
            return PartialView(model);
        }

        public ActionResult NewsPartial()
        {
            // you will need to pass data to this at some point
            return PartialView();
        }

        public ActionResult JoinEmailNewsPartial()
        {
            return PartialView();
        }



    }
}