using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LandLordRating.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace LandLordRating.Controllers
{
    public class LandLordsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandLords
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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

            var landlords = db.LandLords.Where(u => u.IsApproved);
            if (!String.IsNullOrEmpty(searchString))
            {
                landlords = landlords.Where(s => s.FullName.Contains(searchString)
                                                 || s.City.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    landlords = landlords.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    landlords = landlords.OrderBy(s => s.OverallRating);
                    break;
                case "date_desc":
                    landlords = landlords.OrderByDescending(s => s.OverallRating);
                    break;
                default:  // Name ascending 
                    landlords = landlords.OrderBy(s => s.FullName);
                    break;
            }

            foreach (var landlord in db.LandLords)
            {
                var listofratings = db.Ratings.Where(u => u.LandLordId == landlord.LandLordId).Select(u=>u.LandLordRating).ToList();
                if (listofratings.Count() != 0)
                {
                    double result = listofratings.Average();
                    landlord.OverallRating = result;
                }
                
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(landlords.ToPagedList(pageNumber, pageSize));
        }

        // GET: LandLords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null || landLord.IsApproved == false)
            {
                return HttpNotFound();
            }
            LandLordViewModel vm = new LandLordViewModel();
            vm.LandLord = landLord;
            vm.Ratings = db.Ratings.Where(u => u.LandLordId == id && u.IsApproved).OrderBy(u => u.LandLordRating).ToPagedList(1, 10);
            var userid = GetCurrentUser().Id;
            vm.IsClaimingUser = db.Users.Any(u => u.ClaimedLandLordId == landLord.LandLordId && u.Id == userid);

            return View(vm);
        }

        // GET: LandLords/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ViewUserRatings()
        {
            var userid = User.Identity.GetUserId();
            var ratings = db.Ratings.Where(u => u.User.Id == userid).OrderBy(u=>u.RatingName).ToPagedList(1, 10);
            return View(ratings);
        }

        // POST: LandLords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LandLordId,FullName,PhoneNumber,City,State,IsDeclined,IsApproved")] LandLord landLord)
        {
            if (IsAdminUser())
            {
                landLord.IsApproved = true;
                landLord.IsDeclined = false;
            }
            else
            {
                landLord.IsApproved = false;
                landLord.IsDeclined = false;
            }
            if (ModelState.IsValid)
            {
                db.LandLords.Add(landLord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(landLord);
        }

        // GET: LandLords/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            var currentuser = GetCurrentUser();
            if (currentuser.ClaimedLandLordId != landLord.LandLordId)
                return RedirectToAction("Unauthorized");

            if (landLord == null || !IsAdminUser())
            {
                return HttpNotFound();
            }
            return View(landLord);
        }

        // POST: LandLords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "LandLordId,FullName,PhoneNumber,City,State,IsApproved")] LandLord landLord)
        {
            var currentuser = GetCurrentUser();
            if (IsAdminUser() || currentuser.ClaimedLandLordId == landLord.LandLordId)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(landLord).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
        return

        View(landLord);
        }

        // GET: LandLords/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!IsAdminUser())
                return RedirectToAction("Unauthorized");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            if (landLord == null)
            {
                return HttpNotFound();
            }
            return View(landLord);
        }

        // POST: LandLords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LandLord landLord = await db.LandLords.FindAsync(id);
            db.LandLords.Remove(landLord);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //Create a Rating on a LandLord 

        [Authorize]
        public async Task<ActionResult> CreateRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            
            if (landLord == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = landLord.FullName;
            var user = GetCurrentUser();
            if (user.ClaimedLandLordId == id)
                return RedirectToAction("Unauthorized");
            var userid = user.Id;
            var alreadyrated = db.Ratings.Where(u => u.User.Id == userid).Any(u => u.LandLordId == id);
            if (alreadyrated)
            {
                return RedirectToAction("AlreadyCreated", "LandLords");
            }
            Rating r = new Rating();
            r.LandLordId = (int) id;
            return View(r);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CreateRating([Bind(Include = "RatingId,RatingName,LateFees,LandLordNotice,LandLordResponse,ContactPhoneNumer,RecommendLandLord,RentIncrease,WrittenLease,LandLordRating,RateAnonymously,User_Id,LandLordId,RatingDescription,IsApproved,IsDeclined")] Rating rating)
        {
            rating.User = GetCurrentUser();
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "LandLords");
        }

        public async Task<ActionResult> ViewRatingsForLandLord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);

            if (landLord == null)
            {
                return HttpNotFound();
            }
            var ratinglist = db.Ratings.Where(u => u.LandLordId == id).OrderBy(u=>u.LandLordRating).ToPagedList(1, 10);
            ViewBag.Message = "Viewing Ratings for " + landLord.FullName;
            return View(ratinglist);
        }

        public ActionResult ViewRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);

            if (rating == null || !rating.IsApproved)
            {
                return HttpNotFound();
            }
            RatingViewModel vm = new RatingViewModel();
            vm.Rating = rating;
            var landlordid = rating.LandLordId;
            vm.IsClaimingUser = db.Users.Any(u => u.ClaimedLandLordId == landlordid);
            return View(vm);
        }

        public ActionResult AlreadyCreated()
        {
            return View();
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
        public ActionResult ClaimLandLord(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landlord = db.LandLords.Find(id);
            
            if (landlord == null)
            {
                return HttpNotFound();
            }
            var user = GetCurrentUser();
            if (user.ClaimedLandLordId != 0)
                return RedirectToAction("AlreadyClaimed");
            if (landlord.IsClaimed)
                return RedirectToAction("LandLordAlreadyClaimed", new {id=landlord.LandLordId});


            ClaimLandLordViewModel vm = new ClaimLandLordViewModel();
            vm.LandLordName = landlord.FullName;
            TempData["landlordid"] = id;
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ClaimLandLord([Bind(Include = "Id,ClaimName,ClaimDescription,DocumentFilePath,IsPending,IsApproved,ApplicationUser_Id,LandLord_LandLordId")] ClaimLandLordViewModel vm, HttpPostedFileBase document)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                vm.Document = file;
            }

            var landlordid = TempData["landlordid"];
            var landlord = await db.LandLords.FindAsync(landlordid);
            if (landlord != null)
                vm.LandLord = landlord;

            //Manually changed to test all values, redirect to view if not valid. Was previously ModelState.IsValid but was broken and couldn't figure out why it wouldn't find the document.
            if (vm.Document != null && vm.ClaimName != null && vm.ClaimDescription != null && vm.LandLord != null)
            {
                ApplicationUser currentUser = GetCurrentUser();
                var landLordClaim = new LandLordClaim();
                var docPath = Path.Combine(Server.MapPath("~/uploads"), vm.Document.FileName);
                vm.Document.SaveAs(docPath);
                landLordClaim.DocumentFilePath = docPath;
                landLordClaim.ClaimName = vm.ClaimName;
                landLordClaim.ClaimDescription = vm.ClaimDescription;
                landLordClaim.ApplicationUser = currentUser;
                landLordClaim.LandLord = vm.LandLord;
                db.LandLordClaims.Add(landLordClaim);
                await db.SaveChangesAsync();
                return RedirectToAction("ClaimSubmitted");
            }
            ViewBag.Message = "Error. Please fill out all values.";
            return View(vm);
        }

        public ActionResult ClaimSubmitted(LandLord landlord)
        {
            if (landlord == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        //Action used when user already has claimed a landlord on their account
        public ActionResult AlreadyClaimed()
        {
            return View();
        }

        //Action used when a landlord has already been claimed by another user
        public ActionResult LandLordAlreadyClaimed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landlord = db.LandLords.Find(id);

            if (landlord == null)
            {
                return HttpNotFound();
            }
            return View(landlord);
        }

        [Authorize]
        public ActionResult UploadProfileImage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landlord = db.LandLords.Find(id);

            if (landlord == null)
            {
                return HttpNotFound();
            }

            var user = GetCurrentUser();
            TempData["landlordid"] = id;
            if (user.ClaimedLandLordId == landlord.LandLordId)
                return View(landlord);
            return RedirectToAction("Unauthorized");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(HttpPostedFileBase image)
        {
            int landlordid = (int)TempData["landlordid"];
            var landlord = db.LandLords.FirstOrDefault(u => u.LandLordId == landlordid);
            if (image != null && HasImageExtension(image.FileName))
            {
                string pic = System.IO.Path.GetFileName(image.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/img/profile"), pic);
                
                // file is uploaded
                image.SaveAs(path);
               
                landlord.ProfileImageUrl = path;
                db.Entry(landlord).State = EntityState.Modified;
                await db.SaveChangesAsync();

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    await image.InputStream.CopyToAsync(ms);
                //    //commented out as currently not being used
                //    //byte[] array = ms.GetBuffer();
                //}
                return RedirectToAction("Details", new {id = landlord.LandLordId});
            }
            ViewBag.Message = "Error: You must submit a valid image file. A file can either be a .gif, .jpg or .png.";
            return View(landlord.LandLordId);


        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ApplicationUser GetCurrentUser()
        {
            var userid = User.Identity.GetUserId();
            return db.Users.FirstOrDefault(u => u.Id == userid);
        }

        public static bool HasImageExtension(string source)
        {
            return (source.EndsWith(".png") || source.EndsWith(".jpg") || source.EndsWith(".gif"));
        }

        public ActionResult ReplyToRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);

            if (rating == null)
            {
                return HttpNotFound();
            }
            var user = GetCurrentUser();
            if (user.ClaimedLandLordId != rating.LandLordId || rating.RatingReply != null)
            {
                return RedirectToAction("Unauthorized");
            }
            RatingReplyViewModel vm = new RatingReplyViewModel();
            vm.Rating = rating;
            vm.RatingReply = new RatingReply();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReplyToRating(RatingReplyViewModel vm)
        {
            if (vm.RatingReply.ReplyDescription != null && vm.RatingReply.ReplyDescription.Length <= 2000)
            {
                vm.RatingReply.Rating = vm.Rating;
                RatingReply rr = new RatingReply();
                rr.ReplyDescription = vm.RatingReply.ReplyDescription;
                db.RatingReplies.Add(rr);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "LandLords", new {id=vm.Rating.LandLordId});
            }
            ViewBag.Message = "Error. Please enter a valid description.";
            return View(vm);
        }
    }

    
}
