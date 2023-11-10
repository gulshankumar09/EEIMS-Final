using EEIMS.Functionalities;
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
    [Authorize(Roles = "Admin, Manager")]
    public class AssignmentController : Controller
    {
        // GET: Assignment
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public AssignmentController()
        {
            
        }
        public AssignmentController(IAssignmentRepository assignmentRepository, IEquipmentRepository equipmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            _equipmentRepository = equipmentRepository;

        }

        //
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AssignEquipments()
        {
            var equipments = _equipmentRepository.GetNotAssinedEquipments();
            ViewBag.EquipmentList = new MultiSelectList(equipments, "EquipmentId", "IdAndName");
            return View(new CreateAssignmentViewModel());
        }

        [HttpPost]
        public ActionResult AssignEquipments(CreateAssignmentViewModel assignmentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _assignmentRepository.CreateAssignemt(assignmentViewModel);
                    return RedirectToAction("Index", "Assignment");
                }
                else
                {
                    return View(assignmentViewModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Please try again..... " + e.Message);
                ModelState.AddModelError("", "An error occurred while assigning equipments. Please try again.");
                throw e;
            }
        }

        [HttpGet]
        public ActionResult UpdateAssignments(int id)
        {
            var assignment = _assignmentRepository.GetAssignmentById(id);

            return View(assignment);
        }

        [HttpPost]
        public ActionResult UpdateAssignments(UpdateAssignmentsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _assignmentRepository.UpdateAssignment(viewModel);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetAllAssignments()
        {
            var assignments = _assignmentRepository.GetAllAssignments();
            return View(assignments);
        }
    }
}