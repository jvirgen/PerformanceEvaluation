using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Services;
using PES.Models;
using System.Threading.Tasks;
using PES.ViewModels;

namespace PES.Controllers
{
    [Authorize]
    public class VacationRequestController : Controller
    {
        //Declare Services
        private EmployeeService _employeeService;
        private VacationHeaderReqService _headerReqService;
        private VacationReqStatusService _ReqStatusService;
        private VacationSubreqService _subReqService;
        //Added
        //private HolidayService _holiday;

        public VacationRequestController()
        {
            _employeeService = new EmployeeService();
            _headerReqService = new VacationHeaderReqService();
            _ReqStatusService = new VacationReqStatusService();
            _subReqService = new VacationSubreqService();
            //Added
            //_holiday = new HolidayService();
        }

        /// <summary>
        /// GET: New vacation requests. The metod only need a User Id parameter 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>New Request Screen</returns>
        [HttpGet]
        public ActionResult InsertNewRequest(int userid)
        {
            Employee currentEmployee = new Employee();
            _employeeService = new EmployeeService();
            currentEmployee = _employeeService.GetByID(userid);
            InsertNewRequestViewModel newRequest = new InsertNewRequestViewModel();
            newRequest.EmployeeId = userid;
            newRequest.Freedays = currentEmployee.Freedays;
            newRequest.SubRequest = new List<NewVacationDates>();
            ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
            return View(newRequest);
        }

        /// <summary>
        /// POST of New Vacation Request to get all data form the New Request View to process and insert them in the DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirect to Historial Screen</returns>
        [HttpPost]
        public ActionResult InsertNewRequest(InsertNewRequestViewModel model)
        {
            string[] dates;
            //Here add a new instance of the class VacationHeaderReqService to insert the data in the DB (InsertVacHeaderReq metod)

            foreach (var date in model.SubRequest)
            {
                //Here insert the data of the SubResquest in the DB using the metod InsertSubReq in VacationSubreqService
                dates = date.Date.Split('-');
                date.StartDate = Convert.ToDateTime(dates[0]);
                date.EndDate = Convert.ToDateTime(dates[1]);
                
            }
            // Return a message in the screen a redirect to the Historical Request Screen.
            return View();
        }

        /// <summary>
        /// GET: New vacation requests. The metod only need a User Id parameter 
        /// </summary>
        /// <param name="userid"></param>
        ///// <returns>New Request Screen</returns>
        
    


        //[HttpPost]
        //public ActionResult InsertNewHoliday(Holiday model)
        //{
        //    string[] dates;
        //    string description;
        //    //Here add a new instance of the class VacationHeaderReqService to insert the data in the DB (InsertVacHeaderReq metod)

        //    foreach (var date in model.Day)
        //    {
        //        //Here insert the data of the SubResquest in the DB using the metod InsertSubReq in VacationSubreqService
        //        dates = date.Date.Split('-');
        //        date.StartDate = Convert.ToDateTime(dates[0]);
        //        date.EndDate = Convert.ToDateTime(dates[1]);

        //    }
        //    // Return a message in the screen a redirect to the Historical Request Screen.
        //    return View();
        //}



        /// <summary>
        /// GET: VacationRequest Existing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VacationRequest()
        {
            return View();
        }

        /// <summary>
        /// GET: VacationRequest Existing. Metod to get a existing request using Header Request Id 
        /// </summary>
        /// <param name="headerReqId"></param>
        /// <returns>Redirect Vacation Request passing the data of the existing request</returns>
        [HttpGet]
        public ActionResult GetVacationRequest(int headerReqId)
        {
            VacHeadReqViewModel currentRequest = new VacHeadReqViewModel();
            currentRequest = _headerReqService.GetAllVacRequestInfoByVacReqId(headerReqId);
            Employee currentUser = new Employee();
            currentUser = _employeeService.GetByID(currentRequest.EmployeeId);
            ViewBag.status = currentRequest.status;
            currentRequest.FreeDays = currentUser.Freedays;
            return View("VacationRequest", currentRequest);
        }

        /// <summary>
        /// GET: HistoricalResource. Metod to Get all the data of the current employee and all its vacation requests 
        /// </summary>
        /// <returns>A list of all Resquests in the Historical Resource View</returns>
        [HttpGet]
        public ActionResult HistoricalResource()
        {
            //Get current user
            Employee currentUser = new Employee();
            var currentUserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentUserEmail);

            List<VacHeadReqViewModel> listHeaderReqDB = new List<VacHeadReqViewModel>();
            List<VacHeadReqViewModel> listHeaderReqVM = new List<VacHeadReqViewModel>();
            if (currentUser.ProfileId == Convert.ToInt32(ProfileUser.Manager))
            {
                listHeaderReqDB = _headerReqService.GetAllGeneralVacationHeaderReqByManagerId(currentUser.EmployeeId);
            }
            else if (currentUser.ProfileId == Convert.ToInt32(ProfileUser.Resource))
            {
                listHeaderReqDB = _headerReqService.GetGeneralVacationHeaderReqByEmployeeId(currentUser.EmployeeId);
            }
            if (listHeaderReqDB != null && listHeaderReqDB.Count > 0)
            {
                foreach(var headerReq in listHeaderReqDB)
                {
                    var headerReqVm = new VacHeadReqViewModel
                    {
                        VacationHeaderReqId = headerReq.VacationHeaderReqId,
                        EmployeeId = headerReq.EmployeeId,
                        Title = headerReq.Title,
                        NoVacDays = headerReq.NoVacDays,
                        status = _ReqStatusService.GetVacationReqStatusById(headerReq.ReqStatusId).Name,
                        StartDate = headerReq.StartDate,
                        EndDate = headerReq.EndDate,
                        ReturnDate = headerReq.ReturnDate,
                        FirstName = headerReq.FirstName,
                        LastName = headerReq.LastName,
                        HaveProject = headerReq.HaveProject
                    };
                    listHeaderReqVM.Add(headerReqVm);
                }
            }

            ViewBag.Username = currentUser.FirstName + " " + currentUser.LastName;
            ViewBag.UserID = currentUser.EmployeeId;
            ViewBag.FreeDays = currentUser.Freedays;
            ViewBag.profileId = currentUser.ProfileId;
            return View(listHeaderReqVM);
        }
    }
}
