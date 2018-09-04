using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sprint1AppDev3A.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("NewDeployDB6", throwIfV1Schema: false)//DBContext5
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<RFQ> rfq { get; set; }
        public DbSet<Quotes> quote { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Vm> vms { get; set; }
        public DbSet<RateUs> RateUs { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Freequote> Freequotes {get; set;}
        public DbSet<CalAmt> CalAmts { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Vat> Vats { get; set; }

        // public DbSet<Assign> Assigns { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.Assign> Assigns { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.AssignViewModel> AssignViewModels { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.DriverCheckIn> DriverCheckIns { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.NewTruck> NewTrucks { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.NewContainer> NewContainers { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.NewTrailer> NewTrailers { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.NewDriver> NewDrivers { get; set; }

        public System.Data.Entity.DbSet<Sprint1AppDev3A.Models.NewAssign> NewAssigns { get; set; }
    }
}