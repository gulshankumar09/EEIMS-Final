namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "Organization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Organization");
            DropColumn("dbo.Employees", "IsVerified");
        }
    }
}
