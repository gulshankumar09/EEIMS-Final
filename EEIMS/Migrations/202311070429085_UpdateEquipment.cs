namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "DecommissionedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Equipments", "IsDecommissioned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "IsDecommissioned");
            DropColumn("dbo.Equipments", "DecommissionedDate");
        }
    }
}
