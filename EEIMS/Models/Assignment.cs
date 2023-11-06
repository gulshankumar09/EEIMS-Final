using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignId { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public AssignStatus assignStatus { get; set; }

        public virtual Equipment Equipment { get; set; }
        public int EquipmentId { get; set; }

        public virtual Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }

    public enum AssignStatus
    {
        assigned,
        returned,
        overdue
    }
}