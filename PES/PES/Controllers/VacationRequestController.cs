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

        public VacationRequestController()
        {
            _employeeService = new EmployeeService();
            _headerReqService = new VacationHeaderReqService();
            _ReqStatusService = new VacationReqStatusService();
            _subReqService = new VacationSubreqService();
        }

        //GET: New VacationRequest 
        [HttpGet]
        public ActionResult InsertNewRequest(int userid)
        {
            Employee currentEmployee = new Employee();
            _employeeService = new EmployeeService();
            currentEmployee = _employeeService.GetByID(userid);
            InsertNewRequestViewModel newRequest = new InsertNewRequestViewModel();
            newRequest.employeeId = userid;
            newRequest.freedays = currentEmployee.Freedays;
            newRequest.subRequest = new List<NewVacationDates>();

            return View(newRequest);
        }

        //POST: New VacationRequest 
        [HttpPost]
        public ActionResult InsertNewRequest(InsertNewRequestViewModel model)
        {

            string x = "";
            return View();
        }

        // GET: VacationRequest Existing
        [HttpGet]
        public ActionResult VacationRequest()
        {
            return View();
        }

        // GET: VacationRequest Existing 
        [HttpGet]
        public ActionResult GetVacationRequest(int headerReqId)
        {
            VacHeadReqViewModel currentRequest = new VacHeadReqViewModel();
            currentRequest = _headerReqService.GetAllVacRequestInfoByVacReqId(headerReqId);
            Employee currentUser = new Employee();
            currentUser = _employeeService.GetByID(currentRequest.employeeId);
            ViewBag.status = currentRequest.status;
            ViewBag.freedays = currentUser.Freedays;
            return View("VacationRequest", currentRequest);
        }
    
        // GET: VacationRequest/HistoricalResource
        [HttpGet]
        public ActionResult HistoricalResource()
        {
            //Get current user
            Employee currentUser = new Employee();
            var currentUserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentUserEmail);

            List<VacHeadReqViewModel> listHeaderReqDB = new List<VacHeadReqViewModel>();
            List<VacHeadReqViewModel> listHeaderReqVM = new List<VacHeadReqViewModel>();
            if (currentUser.ProfileId == 2)
            {
                listHeaderReqDB = _headerReqService.GetAllGeneralVacationHeaderReqByManagerId(currentUser.EmployeeId);
            }
            else if (currentUser.ProfileId == 1)
            {
                listHeaderReqDB = _headerReqService.GetGeneralVacationHeaderReqByEmployeeId(currentUser.EmployeeId);
            }
            if (listHeaderReqDB != null && listHeaderReqDB.Count > 0)
            {
                foreach(var headerReq in listHeaderReqDB)
                {
                    var headerReqVm = new VacHeadReqViewModel
                    {
                        vacationHeaderReqId = headerReq.vacationHeaderReqId,
                        employeeId = headerReq.employeeId,
                        title = headerReq.title,
                        noVacDays = headerReq.noVacDays,
                        status = _ReqStatusService.GetVacationReqStatusById(headerReq.ReqStatusId).name,
                        start_date = headerReq.start_date,
                        end_date = headerReq.end_date,
                        return_date = headerReq.return_date,
                        first_name = headerReq.first_name,
                        last_name = headerReq.last_name,
                        have_project = headerReq.have_project
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
