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
using System.Data.SqlClient;

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
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Employee/Details ---> to check and update details by admin
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DetailsAdminUse(string id)
        {
            try
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        //
        // GET: Return Json for { Index } ---> Verified Employees List
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult GetVerifiedEmployeeList()
        {
            try
            {
                var employees = _employeeRepository.GetAllVerifiedEmployee().ToList();
                return Json(employees, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }

        }

        //
        // GET: To add new employee details when Users resiters themselves.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FirstTimeAddEmployee(string UserId)
        {
            try
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        //
        // POST: To add new employee details when Users resiters themselves.
        [HttpPost]
        [AllowAnonymous]
        public  ActionResult FirstTimeAddEmployee(FirstTimeAddEmployeeViewModel employee)
        {
            try { 
                if (ModelState.IsValid)
                {
                     _employeeRepository.AddOnce(employee);
                    return RedirectToAction("Index", "Employee");
                }
                return View();
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        //
        // Get: authenticated employee during a session for update his/her details by Admins and Managers.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetEmployeeDetail(string id)
        {
            try
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }
        //
        // POST: authenticated employee update his/her details by Admins and Managers.
        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel model, bool isVerified)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.IsVerified = isVerified;
                    _employeeRepository.Update(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        //
        // Post: to update the profiles
        [HttpPost]
        public ActionResult UpdateProfile(UpdateEmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeRepository.Update(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
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
            try
            {
                var employees = _employeeRepository.GetNonAllVerifiedEmployee().ToList();

                return Json(employees, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        public ActionResult GetNonVerifiedEmployeeCount()
        {
            try 
            { 
                var count = _employeeRepository.GetNonAllVerifiedEmployee().ToList().Count();

                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View("_Error");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return new HttpStatusCodeResult(500, "Internal server error" + ex.Message);
            }
        }

        //
        // Show: get all non-verified employees list
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult NonVerifiedEmployeeList()
        {
            return View();
        }



    }
}