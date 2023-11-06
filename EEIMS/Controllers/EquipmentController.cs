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
            var temp = _equipmentRepository.GetAllEquipments();
            return View(temp);
        }

        [HttpGet]
        public ActionResult AddNewEquipment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewEquipment(AddEquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _equipmentRepository.AddEquipment(model);
                return RedirectToAction("Index", "Equipment");
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
    }
}