using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareTrackerV1.Models;

namespace CareTrackerV1.Controllers
{
    public class NextOfKinController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NextOfKin
        public ActionResult Index()
        {
            return View(db.NextOfKins.ToList());
        }

        // GET: NextOfKin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // GET: NextOfKin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextOfKin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextOfKinID,FirstName,Surname,AddressLine1,AddressLine2,AddressLine3,PhoneNumber,Mobile,Email")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.NextOfKins.Add(nextOfKin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextOfKin);
        }

        // GET: NextOfKin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // POST: NextOfKin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextOfKinID,FirstName,Surname,AddressLine1,AddressLine2,AddressLine3,PhoneNumber,Mobile,Email")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextOfKin);
        }

        // GET: NextOfKin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // POST: NextOfKin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            db.NextOfKins.Remove(nextOfKin);
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
