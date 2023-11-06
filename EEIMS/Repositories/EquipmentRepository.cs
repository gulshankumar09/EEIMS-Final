using EEIMS.Functionalities;
using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        #region Private Fields
        private ApplicationDbContext _context;

        public EquipmentRepository()
        {

        }

        public EquipmentRepository(ApplicationDbContext applicationDbContext)
        {
            Context = applicationDbContext;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? new ApplicationDbContext();
            }
            private set
            {
                _context = value;
            }
        }
        #endregion

        #region Public Methods

        int IEquipmentRepository.AddEquipment(AddEquipmentViewModel item)
        {
            var model = new Equipment
            {
                Name = item.Name,
                ItemModel = item.ItemModel,
                SerialNumber = item.SerialNumber,
                Description = item.Description,
                EquipmentStatus = item.EquipmentStatus,
                CategoryId = item.CategoryId,
                PurchaseDate= DateTime.Now
            };
            Context.Equipments.Add(model);
            return Context.SaveChanges();
        }

        UpdateEquipmentViewModel IEquipmentRepository.GetEquipment(int id)
        {
            return Context.Equipments.Where(e => e.EquipmentId == id).Select(e => new UpdateEquipmentViewModel
            {
                EquipmentId = e.EquipmentId,
                Name = e.Name,
                ItemModel = e.ItemModel,
                SerialNumber = e.SerialNumber,
                Description = e.Description,
                EquipmentStatus = e.EquipmentStatus,
                CategoryId = e.CategoryId
            }).FirstOrDefault();
        }

        IEnumerable<Equipment> IEquipmentRepository.GetAllEquipments()
        {
            return Context.Equipments.ToList();
        }

        int IEquipmentRepository.RemoveEquipment(int id)
        {
            Context.Equipments.Remove(Context.Equipments.Where(e => e.EquipmentId==id).FirstOrDefault());
            return Context.SaveChanges();
        }

        int IEquipmentRepository.UpdateEquipment(UpdateEquipmentViewModel item)
        {
            var equip = Context.Equipments.Where(e => e.EquipmentId == item.EquipmentId).FirstOrDefault();
            equip.Name = item.Name;
            equip.ItemModel = item.ItemModel;
            equip.SerialNumber = item.SerialNumber;
            equip.Description = item.Description;
            equip.CategoryId = item.CategoryId;

            return Context.SaveChanges();

        }


        #endregion
    }
}