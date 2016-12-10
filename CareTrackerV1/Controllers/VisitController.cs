using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareTrackerV1.Models;
using CareTrackerV1.ViewModel;

namespace CareTrackerV1.Controllers
{
    public class VisitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Visit
        public ActionResult Index()
        {
            var visits = db.Visits.Include(c => c.CareGiver).Include(cl => cl.Client); // Including CareGiver and Client allows eager loading of names etc
            //return View(visits.ToList());
            return View(db.Visits.ToList());
        }

        public ActionResult CareGiverClientVisitIndex(int? id)
        {
            return View(db.Visits.ToList().Where(i => i.ClientID == id));
        }

        // GET: Visit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // GET: Visit/Create
        public ActionResult Create()
        {
            ViewBag.CareGiverID = new SelectList(db.CareGivers, "ID", "FirstName");
            Visit visit = new Visit();
            ShowViewTaskData(visit);       //Gives checkbox options
            return View(visit);

        }

        private void ShowViewTaskData(Visit visit)                //Modified PopulateViewTaskData to remove 'pre-existing' tasks
        {
            var allVisitTasks = db.VisitTasks;
            var visitTasks = new HashSet<int>();
            var viewModel = new List<VisitTaskData>();
            foreach (var task in allVisitTasks)
            {
                viewModel.Add(new VisitTaskData
                {
                    VisitID = task.ID,
                    Description = task.Description,
                    Selected = visitTasks.Contains(task.ID)
                });
            }
            ViewBag.VisitTasks = viewModel;
        }

        // POST: Visit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(FormCollection formCollection, string[] selectedTasks)
        {

            var visitToCreate = new Visit();

            if (TryUpdateModel(visitToCreate, "",
      new string[] { "StartTime", "EndTime","Date", "CareGiverID", "ClientID", "Details", "AlertType", "AlertDetails" }))
            {
                try
                {
                    CreateVisitTasks(selectedTasks, visitToCreate);

                    db.Entry(visitToCreate).State = EntityState.Added;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                    ModelState.AddModelError("", "Code not working");
                }
            }
            ShowViewTaskData(visitToCreate);
            return View(visitToCreate);
        }

        private void CreateVisitTasks(string[] selectedTasks, Visit visitToCreate)
        {
            if (selectedTasks == null)
            {
                visitToCreate.VisitTasks = new List<VisitTask>();
                return;
            }

            var selectedTasksHS = new HashSet<string>(selectedTasks);
            var visitVisitTasks = new HashSet<int>(visitToCreate.VisitTasks.Select(t => t.ID));
            foreach (var visitTask in db.VisitTasks)
            {
                if (selectedTasksHS.Contains(visitTask.ID.ToString()))
                {
                    if (!visitVisitTasks.Contains(visitTask.ID))
                    {
                        visitToCreate.VisitTasks.Add(visitTask);
                    }
                }
                else
                {
                    if (visitVisitTasks.Contains(visitTask.ID))
                    {
                        visitToCreate.VisitTasks.Remove(visitTask);
                    }
                }
            }
        }

        // GET: Visit/Edit/5
        public ActionResult Edit(int? id)
        {
            
            Visit visit = db.Visits
                .Include(i => i.CareGiver)
                .Include(i => i.VisitTasks)
                .Where(i => i.ID == id)
                .Single();
            PopulateViewTaskData(visit);
            return View(visit);
        }

        private void PopulateViewTaskData(Visit visit)
        {
            var allVisitTasks = db.VisitTasks;
            var visitTasks = new HashSet<int>(visit.VisitTasks.Select(t => t.ID));
            var viewModel = new List<VisitTaskData>();
            foreach (var task in allVisitTasks)
            {
                viewModel.Add(new VisitTaskData
                {
                    VisitID = task.ID,
                    Description = task.Description,
                    Selected = visitTasks.Contains(task.ID)
                });
            }
            ViewBag.VisitTasks = viewModel;
        }

        // POST: Visit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedTasks)
        {
            
            var visitToUpdate = db.Visits
                .Include(i => i.CareGiver)
                .Include(i => i.VisitTasks)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(visitToUpdate, "",
      new string[] { "StartTime", "EndTime", "Date", "CareGiverID", "ClientID", "Details", "AlertType", "AlertDetails" }))
            {
                try
                {
                    UpdateVisitTasks(selectedTasks, visitToUpdate);

                    db.Entry(visitToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateViewTaskData(visitToUpdate);
            return View(visitToUpdate);
        }

        private void UpdateVisitTasks(string[] selectedTasks, Visit visitToUpdate)
        {
            if (selectedTasks == null)
            {
                visitToUpdate.VisitTasks = new List<VisitTask>();
                return;
            }

            var selectedTasksHS = new HashSet<string>(selectedTasks);
            var visitVisitTasks = new HashSet<int>(visitToUpdate.VisitTasks.Select(t => t.ID));
            foreach (var visitTask in db.VisitTasks)
            {
                if (selectedTasksHS.Contains(visitTask.ID.ToString()))
                {
                    if (!visitVisitTasks.Contains(visitTask.ID))
                    {
                        visitToUpdate.VisitTasks.Add(visitTask);
                    }
                }
                else
                {
                    if (visitVisitTasks.Contains(visitTask.ID))
                    {
                        visitToUpdate.VisitTasks.Remove(visitTask);
                    }
                }
            }
        }
        // GET: Visit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visit visit = db.Visits.Find(id);
            db.Visits.Remove(visit);
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
