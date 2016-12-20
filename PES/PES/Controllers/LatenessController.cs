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
        //Show the latenesses to the current user
        public ActionResult Index()
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            var userEmail = (string)Session["UserEmail"];
            lateness = GetLateness.GetLatenessByEmail(userEmail, "today");
            return View(lateness);
        }

        //Get the lateness from period: today, week, month, year and last 5 years
        [HttpPost]
        public ActionResult GetLatenessByFilter(string period, string email)
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            ViewBag.period = period.ToUpper();

            if (email != null)
            {
                lateness = GetLateness.GetLatenessByEmail(email, period);
                return PartialView("_LatenessReportsByUserPartial", lateness);
            }
            else
            {
                var currentUserEmail = (string)Session["UserEmail"];
                lateness = GetLateness.GetLatenessByEmail(currentUserEmail, period);
            }
            
            return PartialView("_LatenessReportsPartial", lateness);
        }

        //To show all the latenesses to manager (current month)
        public ActionResult LatenessAllUsers()
        {
            if ((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");

            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            lateness = GetLateness.GetLatenessByCurrentMonth();
            return View(lateness);
        }

        //Show the excel file imported by manager
        public ActionResult ShowLatenesExcel(List<Lateness> lateness)
        {
            if((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");
               
            return View(lateness);
        }

        //Read the excel file imported by manager
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

        //Save the excel file imported by manager
        [HttpPost]
        public string saveLatenessExcel()
        {
            try
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
                    return "saved";
                }
            }
            catch (Exception ex)
            {
                return "error";
            }
            return "true";
        }

        //For the manager to see the list of lateness by user
        [HttpGet]
        public ActionResult LatenessByUser(string name, string email)
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            ViewBag.name = name;
            lateness = GetLateness.GetLatenessByEmail(email, "today");
            return View(lateness);
        }

        //for the manager to delete
        [HttpPost]
        public bool LatenessLogicDelete(int id)
        {
            LatenessService LatenessDelete = new LatenessService();

            if (LatenessDelete.delete(id))
            {
                return true;
            }

            return false;
        }
    }
}