namespace CareTrackerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CareGiver",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        Surname = c.String(nullable: false, maxLength: 40),
                        AddressLine1 = c.String(maxLength: 40),
                        AddressLine2 = c.String(maxLength: 40),
                        Region = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        Mobile = c.String(maxLength: 20),
                        Qualifications = c.String(),
                        CV = c.String(),
                        References = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        Surname = c.String(nullable: false, maxLength: 40),
                        DOB = c.DateTime(),
                        AddressLine1 = c.String(nullable: false, maxLength: 40),
                        AddressLine2 = c.String(maxLength: 40),
                        Region = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        Medication = c.String(nullable: false),
                        HealthSummary = c.String(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        NextOfKinID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Doctor", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.NextOfKin", t => t.NextOfKinID, cascadeDelete: true)
                .Index(t => t.DoctorID)
                .Index(t => t.NextOfKinID);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(nullable: false),
                        AddressLine3 = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Mobile = c.String(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NextOfKin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(nullable: false),
                        AddressLine3 = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Mobile = c.String(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CareGiverID = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        Details = c.String(nullable: false),
                        AlertType = c.String(),
                        AlertDetails = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CareGiver", t => t.CareGiverID, cascadeDelete: true)
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.CareGiverID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.VisitTask",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ClientCareGiver",
                c => new
                    {
                        ClientID = c.Int(nullable: false),
                        CareGiverID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientID, t.CareGiverID })
                .ForeignKey("dbo.Client", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.CareGiver", t => t.CareGiverID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.CareGiverID);
            
            CreateTable(
                "dbo.VisitTaskVisit",
                c => new
                    {
                        VisitTask_ID = c.Int(nullable: false),
                        Visit_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VisitTask_ID, t.Visit_ID })
                .ForeignKey("dbo.VisitTask", t => t.VisitTask_ID, cascadeDelete: true)
                .ForeignKey("dbo.Visit", t => t.Visit_ID, cascadeDelete: true)
                .Index(t => t.VisitTask_ID)
                .Index(t => t.Visit_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CareGiver", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VisitTaskVisit", "Visit_ID", "dbo.Visit");
            DropForeignKey("dbo.VisitTaskVisit", "VisitTask_ID", "dbo.VisitTask");
            DropForeignKey("dbo.Visit", "ClientID", "dbo.Client");
            DropForeignKey("dbo.Visit", "CareGiverID", "dbo.CareGiver");
            DropForeignKey("dbo.Client", "NextOfKinID", "dbo.NextOfKin");
            DropForeignKey("dbo.Client", "DoctorID", "dbo.Doctor");
            DropForeignKey("dbo.ClientCareGiver", "CareGiverID", "dbo.CareGiver");
            DropForeignKey("dbo.ClientCareGiver", "ClientID", "dbo.Client");
            DropIndex("dbo.VisitTaskVisit", new[] { "Visit_ID" });
            DropIndex("dbo.VisitTaskVisit", new[] { "VisitTask_ID" });
            DropIndex("dbo.ClientCareGiver", new[] { "CareGiverID" });
            DropIndex("dbo.ClientCareGiver", new[] { "ClientID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Visit", new[] { "ClientID" });
            DropIndex("dbo.Visit", new[] { "CareGiverID" });
            DropIndex("dbo.Client", new[] { "NextOfKinID" });
            DropIndex("dbo.Client", new[] { "DoctorID" });
            DropIndex("dbo.CareGiver", new[] { "UserID" });
            DropTable("dbo.VisitTaskVisit");
            DropTable("dbo.ClientCareGiver");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.VisitTask");
            DropTable("dbo.Visit");
            DropTable("dbo.NextOfKin");
            DropTable("dbo.Doctor");
            DropTable("dbo.Client");
            DropTable("dbo.CareGiver");
        }
    }
}
