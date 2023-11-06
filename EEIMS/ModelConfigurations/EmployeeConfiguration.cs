using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EEIMS.ModelConfigurations
{
    public class EmployeeConfiguration: EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employees");
            HasKey(u => u.EmployeeId);
            Property(u => u.FirstName).HasMaxLength(100);
            Property(u => u.LastName).HasMaxLength(100);
            Property(u => u.PhoneNumber).HasMaxLength(20);
            Property(u => u.Department).HasMaxLength(50);
            Property(u => u.Designation).HasMaxLength(50);
            Property(u => u.Email).HasMaxLength(100);
        }
    }
}