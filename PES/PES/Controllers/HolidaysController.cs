using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
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

        // GET: Holidays1/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Holiday holiday = await db.Holidays.FindAsync(id);
        //    if (holiday == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(holiday);
        //}

        // GET: Holidays1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holidays1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

            return View(holiday);
        }

        //[HttpPost]
        //public ActionResult CreateHoliday(Holiday model)
        //{
        //    //string descripcion = model.Description;
        //    _holidayService.CreateHoliday(model);
        //    //Here add a new instance of the class VacationHeaderReqService to insert the data in the DB (InsertVacHeaderReq metod)

        //    //foreach (var date in model.SubRequest)
        //    //{
        //    //    //Here insert the data of the SubResquest in the DB using the metod InsertSubReq in VacationSubreqService
        //    //    dates = date.Date.Split('-');
        //    //    date.StartDate = Convert.ToDateTime(dates[0]);
        //    //    date.EndDate = Convert.ToDateTime(dates[1]);

        //    //}
        //    // Return a message in the screen a redirect to the Historical Request Screen.
        //    return View();
        //}

        // GET: Holidays1/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Holiday holiday = await db.Holidays.FindAsync(id);
        //    if (holiday == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(holiday);
        //}

        // POST: Holidays1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "HolidayId,Day,Description")] Holiday holiday)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(holiday).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(holiday);
        //}

        // GET: Holidays1/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Holiday holiday = await db.Holidays.FindAsync(id);
        //    if (holiday == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(holiday);
        //}

        // POST: Holidays1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Holiday holiday = await db.Holidays.FindAsync(id);
        //    db.Holidays.Remove(holiday);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
