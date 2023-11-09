using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class AddEquipmentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Item Model")]
        public string ItemModel { get; set; }

        
        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Equipment Status")]
        public bool EquipmentStatus { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateEquipmentViewModel
    {
        public int EquipmentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Item Model")]
        public string ItemModel { get; set; }


        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Equipment Status")]
        public bool EquipmentStatus { get; set; }
        
        public int CategoryId { get; set; }
    }

    public class ViewEquipmentViewModel
    {
        [Display(Name = "Equip Id")]
        public int EquipmentId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Model")]
        public string ItemModel { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }
        public string Description { get; set; }

        [Display(Name = "Status")]
        public bool EquipmentStatus { get; set; }

        [Display(Name = "Assigned")]
        public bool IsAssigned { get; set; }

        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Decommissioned Date")]
        public DateTime DecommissionedDate { get; set; }

        [Display(Name = "Decommissioned")]
        public bool IsDecommissioned { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public string IdAndName
        {
            get
            {
                return $"{EquipmentId} - {Name}";
            }
        }
    }
}