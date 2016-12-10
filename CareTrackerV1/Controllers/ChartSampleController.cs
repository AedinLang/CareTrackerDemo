using CareTrackerV1.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareTrackerV1.Controllers
{
    public class ChartSampleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChartSample
        public ActionResult Index()
        {
            var transactionCounts = new List<TransactionCount>
            {
                new TransactionCount() {MonthName="January",Count=30 },
                new TransactionCount() {MonthName="February",Count=40 },
                new TransactionCount() {MonthName="March",Count=4 },
                new TransactionCount() {MonthName="April",Count=35 }
            };

            var xDataMonths = transactionCounts.Select(i => i.MonthName).ToArray();
            var yDataCounts = transactionCounts.Select(i => new object[] { i.Count }).ToArray();

            var chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                .SetTitle(new Title { Text = "Incoming Transactions per Hour" })
                .SetSubtitle(new Subtitle { Text = "Accounting" })
                .SetXAxis(new XAxis { Categories = xDataMonths })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Number of Transactions" } })
                .SetTooltip(new Tooltip
                {
                    Enabled = true,
                    Formatter = @"function() { return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y;}"
                })
                .SetPlotOptions(new PlotOptions
                {
                    Line = new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Enabled = true
                        },
                        EnableMouseTracking = false
                    }
                })
                .SetSeries(new[]
                {
                    new Series {Name = "Hour", Data = new Data(yDataCounts) }
                });

            return View(chart);
           
        }

        public ActionResult Chart1()
        {
            //Collect data into arrays
            var xDataCareGiver = db.CareGivers.Select(i => i.Surname).ToArray();
            var yDataNumberOfClients = db.Clients.Select(i => new object[] { i.Region }).ToArray(); //required logic to count clients*/.ToArray();

            //create a chart 
            var chart = new Highcharts("chart1")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Scatter })
                .SetTitle(new Title { Text = "Client Number Per CareGiver" })
                .SetXAxis(new XAxis { Categories = xDataCareGiver })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Number of Clients" } })
                //.SetTooltip(new Tooltip
                //{
                //    Enabled = true,
                //    Formatter = @"function() {return '<b>'+ this.series.name +'</b><br/>'+ this.x': '+ this.y;
                //})
                .SetSeries(new[]
                {
                    new Series {Data=new Data(yDataNumberOfClients) }
                });

            return View(chart);
        }

    }
}