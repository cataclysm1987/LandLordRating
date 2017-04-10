using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LandLordRating.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace LandLordRating.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            if (IsAdminUser())
            {
                var vm = new AdminPanelViewModel();
                vm.Users = db.Users.Count();
                vm.Ratings = db.Ratings.Count();
                vm.LandLords = db.LandLords.Count();
                vm.LandLordsAwaitingApproval = db.LandLords.Count(u => u.IsApproved == false);
                vm.AwaitingApprovalList = db.LandLords.Where(u => u.IsApproved == false).ToPagedList(1, 10);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                for (int i = 0; i < s.Count; i++)
                {
                    if (s[i] == "Admin")
                    {
                        return true;
                    }
                }

                return false;
            }
            return false;
        }

        public void AddCurrentUserToAdminRole()
        {
            var userid = User.Identity.GetUserId();
            var currentuser = db.Users.FirstOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.AddToRole(currentuser.Id, "Admin");
        }

        public void RemoveCurrentUserFromAdminRole()
        {
            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var s = UserManager.GetRoles(user.GetUserId());
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == "Admin")
                {
                    s[i] = null;
                }
            }
        }
    }
}