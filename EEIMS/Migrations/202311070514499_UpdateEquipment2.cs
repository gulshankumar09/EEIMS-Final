namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEquipment2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipments", "PurchaseDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Equipments", "DecommissionedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equipments", "DecommissionedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Equipments", "PurchaseDate", c => c.DateTime(nullable: false));
        }
    }
}
