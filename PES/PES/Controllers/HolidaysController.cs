using PES.Models;
using PES.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.Controllers
{
    public class HolidaysController : Controller
    {
        private HolidayService _holidayService;

        public HolidaysController()
        {
            _holidayService = new HolidayService();
        }

        // GET: Holidays
        public ActionResult Index()
        {
            IEnumerable<Holiday> holidays = new List<Holiday>();

            holidays = _holidayService.GetAllHolidays();

            return View(holidays);
        }
    }
}