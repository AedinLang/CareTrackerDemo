using CareTrackerV1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CareTrackerV1.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult GetVisitEvents()
        {
            var allVisits = db.Visits;
            var allVisitForCalendar = new List<Visit>();
            foreach (var calendarentry in allVisits)
            {
                allVisitForCalendar.Add(calendarentry);
            }

            var visitList = from v in allVisitForCalendar
                            select new
                            {
                                id = v.Client.AddressLine1+","+v.Client.AddressLine2,
                                title= v.CareGiver.FirstName + " " + v.CareGiver.Surname + " visit to " + v.Client.FirstName+" "+v.Client.Surname,
                                start = v.StartTime,
                                end = v.EndTime
                            };
            var rows = visitList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVisitEventsForCareGiver(int careGiverID)
        {
            var allVisits = db.Visits;
            var allVisitForCareGiverCalendar = new List<Visit>();
            foreach (var calendarentry in allVisits)
            {
                if (calendarentry.CareGiverID == careGiverID)

                {
                    allVisitForCareGiverCalendar.Add(calendarentry);
                }
            }

            var visitList = from v in allVisitForCareGiverCalendar
                            select new
                            {
                                id = v.Client.AddressLine1 + "," + v.Client.AddressLine2,
                                title = v.Client.FirstName + " " + v.Client.Surname,
                                start = v.StartTime,
                                end = v.EndTime,
                                someKey = v.CareGiverID
                            };
            var rows = visitList.ToArray();
            return (Json(rows, JsonRequestBehavior.AllowGet));
        }

        // Fill array for calendar view
        public List<Visit> Fill(int? careGiverID)
        {
            var allVisits = db.Visits;
            var allVisitsForCalendar = new List<Visit>();

            if (User.IsInRole("Care Giver"))
            {
                foreach (var calendarentry in allVisits)
                {
                    if (calendarentry.CareGiverID == careGiverID)

                    {
                        allVisitsForCalendar.Add(calendarentry);
                    }
                }
            }
            else
            {
                foreach (var calendarentry in allVisits)
                {
                    allVisitsForCalendar.Add(calendarentry);
                }
            }
            return (allVisitsForCalendar);
        }
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CareGiverIndex(int careGiverID)
        {
            ViewBag.CareGiverID = careGiverID;
            return View();
        }
    }
}