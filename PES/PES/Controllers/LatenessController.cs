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
using OfficeOpenXml;

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

        public ActionResult LatenessAllUsers()
        {
            if ((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");

            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            lateness = GetLateness.GetLatenessByCurrentMonth();
            return View(lateness);
        }

        public ActionResult ShowLatenesExcel(List<Lateness> lateness)
        {
            if((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");
               
            return View(lateness);
        }

        [HttpPost]
        public ActionResult UploadLatenessExcel()
        {
            List<Lateness> latenesses = new List<Lateness>();

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            Lateness lateness = new Lateness();
                            lateness.EmployeeEmail = workSheet.Cells[rowIterator, 1].Value.ToString();
                            lateness.Date = Convert.ToDateTime(workSheet.Cells[rowIterator, 2].Value.ToString());
                            lateness.Time = Convert.ToDateTime(workSheet.Cells[rowIterator, 3].Value.ToString());
                            latenesses.Add(lateness);
                        }
                    }
                }

            }
            Session["tmpLateness"] = latenesses;

            return View("ShowLatenesExcel", latenesses);
        }

        [HttpPost]
        public bool saveLatenessExcel()
        {
            LatenessService lateness = new LatenessService();
            var latenessList = Session["tmpLateness"] as List<Lateness>;

            if (latenessList != null)
            {
                lateness.insertLateness(latenessList);
                Session.Remove("tmpLateness");
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}