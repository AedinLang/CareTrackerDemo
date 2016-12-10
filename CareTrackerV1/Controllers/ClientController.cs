using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareTrackerV1.Models;
using CareTrackerV1.Controllers;
using Microsoft.AspNet.Identity;
using CareTrackerV1.ViewModel;
using PagedList;

namespace CareTrackerV1.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*// GET: Client
        //[Authorize(Roles = "Admin, CareGiver, NextOfKin")]
        public ActionResult Index()
        {
           
            return View(db.Clients.ToList());

        }*/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var clients = from c in db.Clients
                           select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => (s.Surname.Contains(searchString) || s.FirstName.Contains(searchString)));
            }

            switch (sortOrder)
            {
                case "_desc":
                    clients = clients.OrderByDescending(s => s.Surname);
                    break;
                case "Date":
                    clients = clients.OrderBy(s => s.DOB);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(s => s.DOB);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Client for CareGiver
        //[Authorize(Roles = "Admin, CareGiver, NextOfKin")]
        public ActionResult CareGiverIndex(string userID)
        {

            var viewModel = new ClientVisitUserData();

            viewModel.Clients = db.Clients.
                        Include(i => i.Visits).
                        Include(i => i.CareGivers);

            var UserID = userID;
            viewModel.Clients = db.CareGivers.
                                Where(i => i.UserID == userID).
                                Single().Clients;

            return View(viewModel);
        }

        // GET: Client for CareGiver
        //[Authorize(Roles = "Admin, CareGiver, NextOfKin")]
        public ActionResult NonAdminIndex(int? id)
        {
            var viewModel = new ClientVisitUserData();

            viewModel.Clients = db.Clients.
                        Include(i => i.Visits);

           /*viewModel.Clients = from v in db.Visits
                                   where
                                   (v.ClientID == id)
                                   where
                                   (v.CareGiverID == id.Value)
                                   select v;*/

            if (id != null)
            {
                ViewBag.ClientID = id.Value;
                viewModel.Visits = viewModel.Clients.
                    Where(i => i.ID == id.Value).Single().Visits;
            }

            return View(viewModel);
        }


        // GET: Client/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        //[Authorize(Roles = "Admin, CareGiver")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin, CareGiver")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,Surname,DOB,AddressLine1,AddressLine2,Region,PhoneNumber,Medication,HealthSummary,DoctorID,NextOfKinID")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Client/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,Surname,DOB,AddressLine1,AddressLine2,Region,PhoneNumber,Medication,HealthSummary,DoctorID,NextOfKinID")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
