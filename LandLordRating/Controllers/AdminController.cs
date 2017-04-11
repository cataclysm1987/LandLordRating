using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
                vm.LandLordsAwaitingApproval = db.LandLords.Where(u=> u.IsDeclined == false).Count(u => u.IsApproved == false);
                vm.LandLordsDeclined = db.LandLords.Count(u => u.IsDeclined);
                return View(vm);
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

        [Authorize]
        public void AddCurrentUserToAdminRole()
        {
            var userid = User.Identity.GetUserId();
            var currentuser = db.Users.FirstOrDefault(u => u.Id == userid);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.AddToRole(currentuser.Id, "Admin");
        }

        [Authorize]
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

        [Authorize]
        public async Task<ActionResult> LandLordsAwaitingApproval(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var landlordapprovallist = db.LandLords.Where(u => u.IsApproved == false);
            
            if (!String.IsNullOrEmpty(searchString))
            {
                landlordapprovallist = landlordapprovallist.Where(s => s.FullName.Contains(searchString)
                                                 || s.City.Contains(searchString)
                                                 || s.State.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    landlordapprovallist = landlordapprovallist.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    landlordapprovallist = landlordapprovallist.OrderBy(s => s.OverallRating);
                    break;
                case "date_desc":
                    landlordapprovallist = landlordapprovallist.OrderByDescending(s => s.OverallRating);
                    break;
                default:  // Name ascending 
                    landlordapprovallist = landlordapprovallist.OrderBy(s => s.FullName);
                    break;
            }

            foreach (var landlord in db.LandLords)
            {
                var listofratings = db.Ratings.Where(u => u.LandLordId == landlord.LandLordId).Select(u => u.LandLordRating).ToList();
                if (listofratings.Count() != 0)
                {
                    double result = listofratings.Average();
                    landlord.OverallRating = result;
                }

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(landlordapprovallist.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public async Task<ActionResult> ApproveLandLord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            return View(landLord);
        }

        [Authorize]
        public async Task<ActionResult> ApproveLandLordFinal([Bind(Include = "LandLordId,FullName,PhoneNumber,City,State,IsApproved")] int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            landLord.IsApproved = true;
            db.Entry(landLord).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [Authorize]
        public async Task<ActionResult> DeclineLandLord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            return View(landLord);
        }

        [Authorize]
        public async Task<ActionResult> DeclineLandLordFinal([Bind(Include = "LandLordId,FullName,PhoneNumber,City,State,IsApproved")] int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            landLord.IsDeclined = true;
            if (landLord.IsApproved)
                landLord.IsApproved = false;
            db.Entry(landLord).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }
    }
}