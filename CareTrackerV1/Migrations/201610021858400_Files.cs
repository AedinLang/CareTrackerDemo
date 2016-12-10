namespace CareTrackerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        CareGiverID = c.Int(nullable: false),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.CareGiver", t => t.CareGiverID, cascadeDelete: true)
                .Index(t => t.CareGiverID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "CareGiverID", "dbo.CareGiver");
            DropIndex("dbo.File", new[] { "CareGiverID" });
            DropTable("dbo.File");
        }
    }
}
