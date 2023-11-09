using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Models
{
    public class AddRoleToUserViewModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class EmployeeRoleViewModel
    {
        public string Id { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }



    }


}