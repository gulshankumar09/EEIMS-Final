namespace EEIMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignId = c.Int(nullable: false, identity: true),
                        AssignDate = c.DateTime(nullable: false),
                        ExpectedReturnDate = c.DateTime(nullable: false),
                        ActualReturnDate = c.DateTime(nullable: false),
                        assignStatus = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(),
                        Equipment_EquipmentId = c.Int(),
                    })
                .PrimaryKey(t => t.AssignId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Equipments", t => t.Equipment_EquipmentId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .Index(t => t.EquipmentId)
                .Index(t => t.EmployeeId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Equipment_EquipmentId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Designation = c.String(maxLength: 50),
                        Department = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 20),
                        Email = c.String(maxLength: 100),
                        Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OTP = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Comments = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CategoryId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ItemModel = c.String(),
                        SerialNumber = c.String(),
                        Description = c.String(),
                        EquipmentStatus = c.Boolean(nullable: false),
                        IsAssigned = c.Boolean(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Assignments", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Assignments", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Requests", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Equipments", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Assignments", "Equipment_EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Assignments", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Equipments", new[] { "CategoryId" });
            DropIndex("dbo.Requests", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "Category_CategoryId" });
            DropIndex("dbo.Requests", new[] { "CategoryId" });
            DropIndex("dbo.Requests", new[] { "EmployeeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Employees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Assignments", new[] { "Equipment_EquipmentId" });
            DropIndex("dbo.Assignments", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Assignments", new[] { "EmployeeId" });
            DropIndex("dbo.Assignments", new[] { "EquipmentId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Equipments");
            DropTable("dbo.Categories");
            DropTable("dbo.Requests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Employees");
            DropTable("dbo.Assignments");
        }
    }
}
