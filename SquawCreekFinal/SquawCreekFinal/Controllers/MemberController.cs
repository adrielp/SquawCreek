using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;

namespace SquawCreekFinal.Controllers
{
    public class MemberController : Controller
    {
        private ICommon m_commonBO = null;
        private IEvent m_eventBO = null;
        private IAdmin m_adminBO = null;

        public MemberController(ICommon commonBO, IEvent eventBO, IAdmin adminBO)
        {
            m_commonBO = commonBO;
            m_eventBO = eventBO;
            m_adminBO = adminBO;
        }
        public ActionResult Index()
        {
            return View("ClubHouse");
        }


    }
}