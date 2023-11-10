using EEIMS.Functionalities;
using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEIMS.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class EquipmentController : Controller
    {
        // GET: Equipment
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllEquipments()
        {
            var temp = _equipmentRepository.GetAllEquipments();
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddEquipment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEquipment(AddEquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _equipmentRepository.AddEquipment(model);
                return RedirectToAction("AddEquipment", "Equipment");
            }
            return View(model);
        }

        public ActionResult RemoveEquipment(int id)
        {
            _equipmentRepository.RemoveEquipment(id);
            return RedirectToAction("Index", "Equipment");
        }

        [HttpGet]
        public ActionResult EditEquipment(int id)
        {
            var temp = _equipmentRepository.GetEquipment(id);
            return View(temp);
        }

        [HttpPost]
        public ActionResult EditEquipment(UpdateEquipmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                _equipmentRepository.UpdateEquipment(model);
                return RedirectToAction("Index", "Equipment");
            }
            return View(model);
        }

        //
        // Get: count of all the equipments in each category
        public ActionResult EquipmentCountByCategory()
        {
            var equipmentCountByCategory = _equipmentRepository.GetEquipmentCountByCategory();
            return View(equipmentCountByCategory);
        }
    }
}