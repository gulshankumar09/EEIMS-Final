using EEIMS.Functionalities;
using EEIMS.Models;
using EEIMS.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEIMS.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RequestController(IEmployeeRepository employeeRepository, ICategoryRepository categoryRepository, IRequestRepository requestRepository)
        {
            _employeeRepository = employeeRepository;
            _categoryRepository = categoryRepository;
            _requestRepository = requestRepository;

        }

        //
        // Show the Request Action
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // Get: add new request
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AddNewRequest()
        {
            var categories = _categoryRepository.GetCategories();
            IEnumerable<SelectListItem> categoriesList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            });
            ViewBag.Categories = categoriesList;

            return View();
        }
        //
        // Post: add new request
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddNewRequest(CreateNewGetViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                var employeeId = _employeeRepository.GetById(User.Identity.GetUserId()).EmployeeId;
                var newModel = new CreateNewRequestViewModel
                {
                    EmployeeId = employeeId,
                    CategoryId = requestViewModel.CategoryId,
                    Comments = requestViewModel.Comments
                };
                _requestRepository.CreateNewRequest(newModel);
                return RedirectToAction("AddNewRequest", "Request");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }
                var categories = _categoryRepository.GetCategories();
                IEnumerable<SelectListItem> categoriesList = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
                ViewBag.Categories = categoriesList;
                return View(requestViewModel);
            }
        }

        //
        // Get: pending requests from the database
        public ActionResult PendingRequests()
        {
            var requests = _requestRepository.GetRequestedByEmployees();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingRequestCount()
        {
            var requests = _requestRepository.GetRequestedByEmployees().Count();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RequestHistory()
        {
            var requests = _requestRepository.GetRequestedHistory();
            return View(requests);
        }

        //
        // Post: approve requests
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult ApproveRequest(int id)
        {
            _requestRepository.ApproveRequest(id);
            return Json(new { success = true });
        }

        //
        // Post: deny
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DenyRequest(int id)
        {
            _requestRepository.DenyRequest(id);
            return Json(new { success = true });
        }




    }
}