using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;


namespace SqualCreekBusinessLayer.concrete
{
    public class Event : IEvent
    {
        public void SendEventRequest(SqualCreekBusinessLayer.entites.SubmitedEventRequest ser)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                EventRequest evReq = new EventRequest();
                evReq.Id = Guid.NewGuid();
                evReq.RequestedTime = ser.RequestedTime.ToString();
                evReq.EventDescription = ser.EventDescription;
                evReq.NumberOfGuests = ser.NumberOfGuests;
                evReq.ContactFirstName = ser.ContactFirstName;
                evReq.ContactLastName = ser.ContactLastName;
                evReq.ContactEmail = ser.ContactEmail;
                evReq.ContactPhone = ser.ContactPhone;
                evReq.Budget = ser.Budget;
                evReq.Approved = false;
                db.EventRequests.Add(evReq);
                db.SaveChanges();
            }

        }


        public string CalenderPopulationPublic()
        {
            string json = "";
            JArray data = new JArray();

            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                DateTime searchStart = DateTime.Now.AddDays(-30);
                List<CalendarEvent> events = new List<CalendarEvent>();
                events = db.Events.Where(x => x.StartDate >= searchStart).Select( x => new CalendarEvent {
                    Id = x.Id,
                    StartDate = x.StartDate.Value,
                    EndDate = x.EndDate.Value,
                    IsPublic = x.IsPublic.Value,
                    EventTitle = x.EventTitle,
                    EventDiscription = x.EventDescription,
                    BookedBy = x.BookedBy,
                    Created = x.Created.Value
                }).ToList();

                foreach (CalendarEvent singleEvent in events)
                {
                    JObject single = new JObject( new JProperty("title", singleEvent.EventTitle),
                    new JProperty("start", singleEvent.StartDate.ToString()),
                    new JProperty("end", singleEvent.EndDate.ToString())
                    );

                    data.Add(single);
                }


            }


            json = data.ToString();   
  
            return json;

        }
    }
}
