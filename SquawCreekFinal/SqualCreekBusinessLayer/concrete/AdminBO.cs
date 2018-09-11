using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqualCreekBusinessLayer.infacing;
using SqualCreekBusinessLayer.entites;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Security;

namespace SqualCreekBusinessLayer.concrete
{
    public class AdminBO : IAdmin
    {
        public MgmtDailyPortal AdminPortalDisplay()
        {
            MgmtDailyPortal portal = new MgmtDailyPortal();
            DateTime today = DateTime.Today;
            portal.TodaysDate = today;
            DateTime buffer = DateTime.Today.AddDays(1);

            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                portal.TeeTimes = db.TeeTimes.Where(x => x.ScheduleTime >= today && x.ScheduleTime < buffer).Select(x => new TeeTimesPerSlot {
                    Id = x.Id,
                    ScheduleTime = x.ScheduleTime.Value,
                    Paid = x.Paid.Value,
                    BookedBy = x.BookedBy,
                    NumberOfPlayers = x.NumberOfPlayers.Value,
                    Member = x.Member.Value,
                    Cart = x.Cart.Value,
                    Created = x.Created.Value
                }).ToList();

                portal.TeeTimesCount = portal.TeeTimes.Count();

                portal.Events = db.Events.Where(x => x.StartDate >= today && x.StartDate < buffer).Select(x => new CalendarEvent
                {
                    Id = x.Id,
                    StartDate = x.StartDate.Value,
                    EndDate = x.EndDate.Value,
                    IsPublic = x.IsPublic.Value,
                    EventTitle = x.EventTitle,
                    EventDiscription = x.EventDescription,
                    BookedBy = x.BookedBy,
                    Created = x.Created.Value
                }).ToList();

                portal.ShopItems = db.ShopItems.Select(x => new entites.ShopItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    Price = (double)x.Price,
                    InStock = x.InStock.Value
                }).ToList();

                portal.EventRequests = db.EventRequests.Where(x => x.Approved == false).ToList();

                portal.EventsCount = portal.Events.Count();

                portal.Users = db.AspNetUsers.ToList();

            }

                return portal;
        }

        public void ScheduleEvent(EventInfo evt)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                
                SqualCreekBusinessLayer.Event evtSave = new SqualCreekBusinessLayer.Event();
                evtSave.BookedBy = string.Format("{0} {1}", evt.ContactFirstName, evt.ContactLastName);
                evtSave.Id = Guid.NewGuid();
                evtSave.IsPublic = evt.IsPublic;
                evtSave.EventTitle = evt.EventTitle;
                evtSave.EventDescription = evt.EventDescription;
                evtSave.StartDate = evt.StartDate;
                evtSave.EndDate = evt.EndDate;
                evtSave.Created = DateTime.Now;
                db.Events.Add(evtSave);
                db.SaveChanges();

                if (db.EventRequests.Where(x => x.Id == evt.EventRequestID).Any())
                {
                    var currentRequest = db.EventRequests.Where(x => x.Id == evt.EventRequestID).FirstOrDefault();
                    currentRequest.Approved = true;
                    currentRequest.ApprovedEventId = evtSave.Id;
                    db.SaveChanges();
                }
                else
                {
                    EventRequest evtRequest = new EventRequest();
                    evtRequest.Approved = true;
                    evtRequest.Budget = 999;
                    evtRequest.RequestedTime = evt.StartDate.ToString();
                    evtRequest.ContactEmail = evt.ContactEmail;
                    evtRequest.ContactFirstName = evt.ContactFirstName;
                    evtRequest.ContactLastName = evt.ContactLastName;
                    evtRequest.ContactPhone = evt.ContactPhone;
                    evtRequest.EventDescription = evt.EventDescription;
                    evtRequest.NumberOfGuests = evt.NumberOfGuests;
                    evtRequest.Id = Guid.NewGuid();
                    evtRequest.ApprovedEventId = evtSave.Id;

                        db.EventRequests.Add(evtRequest);
                        db.SaveChanges();
                }           
            }
        }

        public EventInfo EditEvent(Guid id)
        {
            EventInfo evt = new EventInfo();

            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var eventPartOne = db.Events.Where(x => x.Id == id).FirstOrDefault();
                var eventPartTwo = db.EventRequests.Where(x => x.ApprovedEventId == id).FirstOrDefault();

                evt.Id = id;
                evt.ContactFirstName = eventPartTwo.ContactFirstName;
                evt.ContactLastName = eventPartTwo.ContactLastName;
                evt.ContactPhone = eventPartTwo.ContactPhone;
                evt.EventTitle = eventPartOne.EventTitle;
                evt.NumberOfGuests = eventPartTwo.NumberOfGuests.Value;
                evt.EventDescription = eventPartTwo.EventDescription;
                evt.ContactEmail = eventPartTwo.ContactEmail;
                evt.StartDate = eventPartOne.StartDate.Value;
                evt.EndDate = eventPartOne.EndDate.Value;
                evt.IsPublic = eventPartOne.IsPublic.Value;

                return evt;
            }

        }

        public void UpdateEvent(EventInfo evt)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var eventPartOne = db.Events.Where(x => x.Id == evt.Id).FirstOrDefault();
                var eventPartTwo = db.EventRequests.Where(x => x.ApprovedEventId == evt.Id).FirstOrDefault();

                eventPartOne.BookedBy = string.Format("{0} {1}", evt.ContactFirstName, evt.ContactLastName);
                eventPartOne.IsPublic = evt.IsPublic;
                eventPartOne.EventTitle = evt.EventTitle;
                eventPartOne.EventDescription = evt.EventDescription;
                eventPartOne.StartDate = evt.StartDate;
                eventPartOne.EndDate = evt.EndDate;

                eventPartTwo.RequestedTime = evt.StartDate.ToString();
                eventPartTwo.ContactEmail = evt.ContactEmail;
                eventPartTwo.ContactFirstName = evt.ContactFirstName;
                eventPartTwo.ContactLastName = evt.ContactLastName;
                eventPartTwo.ContactPhone = evt.ContactPhone;
                eventPartTwo.EventDescription = evt.EventDescription;
                eventPartTwo.NumberOfGuests = evt.NumberOfGuests;

                db.SaveChanges();
            }
        }

        public void RemoveEvent(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var eventPartOne = db.Events.Where(x => x.Id == id).FirstOrDefault();
                var eventPartTwo = db.EventRequests.Where(x => x.ApprovedEventId == id).FirstOrDefault();

                if (eventPartOne != null)
                {   
                    db.Events.Remove(eventPartOne);
                }
                if (eventPartTwo != null)
                {
                    db.EventRequests.Remove(eventPartTwo);
                }
                db.SaveChanges();

            }
        }

        public void RemoveEventRequest(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var badRequest = db.EventRequests.Where(x => x.Id == id).FirstOrDefault();
                db.EventRequests.Remove(badRequest);
                db.SaveChanges();
            }
        }
        


        public EventInfo GetRequestInfo(Guid id)
        {
            EventInfo evt = new EventInfo();

            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {

                var eventPartTwo = db.EventRequests.Where(x => x.Id == id).FirstOrDefault();

                evt.Id = Guid.NewGuid();
                evt.EventRequestID = id;
                evt.ContactFirstName = eventPartTwo.ContactFirstName;
                evt.ContactLastName = eventPartTwo.ContactLastName;
                evt.ContactPhone = eventPartTwo.ContactPhone;
               // evt.EventTitle = eventPartOne.EventTitle;
                evt.NumberOfGuests = eventPartTwo.NumberOfGuests.Value;
                evt.EventDescription = eventPartTwo.EventDescription;
                evt.ContactEmail = eventPartTwo.ContactEmail;
                evt.StartDate = DateTime.Parse(eventPartTwo.RequestedTime);
                evt.EndDate = DateTime.Parse(eventPartTwo.RequestedTime);
                // evt.StartDate = eventPartOne.StartDate.Value;
                //evt.EndDate = eventPartOne.EndDate.Value;
                // evt.IsPublic = eventPartOne.IsPublic.Value;
                return evt;
            }

        }

        public List<entites.ShopItem> GetListOfCurrentShopItems()
        {
            List<entites.ShopItem> items = new List<entites.ShopItem>();
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                items = db.ShopItems.Select(x => new entites.ShopItem {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    Price = (double)x.Price,
                    InStock = x.InStock.Value
                }).ToList();
            }
                return items;
        }

        public entites.ShopItem FindShopItem(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                entites.ShopItem item = db.ShopItems.Where(x => x.Id == id).Select(x => new entites.ShopItem {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    Price = (double)x.Price,
                    InStock = x.InStock.Value
                }).FirstOrDefault();

                return item;
            }
        }

        public void UpdateShopItem(entites.ShopItem item)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var current = db.ShopItems.Where(x => x.Id == item.Id).FirstOrDefault();
                current.Title = item.Title;
                current.Price = (decimal)item.Price;
                current.ImageURL = item.ImageURL;
                current.InStock = item.InStock;
                current.Description = item.Description;
                db.SaveChanges();
            }
        }

        public void AddShopItem(entites.ShopItem item)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                ShopItem adding = new ShopItem();
                adding.Id = Guid.NewGuid();
                adding.Title = item.Title;
                adding.Description = item.Description;
                adding.ImageURL = item.ImageURL;
                adding.Price = (decimal)item.Price;
                adding.InStock = item.InStock;
                db.ShopItems.Add(adding);
                db.SaveChanges();
            }
        }

        public void RemoveShopItem(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var remove = db.ShopItems.Where(x => x.Id == id).FirstOrDefault();
                db.ShopItems.Remove(remove);
                db.SaveChanges();
            }
        }

        public void CancelTeeTime(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var removeTime = db.TeeTimes.Where(x => x.Id == id).FirstOrDefault();
                db.TeeTimes.Remove(removeTime);
                db.SaveChanges();
            }
        }

        public void RemoveUser(Guid id)
        {
            using (SquawCreekDeVryEntities db = new SquawCreekDeVryEntities())
            {
                var user = db.AspNetUsers.Where(x => x.Id == id.ToString()).FirstOrDefault();
                if (user != null)
                {
                    db.AspNetUsers.Remove(user);
                    db.SaveChanges();
                }
                
            }
        }

    }
}
