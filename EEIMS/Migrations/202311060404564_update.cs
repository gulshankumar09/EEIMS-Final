namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Employees", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Employees", "Id");
            RenameColumn(table: "dbo.Employees", name: "ApplicationUser_Id", newName: "Id");
            AlterColumn("dbo.Employees", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "Id" });
            AlterColumn("dbo.Employees", "Id", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Employees", name: "Id", newName: "ApplicationUser_Id");
            AddColumn("dbo.Employees", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "ApplicationUser_Id");
        }
    }
}
