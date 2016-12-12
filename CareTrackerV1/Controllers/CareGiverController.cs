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
using DotNet.Highcharts;
using CareTrackerV1.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System.Drawing;

namespace CareTrackerV1.Controllers
{
    public class CareGiverController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index(int? id, int? clientID)
        {
            var viewModel = new CareGiverIndexData();

            viewModel.CareGivers = db.CareGivers
                .Include(i => i.Clients)
                .Include(i => i.Visits)
                .OrderBy(i => i.Surname);



            if (id != null)
            {
                ViewBag.CareGiverID = id.Value;
                viewModel.Clients = viewModel.CareGivers.
                    Where(i => i.ID == id.Value).Single().Clients;

            }

            if (clientID != null)
            {
                ViewBag.ClientID = clientID.Value;

                viewModel.Visits = from v in db.Visits
                                   where
                                   (v.ClientID == clientID)
                                   where
                                   (v.CareGiverID == id.Value)
                                   select v;
            }

            return View(viewModel);

        }

        public ActionResult Chart1()
        {
            //X axis data
            var xData = db.CareGivers.Select(i => i.ID.ToString()).ToArray();

            //Collect y axis data
            var xDataID = db.CareGivers.Select(i => i.ID).ToArray();

            int noOfCareGivers = xData.Length;
            List<Object> totalH = new List<Object>();
            List<Object> totalV = new List<Object>();

            for (int i = 0; i < noOfCareGivers; i++)
            {
                int ID = xDataID[i];
                TimeSpan? totalHours = new TimeSpan(0, 0, 0, 0);
                int count = 0;
                foreach (var j in db.Visits)
                {
                    if (j.CareGiverID == ID)
                    {
                        TimeSpan? visitLength = j.EndTime - j.StartTime;
                        totalHours += visitLength.Value;
                        count += 1;
                    }
                    

                }
                object total = totalHours.Value.TotalHours;
                totalH.Add(total);
                totalV.Add(count);

            }
            var yDataHours = totalH.Select(i => new Object[] { i }).ToArray();
            var yDataVisits = totalV.Select(i => new Object[] { i }).ToArray();

            //create a chart 
            var chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "Hours worked by CareGiver" })
                .SetSubtitle(new Subtitle { Text = "Source: CareTracker db" })
                .SetXAxis(new XAxis
                {
                    Categories = xData,
                    Title = new XAxisTitle { Text = "Care Giver ID" },
                    Labels = new XAxisLabels
                    {
                        //Rotation = -45,
                        Align = HorizontalAligns.Right,
                        Style = "fontSize: '13px',fontFamily: 'Verdana, sans-serif'"
                    }
                })
                .SetYAxis(new YAxis {
                    Title = new YAxisTitle { Text = "Number of hours/visits" }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    VerticalAlign = VerticalAligns.Top,
                    Align = HorizontalAligns.Left,
                    Shadow = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    Floating = true
                })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Enabled = true
                        },
                        EnableMouseTracking = false
                    }
                })
                .SetSeries(new[]
            {
                new Series {Name = "Total Hours", Data = new Data(yDataHours) },
                new Series {Name = "Total Visits", Data = new Data(yDataVisits) }
            });

            return View(chart);

        }
    

        public ActionResult NonAdminIndex(int? id, int? clientID)
        {

            var viewModel = new CareGiverIndexData();

            viewModel.Clients = db.Clients
                .Include(i => i.CareGivers)
                .Include(i => i.Visits);

            if (id != null)
            {
                ViewBag.ClientID = clientID.Value;
                viewModel.Clients = viewModel.CareGivers.
                    Where(i => i.ID == id.Value).Single().Clients;
            }

            if (clientID != null)
            {
                ViewBag.ClientID = clientID.Value;

                viewModel.Visits = from v in db.Visits
                                   where
                                   (v.ClientID == clientID)
                                   where
                                   (v.CareGiverID == id.Value)
                                   select v;
            }


            return View(viewModel);

        }

        // GET: CareGiver/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareGiver careGiver = db.CareGivers.Include(c => c.Files).SingleOrDefault(c => c.ID == id);
            if (careGiver == null)
            {
                return HttpNotFound();
            }
            return View(careGiver);
        }

        // GET: CareGiver/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CareGiver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,Surname,AddressLine1,AddressLine2,Region,Email,PhoneNumber,Mobile,Qualifications,CV,References")] CareGiver careGiver, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photoID = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.PhotoID,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        photoID.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    careGiver.Files = new List<File> { photoID };
                }
                db.CareGivers.Add(careGiver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(careGiver);
        }

        // GET: CareGiver/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareGiver careGiver = db.CareGivers.Include(c => c.Files).SingleOrDefault(c => c.ID == id);
            if (careGiver == null)
            {
                return HttpNotFound();
            }

            var allClientsList = db.Clients.ToList();
            careGiver.AllClients = allClientsList.Select(o => new SelectListItem
            {
                Text = o.FirstName + " " + o.Surname,
                Value = o.ID.ToString()
            });
            PopulateCareGiverClientData(careGiver);
            return View(careGiver);
        }

        private void PopulateCareGiverClientData(CareGiver careGiver)
        {
            var allClients = db.Clients;
            var clients = new HashSet<int>(careGiver.Clients.Select(c => c.ID));
            var viewModel = new List<CareGiverClientData>();
            foreach (var client in allClients)
            {
                viewModel.Add(new CareGiverClientData
                {
                    ClientID = client.ID,
                    Selected = clients.Contains(client.ID)
                });
            }
            ViewBag.Clients = viewModel;
        }

        // POST: CareGiver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedClients, HttpPostedFileBase upload)
        {

            var careGiverToUpdate = db.CareGivers
                .Include(i => i.Clients)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(careGiverToUpdate, "",
      new string[] { "FirstName", "Surname", "AddressLine1", "AddressLine2", "Region", "Email", "PhoneNumber", "Mobile", "Qualifications", "CV", "References", "UserID" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (careGiverToUpdate.Files.Any(f => f.FileType == FileType.PhotoID))
                        {
                            db.Files.Remove(careGiverToUpdate.Files.First(f => f.FileType == FileType.PhotoID));
                        }
                        var photoID = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.PhotoID,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            photoID.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        careGiverToUpdate.Files = new List<File> { photoID };
                    }

                    UpdateCareGiver(selectedClients, careGiverToUpdate);

                    db.Entry(careGiverToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateCareGiverClientData(careGiverToUpdate);
            return View(careGiverToUpdate);
        }

        private void UpdateCareGiver(string[] selectedClients, CareGiver careGiverToUpdate)
        {
            if (selectedClients == null)
            {
                careGiverToUpdate.Clients = new List<Client>();
                return;
            }

            var selectedClientsHS = new HashSet<string>(selectedClients);
            var careGiverClients = new HashSet<int>(careGiverToUpdate.Clients.Select(t => t.ID));
            foreach (var client in db.Clients)
            {
                if (selectedClientsHS.Contains(client.ID.ToString()))
                {
                    if (!careGiverClients.Contains(client.ID))
                    {
                        careGiverToUpdate.Clients.Add(client);
                    }
                }
                else
                {
                    if (careGiverClients.Contains(client.ID))
                    {
                        careGiverToUpdate.Clients.Remove(client);
                    }
                }
            }
        }

        // GET: CareGiver/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CareGiver careGiver = db.CareGivers.Find(id);
            if (careGiver == null)
            {
                return HttpNotFound();
            }
            return View(careGiver);
        }

        // POST: CareGiver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CareGiver careGiver = db.CareGivers.Find(id);
            db.CareGivers.Remove(careGiver);
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
