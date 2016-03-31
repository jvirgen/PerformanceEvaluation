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

        [HttpGet]
        public ActionResult InsertEmployee()
        {
            InsertEmployeeViewModel model = new InsertEmployeeViewModel();

            // Get profile current user 
            var profileUser = Session["UserProfile"];

            if (profileUser == null)
            {
                return RedirectToAction("Login", "LoginUser");
            }

            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);

            model = SetUpDropdowns(model);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home  
                // send error
                //TempData["Error"] = "You're resource. You're not allowed to insert employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }

            model.HireDate = DateTime.Today;
            ViewBag.currentUserProfileId = currentUser.ProfileId;
            return View(model);
        }

        [HttpPost]
        public ActionResult InsertEmployee(InsertEmployeeViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = new Employee();


                newEmployee.FirstName = employeeModel.FirstName;
                newEmployee.LastName = employeeModel.LastName;
                newEmployee.Email = employeeModel.Email;
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                newEmployee.ManagerId = employeeModel.SelectedManager;
                newEmployee.HireDate = employeeModel.HireDate;
                newEmployee.Customer = "No customer";
                newEmployee.Position = "Not specified";

                if (uniqueEmail(newEmployee))
                {
                    _employeeService.InsertEmployee(newEmployee);

                    // Add success message
                    TempData["Success"] = "Successfully registered";

                    return RedirectToAction("ViewEmployees");
                }
                else
                {
                    // Add error message
                    TempData["Error"] = "Email already is registered";

                    employeeModel = SetUpDropdowns(employeeModel);
                    return View(employeeModel);
                }
               
            }

            employeeModel = SetUpDropdowns(employeeModel);

            return View(employeeModel);
        }

        #region verify uniqueEmail
        private bool uniqueEmail (Employee employee)
        {
            var counter = 0;
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _employeeService.GetAll();

            foreach(var item in EmployeeList)
            {
                if(employee.Email == item.Email)
                {
                    counter++;
                    if(counter > 0)
                    {
                        break;
                    }
                }
            }

            if(counter == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private InsertEmployeeViewModel SetUpDropdowns(InsertEmployeeViewModel model)
        {
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
            foreach (var manager in managers)
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

            return model;
        }

        private UpdateEmployeeViewModel SetUpDropdowns(UpdateEmployeeViewModel model)
        {
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
            foreach (var manager in managers)
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

            return model;
        }


        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            UpdateEmployeeViewModel model = new UpdateEmployeeViewModel();

            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);

            model = SetUpDropdowns(model);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home  
                // send error
                //TempData["Error"] = "You're resource. You're not allowed to update employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }

            var employee =_employeeService.GetByID(id);

            model.EmployeeId = employee.EmployeeId;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Email = employee.Email;
            model.SelectedProfile = employee.ProfileId;
            model.SelectedManager = employee.ManagerId;

            ViewBag.currentUserProfileId = currentUser.ProfileId;
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = new Employee();

                newEmployee.EmployeeId = employeeModel.EmployeeId;
                newEmployee.FirstName = employeeModel.FirstName;
                newEmployee.LastName = employeeModel.LastName;
                newEmployee.Email = employeeModel.Email;
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                newEmployee.ManagerId = employeeModel.SelectedManager;

                _employeeService.UpdateEmployee(newEmployee);

                // Add success message
                TempData["Success"] = "Successfully updated";

                return RedirectToAction("ViewEmployees");
            }
            employeeModel = SetUpDropdowns(employeeModel);

            return View(employeeModel);
        }

        public ActionResult ViewEmployees()
        {
            // Get all employees
            List<Employee> EmployeeList = _employeeService.GetAll();
            List<EmployeeDetailsViewModel> ModelList = new List<EmployeeDetailsViewModel>();

            foreach (var employee in EmployeeList)
            {
                EmployeeDetailsViewModel modelo = new EmployeeDetailsViewModel();

                //get porfiles
                var porfile = _profileService.GetProfileByID(employee.ProfileId);
                var manager = _employeeService.GetByID(employee.ManagerId);

                modelo.EmployeeId = employee.EmployeeId;
                modelo.FirstName = employee.FirstName;
                modelo.LastName = employee.LastName;
                modelo.Email = employee.Email;
                modelo.Customer = employee.Customer;
                modelo.Position = employee.Position;
                modelo.ProfileId = employee.ProfileId;
                modelo.ManagerId = employee.ManagerId;
                modelo.HireDate = employee.HireDate;
                modelo.EndDate = employee.EndDate;
                modelo.Project = employee.Project;
                modelo.Profile = porfile;
                modelo.Manager = manager;

                ModelList.Add(modelo);
            }

            return View(ModelList);
        }


        [HttpGet]
        public ActionResult EmployeeDetails(int id)
        {
            var employee = _employeeService.GetByID(id);

            // Get manager
            var manager = _employeeService.GetByID(employee.ManagerId);
            // Get profile
            var porfile = _profileService.GetProfileByID(employee.ProfileId);

            EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Email = employee.Email;            
            model.ProfileId = employee.ProfileId;
            model.ManagerId = employee.ManagerId;
            model.HireDate = employee.HireDate;
            model.EndDate = employee.EndDate;
            model.Profile = porfile;
            model.Manager = manager;
    
            return View(model);
        }

        public ActionResult DisableEmployee(int id)
        {
            var disabledEmployee = _employeeService.GetByID(id);
            disabledEmployee.EndDate = DateTime.Now;
            _employeeService.UpdateEmployee(disabledEmployee);

            return RedirectToAction("ViewEmployees");
        }

        public ActionResult EnableEmployee(int id)
        {
            var enabledEmployee = _employeeService.GetByID(id);
            enabledEmployee.EndDate = null;
            _employeeService.UpdateEmployee(enabledEmployee);

            return RedirectToAction("ViewEmployees");
        }
    }
}