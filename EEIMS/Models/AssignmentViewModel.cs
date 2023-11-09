using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class CreateAssignmentViewModel
    {
        public int EmployeeId { get; set; }
        public List<int> EquipmentIds { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }

    public class UpdateAssignmentsViewModel
    {
        public int AssignId { get; set; }
        public int EmployeeId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

    }

    public class GetAllAssignmentViewModel
    {
        public int AssignId { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public AssignStatus assignStatus { get; set; }

        public virtual Equipment Equipment { get; set; }
        public int EquipmentId { get; set; }

        public int EmployeeId { get; set; }
    }
}