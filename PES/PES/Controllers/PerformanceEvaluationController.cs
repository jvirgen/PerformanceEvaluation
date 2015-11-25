using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using OfficeOpenXml;

namespace PES.Controllers
{
    public class PerformanceEvaluationController : Controller
    {
        // GET: PerformanceEvaluation
        public ActionResult Index()
        {
            return View();
        }

        // GET: PerformanceEvaluation/UploadFile
        public ActionResult UploadFile()
        {
            return View();
        }

        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult SearchInformation()
        {
            return View();
        }
        
        // GET: PeformanceEvaluation/SearchIformation
        public ActionResult Login()
        {
            return View();
        }

        // GET: PerformanceEvaluation/ChoosePeriod
        public ActionResult ChoosePeriod()
        {
            return View();
        }

        // GET: PerformanceEvaluation/SearchInfoRank
        public ActionResult SearchInfoRank()
        {
            return View();
        }

        //GET: PerformanceEvaluation/PEVisualization
        public ActionResult PEVisualization()
        {
            return View();
        }

        public ActionResult LoadPerformanceEvaluationFile(HttpPostedFileBase fileUploaded)
        {
            string errorMessage = "";
            try
            {
               
                bool result = false;

                // Check file was submitted             
                if (fileUploaded != null && fileUploaded.ContentLength > 0)
                {
                    string fname = "";
                    string fullPath = "";

                    //Store file temporary
                    fname = Path.GetFileName(fileUploaded.FileName);
                    fileUploaded.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));

                    //Get full path of the file 
                    fullPath = Request.MapPath("~/App_Data/" + fname);

                    try
                    {
                        // Load file into database
                        //result = await _userService.LoadUsersFromXLSFile(fullPath);
                    }
                    finally
                    {
                        //Delete temporary file, always delete file already stored 
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }

                //If there are users 
                if (result != false)
                {
                    // File loaded successfuly
                    return View("SaveUsers", result);
                }
                else
                {
                    // Problem loading the file
                    errorMessage = "There was a problem trying to load the file. Please review file and try again.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = "There was a problem trying to load the file. Please review file and try again. " + ex.Message;
            }

            return View();
        }

        private bool SavePerformanceEvaluationFile(string pathFileString)
        {
            //Declare variables

            #region Load XLS file with EPPlus
            //Validate path file 
            //if (!String.IsNullOrEmpty(pathFileString))
            //{
            //    FileInfo file;

            //    try
            //    {
            //        //Creates a new file
            //        file = new FileInfo(pathFileString);

            //        //Excel file valid extensions
            //        var validExtensions = new string[] { ".xls", ".xlsx" };

            //        //Validate extesion of file selected
            //        if (!validExtensions.Contains(file.Extension))
            //        {
            //            throw new System.IO.FileFormatException("Excel file not found in specified path");
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }

            //    // Open and read the XlSX file.
            //    ExcelPackage package = null;
            //    try
            //    {
            //        package = new ExcelPackage(file);
            //    }
            //    catch (System.IO.FileFormatException ex)
            //    {
            //        throw new System.IO.FileFormatException("Unable to read excel file");
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }

            //    using (package)
            //    {
            //        // Get the work book in the file
            //        ExcelWorkbook workBook = package.Workbook;

            //        //If there is a workbook
            //        if (workBook != null && workBook.Worksheets.Count > 0)
            //        {
            //            // Get the first worksheet
            //            ExcelWorksheet sheet = workBook.Worksheets.First();
            //        }
            //    }
            //}
            #endregion

            return false;
        }
    }
}