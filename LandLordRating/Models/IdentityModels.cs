using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LandLordRating.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<LandLordClaim> LandLordClaims { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }

        public int ClaimedLandLordId { get; set; }

        public ApplicationUser()
        {
            Ratings = new List<Rating>();
            LandLordClaims = new List<LandLordClaim>();
        }

        


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
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<LandLord> LandLords { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<LandLordClaim> LandLordClaims { get; set; }
        public DbSet<RatingReply> RatingReplies { get; set; }
        public DbSet<Flag> Flags { get; set; }
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>()
                .HasOptional(pi => pi.RatingReply)
                .WithRequired(lu => lu.Rating);

            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }
    }
}