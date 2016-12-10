using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareTrackerV1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            //One to one relationship with CareGiver Table
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;

        }

        //public int CareGiverID { get; set; }

        //[ForeignKey("CareGiverID")]
        //public virtual CareGiver CareGiver { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection"/*, throwIfV1Schema: false*/)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<CareGiver> CareGivers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitTask> VisitTasks { get; set; }
        public DbSet<File> Files { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         //without this line of code get errors. Got this off Stack overflow - what does it mean??
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Client>()
                .HasMany(c => c.CareGivers).WithMany(i => i.Clients)
                .Map(t => t.MapLeftKey("ClientID")
                    .MapRightKey("CareGiverID")
                    .ToTable("ClientCareGiver"));
        }
    }
}