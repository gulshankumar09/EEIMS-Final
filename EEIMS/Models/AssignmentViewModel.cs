using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class createAssignmentViewModel
    {
        public int EmployeeId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
    }
}