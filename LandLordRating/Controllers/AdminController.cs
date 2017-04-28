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
using Microsoft.Owin.Security.OAuth;
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
                vm.PendingLandLordClaims = db.LandLordClaims.Count(u => u.IsPending);
                vm.PendingRatings = db.Ratings.Count(u => !u.IsApproved);
                vm.PendingRatingReplies = db.RatingReplies.Count(u => !u.IsApproved);
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

        public ActionResult ASDLKASLDK()
        {
            AddCurrentUserToAdminRole();
            return RedirectToAction("Index", "Admin");
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
        public ActionResult DeclinedLandLords(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!IsAdminUser())
                return RedirectToAction("Index", "Home");
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

            var landlordsdeclined = db.LandLords.Where(u => u.IsDeclined);

            if (!string.IsNullOrEmpty(searchString))
            {
                landlordsdeclined = landlordsdeclined.Where(s => s.FullName.Contains(searchString)
                                                 || s.City.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    landlordsdeclined = landlordsdeclined.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    landlordsdeclined = landlordsdeclined.OrderBy(s => s.OverallRating);
                    break;
                case "date_desc":
                    landlordsdeclined = landlordsdeclined.OrderByDescending(s => s.OverallRating);
                    break;
                default:  // Name ascending 
                    landlordsdeclined = landlordsdeclined.OrderBy(s => s.FullName);
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
            return View(landlordsdeclined.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult LandLordsAwaitingApproval(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
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

            var landlordapprovallist = db.LandLords.Where(u => u.IsApproved == false).Where(u => u.IsDeclined == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                landlordapprovallist = landlordapprovallist.Where(s => s.FullName.Contains(searchString)
                                                 || s.City.Contains(searchString));
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
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
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
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
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
            ViewBag.Message = "You have approved the landlord " + landLord.FullName +
                              ". This account is now live on the website.";
            db.Entry(landLord).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        [Authorize]
        public async Task<ActionResult> DeclineLandLord(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
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
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
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
            ViewBag.Message = "You have declined the landlord page " + landLord.FullName;
            return RedirectToAction("LandLordsAwaitingApproval", "Admin");
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
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
            LandLordViewModel vm = new LandLordViewModel();
            vm.LandLord = landLord;
            vm.Ratings = db.Ratings.Where(u => u.LandLordId == id).OrderBy(u => u.LandLordRating).ToPagedList(1, 10);
            return View(vm);
        }


        [Authorize]
        public ActionResult PendingLandLordClaims(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var claimlist = db.LandLordClaims.Where(u => u.IsPending).Where(u => !u.IsApproved);

            if (!string.IsNullOrEmpty(searchString))
            {
                claimlist = claimlist.Where(s => s.ClaimName.Contains(searchString)
                                                 || s.ClaimDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    claimlist = claimlist.OrderByDescending(s => s.ClaimName);
                    break;
                case "Date":
                    //Should be a date here, will add later and update the model as well.
                    claimlist = claimlist.OrderBy(s => s.ClaimDescription);
                    break;
                case "date_desc":
                    claimlist = claimlist.OrderByDescending(s => s.ClaimDescription);
                    break;
                default:  // Name ascending 
                    claimlist = claimlist.OrderBy(s => s.ClaimName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(claimlist.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public async Task<ActionResult> ViewLandLordClaim(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLordClaim claim = await db.LandLordClaims.FindAsync(id);
            if (claim == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            return View(claim);

        }

        [Authorize]
        public async Task<ActionResult> ApproveLandLordClaim(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLordClaim claim = await db.LandLordClaims.FindAsync(id);
            if (claim == null || !IsAdminUser() || claim.LandLord.IsClaimed)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        [Authorize]
        public async Task<ActionResult> ApproveLandLordClaimFinal(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLordClaim claim = await db.LandLordClaims.FindAsync(id);
            if (claim == null || !IsAdminUser() || claim.LandLord.IsClaimed || claim.ApplicationUser.ClaimedLandLordId != 0)
            {
                return HttpNotFound();
            }
            claim.IsApproved = true;
            claim.IsPending = false;
            var user = claim.ApplicationUser;
            var landlord = claim.LandLord;
            user.ClaimedLandLordId = claim.LandLord.LandLordId;
            landlord.IsClaimed = true;
            db.Entry(landlord).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;
            db.Entry(claim).State = EntityState.Modified;
            if (db.Ratings.Any(u => u.LandLord == landlord && u.User == user))
            {
                var rating = db.Ratings.SingleOrDefault(u => u.LandLord == landlord && u.User == user);
                db.Ratings.Remove(rating);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("PendingLandLordClaims");
        }


        [Authorize]
        public ActionResult PendingRatings(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var ratinglist = db.Ratings.Where(u => !u.IsApproved);

            if (!string.IsNullOrEmpty(searchString))
            {
                ratinglist = ratinglist.Where(s => s.RatingDescription.Contains(searchString)
                                                 || s.User.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ratinglist = ratinglist.OrderByDescending(s => s.RatingDescription);
                    break;
                case "Date":
                    //Should be a date here, will add later and update the model as well.
                    ratinglist = ratinglist.OrderBy(s => s.LandLordRating);
                    break;
                case "date_desc":
                    ratinglist = ratinglist.OrderByDescending(s => s.LandLordRating);
                    break;
                default:  // Name ascending 
                    ratinglist = ratinglist.OrderBy(s => s.RatingDescription);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ratinglist.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public async Task<ActionResult> ViewRating(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            return View(rating);

        }

        [Authorize]
        public async Task<ActionResult> ApproveRating(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null || !IsAdminUser() || rating.IsApproved)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        public async Task<ActionResult> ApproveRatingFinal(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null || !IsAdminUser() || rating.IsApproved)
            {
                return HttpNotFound();
            }
            rating.IsApproved = true;
            db.Entry(rating).State = EntityState.Modified;
            await db.SaveChangesAsync();
            ViewBag.Message = "The Rating for " + rating.LandLord.FullName + " was approved and is live.";
            return RedirectToAction("PendingRatings");
        }

        public ActionResult PendingRatingReplies(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var ratingreplylist = db.RatingReplies.Where(u => !u.IsApproved);

            if (!string.IsNullOrEmpty(searchString))
            {
                ratingreplylist = ratingreplylist.Where(s => s.ReplyDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    ratingreplylist = ratingreplylist.OrderByDescending(s => s.ReplyDescription);
                    break;
                case "Date":
                    //Should be a date here, will add later and update the model as well.
                    ratingreplylist = ratingreplylist.OrderBy(s => s.Rating.RatingName);
                    break;
                case "date_desc":
                    ratingreplylist = ratingreplylist.OrderByDescending(s => s.Rating.RatingName);
                    break;
                default:  // Name ascending 
                    ratingreplylist = ratingreplylist.OrderBy(s => s.ReplyDescription);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ratingreplylist.ToPagedList(pageNumber, pageSize));
        }


        [Authorize]
        public async Task<ActionResult> ViewRatingReply(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatingReply ratingreply = await db.RatingReplies.FindAsync(id);
            if (ratingreply == null || !IsAdminUser() || ratingreply.IsApproved)
            {
                return HttpNotFound();
            }
            return View(ratingreply);
        }

        [Authorize]
        public async Task<ActionResult> ApproveRatingReply(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatingReply ratingreply = await db.RatingReplies.FindAsync(id);
            if (ratingreply == null || !IsAdminUser() || ratingreply.IsApproved)
            {
                return HttpNotFound();
            }
            return View(ratingreply);
        }

        [Authorize]
        public async Task<ActionResult> ApproveRatingReplyFinal(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized", "LandLords");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatingReply ratingreply = await db.RatingReplies.FindAsync(id);
            if (ratingreply == null || !IsAdminUser() || ratingreply.IsApproved)
            {
                return HttpNotFound();
            }
            ratingreply.IsApproved = true;
            db.Entry(ratingreply).State = EntityState.Modified;
            await db.SaveChangesAsync();
            ViewBag.Message = "The Rating Reply for " + ratingreply.Rating.LandLord.FullName + " was approved and is live.";
            return RedirectToAction("PendingRatingReplies");
        }
    }
}