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
        // GET: /EmployeeList
        public ActionResult GetEmployeeList()
        {
            var employees = _employeeRepository.GetAllEmployee().ToList();

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

        [HttpGet]
        public ActionResult GetEmployeeDetail()
        {
            var employee = _employeeRepository.GetById(User.Identity.GetUserId());

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

        


        public ActionResult Delete(int id)
        {
            _employeeRepository.DeleteById(id);
            return View();
        }

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