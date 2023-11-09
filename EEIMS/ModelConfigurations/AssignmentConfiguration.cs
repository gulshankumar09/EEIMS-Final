using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EEIMS.ModelConfigurations
{
    public class AssignmentConfiguration: EntityTypeConfiguration<Assignment>
    {
        public AssignmentConfiguration()
        {
            HasKey(a=> a.AssignId);

            HasRequired(r => r.Employee)
            .WithMany()
            .HasForeignKey(r => r.EmployeeId);


            HasRequired(r => r.Equipment)
            .WithMany()
            .HasForeignKey(r => r.EquipmentId);

            Property(e => e.ExpectedReturnDate).HasColumnType("datetime2");
            Property(e => e.ActualReturnDate).HasColumnType("datetime2");
        }
    }
}