using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using PES.Models;
using PES.Services;
using PES.ViewModels;
using System.Threading.Tasks;

namespace PES.Controllers
{
    public class LatenessController : Controller
    {
        // GET: LatenessReports
        public ActionResult Index()
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            var userEmail = (string)Session["UserEmail"];
            lateness = GetLateness.GetLatenessByEmail(userEmail, "today");
            return View(lateness);
        }

        [HttpPost]
        public ActionResult GetLatenessByFilter(string period)
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            var userEmail = (string)Session["UserEmail"];
            lateness = GetLateness.GetLatenessByEmail(userEmail, period);

            ViewBag.period = period.ToUpper();
            return PartialView("_LatenessReportsPartial", lateness);
        }
    }
}