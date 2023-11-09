using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace EEIMS.ModelConfigurations
{
    public class EquipmentConfiguration: EntityTypeConfiguration<Equipment>
    {
        public EquipmentConfiguration()
        {
            HasKey(e => e.EquipmentId);
            Property(e => e.PurchaseDate).HasColumnType("datetime2");
            Property(e => e.DecommissionedDate).HasColumnType("datetime2");
        }
    }
}