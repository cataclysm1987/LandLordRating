using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LandLordRating.Models;
using Microsoft.AspNet.Identity;
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


            var landlords = from s in db.LandLords
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                landlords = landlords.Where(s => s.FullName.Contains(searchString)
                                                 || s.City.Contains(searchString)
                                                 || s.State.Contains(searchString));
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

            int pageSize = 3;
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
            if (landLord == null)
            {
                return HttpNotFound();
            }
            return View(landLord);
        }

        // GET: LandLords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LandLords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LandLordId,FullName,PhoneNumber,City,State")] LandLord landLord)
        {
            if (ModelState.IsValid)
            {
                db.LandLords.Add(landLord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(landLord);
        }

        // GET: LandLords/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            return View(landLord);
        }

        // POST: LandLords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LandLordId,FullName,PhoneNumber,City,State")] LandLord landLord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landLord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(landLord);
        }

        // GET: LandLords/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        //View a LandLord
        public async Task<ActionResult> ViewLandLord(int? id)
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
            LandLordViewModel vm = new LandLordViewModel();
            vm.LandLord = landLord;
            vm.Ratings = db.Ratings.ToPagedList(1, 10);
            return View(vm);
        }


        //Create a Rating on a LandLord 

        public async Task<ActionResult> CreateRating(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandLord landLord = await db.LandLords.FindAsync(id);
            ViewBag.LandLord = landLord;
            if (landLord == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRating([Bind(Include = "RatingId,RatingName,RatingDescription,PropertyRating,LandLordRating,SafetyRating,CommunicationRating,RateAnonymously,User_Id")] Rating rating)
        {
            var userid = User.Identity.GetUserId();
            rating.User = db.Users.FirstOrDefault(u => u.Id == userid);
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "LandLords");
        }

    }

    
}
