using Microsoft.Owin;
using Owin;
using CareTrackerV1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(CareTrackerV1.Startup))]
namespace CareTrackerV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // Create default User roles and Admin user for login
        private void createRolesandUsers()
        {
               
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Create Admin Role and a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // Create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create default Admin user				

                var user = new ApplicationUser();
                user.UserName = "aedin";
                user.Email = "aedin@gmail.com";

                string userPWD = "123Aedin#";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // Create CareGiver role 
            if (!roleManager.RoleExists("Care Giver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Care Giver";
                roleManager.Create(role);

            }

            // Create Next Of Kin role 
            if (!roleManager.RoleExists("Next Of Kin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Next Of Kin";
                roleManager.Create(role);

            }
        }
    }
}
