using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Services;
using PES.Models;
using System.Threading.Tasks;

namespace PES.Controllers
{
    [Authorize]
    public class VacationRequestController : Controller
    {
        //Declare Services
        private EmployeeService _employeeService;
        private VacationHeaderReqService _headerReqService;
        private VacationReqStatusService _ReqStatusService;

        public VacationRequestController()
        {
            _employeeService = new EmployeeService();
            _headerReqService = new VacationHeaderReqService();
            _ReqStatusService = new VacationReqStatusService();
        }

        /*// GET: VacationRequest
        public ActionResult HistoricalResource()
        {
            ViewBag.title = "Resource Name";
            return View();
        }*/

        // GET: VacationRequest
        public ActionResult VacationRequest()
        {
            ViewBag.title = "A Resource Name...";
            return View();
        }

        // GET: VacationRequest/HistoricalResource
        [HttpGet]
        public ActionResult HistoricalResource()
        {
            //Get current user
            Employee currentUser = new Employee();
            var currentUserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentUserEmail);

            List<VacHeadReqViewModel> listHeaderReqDB = _headerReqService.GetAllGeneralVacationHeaderReqByEmployeeId(currentUser.EmployeeId);
            List<VacHeadReqViewModel> listHeaderReqVM = new List<VacHeadReqViewModel>();

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
                        return_date = headerReq.return_date
                    };
                    listHeaderReqVM.Add(headerReqVm);
                }
            }

            ViewBag.Username = currentUser.FirstName + " " + currentUser.LastName;
            ViewBag.UserID = currentUser.EmployeeId;
            return View(listHeaderReqVM);
        }
    }
}
