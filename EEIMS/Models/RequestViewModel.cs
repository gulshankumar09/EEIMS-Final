using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class CreateNewRequestViewModel
    {
        public int EmployeeId { get; set; }
        public int CategoryId { get; set; }
        public string Comments { get; set; }
    }

    public class ReviewRequestsViewModel
    {
        public int RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public RequestStatus Status { get; set; }
        public string Comments { get; set; }
        public string RequestedBy { get; set; }
        public string Category { get; set; }
    }
}