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
            try
            {
                var model = new Equipment
                {
                    Name = item.Name,
                    ItemModel = item.ItemModel,
                    SerialNumber = item.SerialNumber,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    EquipmentStatus = true,
                    PurchaseDate = DateTime.Now
                };
                Context.Equipments.Add(model);
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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

        // Show all Equipments
        IEnumerable<ViewEquipmentViewModel> IEquipmentRepository.GetAllEquipments()
        {
            var equipments = Context.Equipments.Include("CategoryReference");
            var mapEquip = equipments.Select(e => new ViewEquipmentViewModel
            {
                EquipmentId = e.EquipmentId,
                Name = e.Name,
                ItemModel = e.ItemModel,
                SerialNumber = e.SerialNumber,
                Description = e.Description,
                EquipmentStatus = e.EquipmentStatus,
                IsAssigned = e.IsAssigned,
                PurchaseDate = e.PurchaseDate,
                CategoryName = e.CategoryReference.CategoryName
            }).ToList();

            return mapEquip;
        }
        
        // Show Equipments which is not assigned
        IEnumerable<ViewEquipmentViewModel> IEquipmentRepository.GetNotAssinedEquipments()
        {
            var equipments = Context.Equipments.Include("CategoryReference").Where(e => e.IsAssigned == false);
            var mapEquip = equipments.Select(e => new ViewEquipmentViewModel
            {
                EquipmentId = e.EquipmentId,
                Name = e.Name,
                ItemModel = e.ItemModel,
                SerialNumber = e.SerialNumber,
                Description = e.Description,
                EquipmentStatus = e.EquipmentStatus,
                PurchaseDate = e.PurchaseDate,
                CategoryName = e.CategoryReference.CategoryName
            }).ToList();

            return mapEquip;
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

        IDictionary<string, int> IEquipmentRepository.GetEquipmentCountByCategory()
        {
            return Context.Categories
                .Select(c => new
                {
                    CategoryName = c.CategoryName,
                    EquipmentCount = c.Equipments.Count
                })
                .ToDictionary(c => c.CategoryName, c => c.EquipmentCount);
        }

        #endregion
    }
}