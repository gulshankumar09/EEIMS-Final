using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EEIMS.ModelConfigurations
{
    public class RequestConfiguration: EntityTypeConfiguration<Request>
    {
        public RequestConfiguration()
        {
            HasRequired(r => r.RequestedBy)
            .WithMany()
            .HasForeignKey(r => r.EmployeeId);

           
            HasRequired(r => r.Category)
            .WithMany()
            .HasForeignKey(r => r.CategoryId);

            Property(e => e.RequestDate).HasColumnType("datetime2");
        }
    }
}