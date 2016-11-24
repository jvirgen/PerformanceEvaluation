using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.Controllers
{
    public class VacationRequestController : Controller
    {
        // GET: VacationRequest
        public ActionResult HistoricalResource()
        {
            ViewBag.title = "Resource Name";
            return View();
        }

        // GET: VacationRequest
        public ActionResult VacationRequest()
        {
            ViewBag.title = "A Resource Name...";
            return View();
        }
    }
}
