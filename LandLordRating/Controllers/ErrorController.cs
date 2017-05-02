using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LandLordRating.Controllers
{
    public class ErrorController : Controller
    {
        //Add error handlers here once site is live!
        //Currently not working, still goes to old error page. Have to figure out why!
        [HandleError]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }
    }
}