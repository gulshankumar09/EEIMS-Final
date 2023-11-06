using EEIMS.Models;
using EEIMS.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEIMS.Controllers
{
    public class AssignmentController : Controller
    {
        // GET: Assignment
        private AssignmentRepository _assignmentRepository;

        public AssignmentController()
        {
            _assignmentRepository = new AssignmentRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateNewAssinment()
        {
            return View(new createAssignmentViewModel());
        }

        [HttpPost]
        public ActionResult CreateNewAssinment(createAssignmentViewModel assignmentViewModel)
        {
            if (ModelState.IsValid)
            {
                _assignmentRepository.CreateAssignemt(assignmentViewModel);
                return RedirectToAction("Index", "Assignment");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // You can log the error messages to your preferred logging system
                    // or display them in your view for debugging purposes.
                    Debug.WriteLine(error.ErrorMessage);
                }
                return View(assignmentViewModel);
            }
        }
    }
}