using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string ItemModel { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }

        public bool EquipmentStatus { get; set; }  // 0: issue(not-in-use), 1: working(in-use)
        public bool IsAssigned { get; set; }   // 0: not-assigned, 1: assigned

        public DateTime PurchaseDate { get; set; }
        public DateTime DecommissionedDate { get; set; }
        public bool IsDecommissioned { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category CategoryReference { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        [NotMapped]
        public string IdAndName
        {
            get
            {
                return $"{EquipmentId} - {Name}";
            }
        }
    }
}