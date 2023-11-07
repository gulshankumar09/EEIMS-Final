using EEIMS.Functionalities;
using EEIMS.Models;
using EEIMS.Repositories;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace EEIMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        IEmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            
        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Employee/Details ---> to check and update details by admin
        public ActionResult DetailsAdminUse(string id)
        {
            var employee = _employeeRepository.GetById(id);

            var newDetails = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Designation = employee.Designation,
                Department = employee.Department,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Organization = employee.Organization
            };
            return View(newDetails);
        }

        //
        // GET: /EmployeeList
        public ActionResult GetVerifiedEmployeeList()
        {
            var employees = _employeeRepository.GetAllVerifiedEmployee().ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Employee/FirstTimeAddEmployee
        [HttpGet]
        public ActionResult FirstTimeAddEmployee(string UserId)
        {
            var emp = _employeeRepository.GetById(UserId);

            var modelVM = new FirstTimeAddEmployeeViewModel
            {
                Id = emp.Id,
                Email = emp.Email,
                EmployeeId = emp.EmployeeId,
            };

            return View(modelVM);
        }

        //
        // POST: /Employee/FirstTimeAddEmployee
        [HttpPost]
        public  ActionResult FirstTimeAddEmployee(FirstTimeAddEmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                 _employeeRepository.AddOnce(employee);
                return RedirectToAction("Index", "Employee");
            }
            return View();
        }

        //
        // get authenticated employee during a session
        [HttpGet]
        public ActionResult GetEmployeeDetail(string id)
        {
            if (id == null)
            {
                id = User.Identity.GetUserId();
            }
            var employee = _employeeRepository.GetById(id);

            var newDetails = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Designation = employee.Designation,
                Department = employee.Department,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Organization = employee.Organization
            };
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // to edit employee user see and update his/her profile
        public ActionResult EmployeeProfile()
        {
            return View();
        }


        

        //
        //Created a Dropdown list for any
        /*public void PopulateTrainerSelectList()  // function to get trainer list as dropdown
        {
            IRepositary<Trainer> trainerRepo = new TrainerRepositary();
            var trainerOptions = trainerRepo.GetList();
            IEnumerable<SelectListItem> trainerSelectList = trainerOptions.Select(c => new SelectListItem
            {
                Text = c.TrainerName,
                Value = c.TrainerId.ToString()
            }).ToList();

            ViewBag.TrainerId = trainerSelectList;
        }*/
    }
}