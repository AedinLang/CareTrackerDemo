using System.Web;
using System.Web.Optimization;

namespace CareTrackerV1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundle code for full calender
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",        
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/highchartjs").Include(
                              "~/Scripts/Highcharts-4.0.1/js"));

            bundles.Add(new ScriptBundle("~/bundles/highchart").Include(
                              "~/Scripts/Highcharts-4.0.1/js/highcharts*"));

            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/bootstrap.css")); 
            
            //materialize css
            bundles.Add(new StyleBundle("~/materialize/css").Include(
                        "~/Content/materialize/css/materialize*"));
            bundles.Add(new StyleBundle("~/materialize/style").Include(
                       "~/Content/materialize/css/style.css"));

            //materialize script file
            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                              "~/Scripts/materialize/materialize.*"));

            //**MPC**Next 2 lines of code included after adding jQuery.UI.Combined Nuget package and using info in link
            //http://www.codeguru.com/csharp/.net/net_asp/a-jquery-ui-based-date-picker-for-asp.net-mvc-5.html
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include("~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/themes/base/all.css"));

            //Calendar css file
            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include(
                "~/Content/theme/jquery.ui.all.css",
                "~/Content/fullcalendar.css"));

            //Calendar script file
            bundles.Add(new ScriptBundle("~/bundles/fullcalendarjs").Include(
                "~/Scripts/jquery-ui-{version}.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/fullcalendar.min.js"));
        }
    }
}
