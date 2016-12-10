using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CareTrackerV1.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CareTrackerV1.Controllers
{

    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;
                //	ApplicationDbContext context = new ApplicationDbContext();
                //	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //var s=	UserManager.GetRoles(user.GetUserId());
                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                else if (User.IsInRole("Care Giver"))
                {
                    ViewBag.displayMenu = "Care Giver";
                    ViewBag.UserID = User.Identity.GetUserId();
                    var UserId = User.Identity.GetUserId();  //UserID loaded into CareGiver entity when new CareGiver is registered
                    var CareGiver = db.CareGivers;
                    foreach (var cg in CareGiver)
                    { if (cg.UserID==UserId)
                        {
                            ViewBag.CareGiverID = cg.ID;
                        }
                    }
                    //ViewBag.CareGiverID = db.CareGivers.Select(i => i.UserID == UserId);
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }


            return View();


        }
    }
}
