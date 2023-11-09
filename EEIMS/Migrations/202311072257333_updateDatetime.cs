namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "ExpectedReturnDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Assignments", "ActualReturnDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Requests", "RequestDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "RequestDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Assignments", "ActualReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Assignments", "ExpectedReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
