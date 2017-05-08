using System.Data.Entity;
using System.Linq;
using FluentScheduler;
using LandLordRating.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LandLordRating.Startup))]

namespace LandLordRating
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            JobManager.Initialize(new MyRegistry());
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole {Name = "Admin"};
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    UserName = "premworkouts@gmail.com",
                    Email = "premworkouts@gmail.com"
                };

                string userPWD = "Winter89!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole {Name = "User"};
                roleManager.Create(role);

            }
        }
    }

    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<UpdateAverageRatings>().ToRunEvery(1).Days().At(3, 0);
            //Commented out to not schedule this every 1 minute, use only to update current overall ratings for landlords if not updated recently.
            //Schedule<UpdateAverageRatings>().ToRunEvery(1).Minutes();
        }
        
    }

    public class UpdateAverageRatings : IJob
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Execute()
        {
            foreach (var landlord in db.LandLords)
            {
                var listofratings = db.Ratings.Where(u => u.LandLordId == landlord.LandLordId & u.IsApproved).Select(u => u.LandLordRating).ToList();
                if (listofratings.Count() != 0)
                {
                    double result = listofratings.Average();
                    landlord.OverallRating = result;
                }
                db.Entry(landlord).State = EntityState.Modified;
                
            }
            db.SaveChanges();
        }
    }
}
