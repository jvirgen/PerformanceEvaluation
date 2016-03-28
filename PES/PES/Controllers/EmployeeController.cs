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

namespace PES.Controllers
{
    [AllowAnonymous]
    public class EmployeeController : Controller
    {
        // Declare services here
        private ProfileService _profileService;
        private EmployeeService _employeeService;

        public EmployeeController() 
        {
            _profileService = new ProfileService();
            _employeeService = new EmployeeService();
        }

        // GET: Employee
        public ActionResult Index()
        {   

            Login Mail = new Login();
            EmployeeService Getemployee = new EmployeeService();
            Employee Employee = new Employee();

            Employee = Getemployee.GetByEmail(Mail.UserEmail);
            return View(Employee);
        }

        public ActionResult InsertEmployee()
        {
            InsertEmployeeViewModel model = new InsertEmployeeViewModel();

            // Get profiles
            // test empty list of profiles, replace this line with a call to service to get the profiles
            var profiles = _profileService.GetAllProfiles();

            // Populate profiles 
            List<SelectListItem> profilesList = new List<SelectListItem>();
            foreach (var profile in profiles)
            {
                var newItem = new SelectListItem()
                {
                    Text = profile.Name,
                    Value = (profile.ProfileId).ToString(),
                    Selected = false
                };
                profilesList.Add(newItem);
            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();


            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach(var manager in managers)
            {
                var newItem = new SelectListItem()
                {
                    Text = manager.FirstName + " " + manager.LastName,
                    Value = (manager.EmployeeId).ToString(),
                    Selected = false
                };
                managersList.Add(newItem);
            }

            #region Set data
            // Set profiles
            model.ListProfiles = profilesList;
            model.ListManagers = managersList;

            #endregion


            return View(model);
        }

        [HttpPost]
        public ActionResult InsertEmployee(InsertEmployeeViewModel employeeModel)
        {
            Employee newEmployee = new Employee();

            newEmployee.FirstName = employeeModel.FirstName;
            newEmployee.LastName = employeeModel.LastName;
            newEmployee.Email = employeeModel.Email;
            newEmployee.ProfileId = employeeModel.SelectedProfile;
            newEmployee.ManagerId = employeeModel.SelectedManager;
            newEmployee.HireDate = employeeModel.HireDate;
            newEmployee.Customer = "No Customer";
            newEmployee.Position = "No Specified";
            newEmployee.Project = employeeModel.Project;

            _employeeService.InsertEmployee(newEmployee);

            // Add success message

            return RedirectToAction("ViewEmployees");
        }

        public ActionResult ViewEmployees()
        {
            return View(_employeeService.GetAll());
        }


        [HttpGet]
        public ActionResult EmployeeDetails(int EmployeeId)
        {
            return View(_employeeService.GetByID(EmployeeId));
        }
    }
}