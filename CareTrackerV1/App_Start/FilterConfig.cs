using System.Web;
using System.Web.Mvc;

namespace CareTrackerV1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());           //CODE TO PREVENT BACK OPTION ON WEBPAGE, stackoverflow.com/questions/21930487/how-to-prevent-browser-back-button-after-logout
        }


    }
}
