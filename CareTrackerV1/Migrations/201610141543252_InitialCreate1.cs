namespace CareTrackerV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Visit", "EndTime", c => c.DateTime());
            DropColumn("dbo.Visit", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visit", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Visit", "EndTime");
            DropColumn("dbo.Visit", "StartTime");
        }
    }
}
