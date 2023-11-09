using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class CreateNewGetViewModel
    {
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }


        public string Comments { get; set; }
    }
    public class CreateNewRequestViewModel
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public string Comments { get; set; }
    }

    public class ReviewRequestsViewModel
    {
        [Display(Name = "ID")]
        public int RequestId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Display(Name = "Status")]
        public RequestStatus Status { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Name")]
        public string RequestedByEmployee { get; set; }

        [Display(Name = "Item")]
        public string Category { get; set; }

        [Display(Name = "Emp ID")]
        public int EmployeeId { get; set; }
    }
}