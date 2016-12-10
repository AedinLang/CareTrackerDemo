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
    public class VisitTaskController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VisitTask
        public ActionResult Index()
        {
            return View(db.VisitTasks.ToList());
        }

        // GET: VisitTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTask visitTask = db.VisitTasks.Find(id);
            if (visitTask == null)
            {
                return HttpNotFound();
            }
            return View(visitTask);
        }

        // GET: VisitTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VisitTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description")] VisitTask visitTask)
        {
            if (ModelState.IsValid)
            {
                db.VisitTasks.Add(visitTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visitTask);
        }

        // GET: VisitTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTask visitTask = db.VisitTasks.Find(id);
            if (visitTask == null)
            {
                return HttpNotFound();
            }
            return View(visitTask);
        }

        // POST: VisitTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] VisitTask visitTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitTask);
        }

        // GET: VisitTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTask visitTask = db.VisitTasks.Find(id);
            if (visitTask == null)
            {
                return HttpNotFound();
            }
            return View(visitTask);
        }

        // POST: VisitTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitTask visitTask = db.VisitTasks.Find(id);
            db.VisitTasks.Remove(visitTask);
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
