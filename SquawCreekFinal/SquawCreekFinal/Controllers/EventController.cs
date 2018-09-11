using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;

namespace SquawCreekFinal.Controllers
{
    public class EventController : Controller
    {

        private ICommon m_commonBO = null;
        private IEvent m_eventBO = null;

        public EventController(ICommon commonBO, IEvent eventBO)
        {
            m_commonBO = commonBO;
            m_eventBO = eventBO;
        }

        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostEventRequest(PublicModel model)
        {  if (model != null)
            {
                try
                {
                    m_eventBO.SendEventRequest(model.SubEventRequest);
                    return View("RequestSuccess");
                }
                catch(Exception ex)
                {
                    return View("error");
                }
                
            }
            return View("error");
        }


        //non members get event data
        public string EventData()
        {
            string jsondata = m_eventBO.CalenderPopulationPublic();
            return jsondata;
        }
    }
}