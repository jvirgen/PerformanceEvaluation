using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PES.Models;
using PES.Services;


namespace PES.Controllers
{
    public class HolidaysController : Controller
    {
        private HolidayService _holidayService;

        public HolidaysController()
        {
            _holidayService = new HolidayService();
        }

        // GET: Holidays1
        public async Task<ActionResult> Index()
        {
            IEnumerable<Holiday> holidays = new List<Holiday>();

            holidays = _holidayService.GetAllHolidays();

            return View(holidays);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int holidayId)
        {
            _holidayService.DeleteHoliday(holidayId);
            return RedirectToAction("Index");
        }

        // POST: Holidays1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Holiday holiday)
        {
            Holiday newHoliday = new Holiday();
            newHoliday.Description = holiday.Description;
            string endDate = holiday.InsertDay;
            string eMonth = endDate.Substring(0, 2);
            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "/" + eMonth + "/" + eYear);
            newHoliday.InsertDay = eFinalEndDate;
            if (ModelState.IsValid)
            {
                _holidayService.CreateHoliday(newHoliday);
            }
            return View();
            //return View(holiday);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHoliday(Holiday holiday)
        {

            return View();
        }

    }
}
