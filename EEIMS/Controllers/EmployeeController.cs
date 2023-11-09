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

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController() { }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //
        // Show: Organization Verified Employee List
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
                Organization = employee.Organization,
                IsVerified = employee.IsVerified
            };
            return View(newDetails);
        }

        //
        // GET: Return Json for { Index } ---> Verified Employees List
        public ActionResult GetVerifiedEmployeeList()
        {
            var employees = _employeeRepository.GetAllVerifiedEmployee().ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: To add new employee details when Users resiters themselves.
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
        // POST: To add new employee details when Users resiters themselves.
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
        // Get: authenticated employee during a session for update his/her details by Admins and Managers.
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
                Organization = employee.Organization,
            };
            return Json(employee, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: authenticated employee during a session for update his/her details by Admins and Managers.
        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel model, bool isVerified)
        {
            if (ModelState.IsValid)
            {
                model.IsVerified = isVerified;
                _employeeRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // Show: (Json uses) UpdateEmployee View for Admins and Managers
        public ActionResult EmployeeProfile()
        {
            return View();
        }

        //
        // Get: get all non-verified employees list
        public ActionResult GetNonVerifiedEmployeeList()
        {
            var employees = _employeeRepository.GetNonAllVerifiedEmployee().ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetNonVerifiedEmployeeCount()
        {
            var count = _employeeRepository.GetNonAllVerifiedEmployee().ToList().Count();

            return Json(count, JsonRequestBehavior.AllowGet);
        }



        //
        // Show: get all non-verified employees list
        public ActionResult NonVerifiedEmployeeList()
        {
            return View();
        }

    }
}