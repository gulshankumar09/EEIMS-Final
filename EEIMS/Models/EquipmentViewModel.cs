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
        public bool EquipmentStatus { get; set; } // 0: not-in-use, 1: in-use
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
}