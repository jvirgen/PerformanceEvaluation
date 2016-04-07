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
                // user not logged in, return home
                TempData["Error"] = "You need to log in first.";
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
                TempData["Error"] = "You're resource. You're not allowed to insert employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }

            //model.HireDate = DateTime.Today;
            //ViewBag.currentUserProfileId = currentUser.ProfileId;
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
                newEmployee.Email = employeeModel.Email + "@4thsource.com";
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                newEmployee.ManagerId = employeeModel.SelectedManager;
                //newEmployee.HireDate = employeeModel.HireDate;
                newEmployee.Customer = "No customer";
                newEmployee.Position = "Not specified";

                if (uniqueEmail(newEmployee))
                {
                    _employeeService.InsertEmployee(newEmployee);

                    // Success message
                    TempData["Success"] = "Successfully registered";

                    return RedirectToAction("ViewEmployees");
                }
                else
                {
                    // Error message
                    TempData["Error"] = "This email is already registered";

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
                        break;
                    }
                }

            if(counter == 0)
                return true;
            else
                return false;
            }
        #endregion

        private InsertEmployeeViewModel SetUpDropdowns(InsertEmployeeViewModel model)
        {
            // Get profile current user 
            var profileUser = Session["UserProfile"];
            // Get profiles
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
                if((int)profileUser == (int)ProfileUser.Director)
                {
                    profilesList.Add(newItem);
                }
                else if((int)profileUser == (int)ProfileUser.Manager && newItem.Text == "Resource")
                {
                    profilesList.Add(newItem);
                }
                
            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();

            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach (var manager in managers)
            {
                if ((int)profileUser == (int)ProfileUser.Director && manager.ProfileId == (int)ProfileUser.Director || manager.ProfileId == (int)ProfileUser.Manager)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
                else if((int)profileUser == (int)ProfileUser.Manager && manager.ProfileId == (int)ProfileUser.Manager)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
                
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
            // Get profile current user 
            var profileUser = Session["UserProfile"];
            // Get profiles
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
                if ((int)profileUser == (int)ProfileUser.Director)
                {
                    profilesList.Add(newItem);
                }
                else if ((int)profileUser == (int)ProfileUser.Manager && newItem.Text == "Resource")
                {
                    profilesList.Add(newItem);
                }
            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();

            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach (var manager in managers)
            {
                if (manager.ProfileId == 2 || manager.ProfileId == 3)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
            }

            #region Set data
            // Set profiles
            model.ListProfiles = profilesList;
            model.ListManagers = managersList;

            #endregion

            return model;
        }

        public JsonResult GetEmployeesProifile(int profile)
        {
            //Get all employees depending profile
            var employees = _employeeService.getByPorfileId((profile + 1));
           
            //Return employees json file
            return Json(new { employees = employees }, JsonRequestBehavior.AllowGet);
        } 


        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            UpdateEmployeeViewModel model = new UpdateEmployeeViewModel();

            // Get profile current user 
            var profileUser = Session["UserProfile"];

            if (profileUser == null)
            {
                // user not logged in, return home
                TempData["Error"] = "You need to log in first.";
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
                TempData["Error"] = "You're resource. You're not allowed to update employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }
            else
            {
                var employee = _employeeService.GetByID(id);

                    model.EmployeeId = employee.EmployeeId;
                    model.FirstName = employee.FirstName;
                    model.LastName = employee.LastName;
                    //model.Email = employee.Email;

                    string email = employee.Email;
                    var emailUsername = email.Split('@');

                    model.Email = emailUsername[0];
                    model.SelectedProfile = employee.ProfileId;
                    model.SelectedManager = employee.ManagerId;

                    //ViewBag.currentUserProfileId = currentUser.ProfileId;
                    return View(model);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel employeeModel)
        {
            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);

            if (ModelState.IsValid)
            {
                var newEmployee = new Employee();

                newEmployee.EmployeeId = employeeModel.EmployeeId;
                newEmployee.FirstName = employeeModel.FirstName;
                newEmployee.LastName = employeeModel.LastName;
                newEmployee.Email = employeeModel.Email + "@4thsource.com";
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                newEmployee.ManagerId = employeeModel.SelectedManager;

                _employeeService.UpdateEmployee(newEmployee);

                // Success message
                TempData["Success"] = "Successfully updated";
                return RedirectToAction("ViewEmployees", "Employee");
            }
            employeeModel = SetUpDropdowns(employeeModel);
            return View(employeeModel);
        }

        public ActionResult ViewEmployees()
        {
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];

            currentUser = _employeeService.GetByEmail(userEmail);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                //Resource is only allowed to view his own info
                return RedirectToAction("EmployeeDetails/" + currentUser.EmployeeId);
            }
            else if(currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                //Get employees of manager org
                List<Employee> EmployeeList = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                List<EmployeeDetailsViewModel> ModelList = new List<EmployeeDetailsViewModel>();

                foreach (var employee in EmployeeList)
                {
                    EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

                    //get porfiles
                    var porfile = _profileService.GetProfileByID(employee.ProfileId);
                    var manager = _employeeService.GetByID(employee.ManagerId);

                    model.EmployeeId = employee.EmployeeId;
                    model.FirstName = employee.FirstName;
                    model.LastName = employee.LastName;
                    model.Email = employee.Email;
                    model.Customer = employee.Customer;
                    model.Position = employee.Position;
                    model.ProfileId = employee.ProfileId;
                    model.ManagerId = employee.ManagerId;
                    //model.HireDate = employee.HireDate;
                    model.EndDate = employee.EndDate;
                    model.Project = employee.Project;
                    model.Profile = porfile;
                    model.Manager = manager;
                    if (model.EmployeeId != currentUser.EmployeeId)
                    {
                        ModelList.Add(model);
                    }
                }

                return View(ModelList);
            }
            else if(currentUser.ProfileId == (int)ProfileUser.Director)
            {
            // Get all employees
            List<Employee> EmployeeList = _employeeService.GetAll();
            List<EmployeeDetailsViewModel> ModelList = new List<EmployeeDetailsViewModel>();

            foreach (var employee in EmployeeList)
            {
                EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

                //get porfiles
                var porfile = _profileService.GetProfileByID(employee.ProfileId);
                var manager = _employeeService.GetByID(employee.ManagerId);

                model.EmployeeId = employee.EmployeeId;
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.Email = employee.Email;
                model.Customer = employee.Customer;
                model.Position = employee.Position;
                model.ProfileId = employee.ProfileId;
                model.ManagerId = employee.ManagerId;
                //model.HireDate = employee.HireDate;
                model.EndDate = employee.EndDate;
                model.Project = employee.Project;
                model.Profile = porfile;
                model.Manager = manager;

                ModelList.Add(model);
            }

            return View(ModelList);
        }
            else
            {
                // Error if not logged in
                TempData["Error"] = "You are not logged in.";

                return RedirectToAction("Login", "UserLogin");
            }
               
        }

        public JsonResult GetEmployeeStatus(string email)
        {
            //Get all employees depending profile
            var employees = _employeeService.GetByEmail(email);

            //Return employees json file
            return Json(new { employees = employees }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult EmployeeDetails(string email)
        {
            var employee = _employeeService.GetByEmail(email);

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
            //model.HireDate = employee.HireDate;
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

        [HttpGet]
        public ActionResult ChangeProfile()
        {
            // Get current user  
            Employee currentUser = new Employee();
            ChangeProfileViewModel ChangedEmployee = new ChangeProfileViewModel();
            currentUser = _employeeService.GetByEmail((string)Session["UserEmail"]);
            if (currentUser.ProfileId == (int)ProfileUser.Director || currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                if (_employeeService.GetEmployeeByManager(currentUser.EmployeeId).Count > 1)
                {
                    ChangedEmployee.FirstName = currentUser.FirstName;
                    ChangedEmployee.LastName = currentUser.LastName;
                    ChangedEmployee.Email = currentUser.Email;
                    ChangedEmployee.SelectedProfile = currentUser.ProfileId;
                    ChangedEmployee.SelectedManager = currentUser.ManagerId;
                    SetUpDropdowns(ChangedEmployee);

                    return View(ChangedEmployee);
                }
                else
                {
                    TempData["Error"] = "You do not have employees in you org to transfer";
                    return RedirectToAction("UpdateEmployee", new { id = currentUser.EmployeeId });
                }
            }
            else if(currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                TempData["Error"] = "Resources are not allowed to change his profile";
                return RedirectToAction("ViewEmployees");
            }
            else
            {
                TempData["Error"] = "You are not logged in.";
                return RedirectToAction("Login", "LoginUser");
            }
            
        }

        [HttpPost]
        public ActionResult ChangeProfile(ChangeProfileViewModel model)
        {

            Employee changedEmployee = new Employee();
            changedEmployee = _employeeService.GetByEmail(model.Email);
            
            changedEmployee.ManagerId = model.SelectedManager;
            changedEmployee.ProfileId = model.SelectedProfile;

            if(_employeeService.GetByID(changedEmployee.ManagerId).ProfileId > changedEmployee.ProfileId || (_employeeService.GetByID(changedEmployee.ManagerId).EmployeeId == changedEmployee.EmployeeId && changedEmployee.ProfileId == (int)ProfileUser.Director))
            {
                //Send info to service
                if (_employeeService.TransferAllEmployees(changedEmployee.EmployeeId, model.NewManager))
                {
                    _employeeService.UpdateEmployee(changedEmployee);
                    TempData["Success"] = "Employees in your org have been transfered successfully.";
                    return RedirectToAction("Logout", "LoginUser");
                }
                else
                {
                    TempData["Error"] = "Employees transfering error. Please verify your information.";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Invalid manager. Please select an employee with a profile higher than you.";
                return View(model);
            }
            
        }

    }
}