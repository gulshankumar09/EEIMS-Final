using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class FirstTimeAddEmployeeViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Organization { get; set; }
    }

    public class GetAllEmployeeVewModel
    {

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }
    }

    public class UpdateEmployeeViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Organization { get; set; }
    }
}