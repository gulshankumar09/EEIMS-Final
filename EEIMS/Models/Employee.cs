using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsVerified { get; set; }   // this employee is not verified
        public string Organization { get; set; }

        [StringLength(128)]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}