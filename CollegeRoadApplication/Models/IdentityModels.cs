using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CollegeRoadApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }

        //[RegularExpression(@"^(?:male|Male|female|Female)$", ErrorMessage = "Please enter Male or Female")]
        public string Gender { get; set; }

        public string ContactNumber { get; set; }

        public int Age { get; set; }

        public bool isAllowedToSwim { get; set; }

        public bool IsArchived { get; set; }

        [Display(Name = "Family Group")]
        public int? FamilyGroupId { get; set; } // Foregin Key

        public FamilyGroup FamilyGroup { get; set; } // Single Nav reference
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PoolSizeType> PoolSizeTypes { get; set; }

        public DbSet<SwimmingMeet> SwimmingMeets { get; set; }

        public DbSet<SwimmingEvent> SwimmingEvents { get; set; }

        public DbSet<Lane> Lanes { get; set; }

        public DbSet<FamilyGroup> FamilyGroups { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}