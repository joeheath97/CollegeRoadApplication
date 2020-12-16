namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSwimmingEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SwimmingEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SwimmingMeetId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        AgeRange = c.String(nullable: false),
                        Distance = c.String(nullable: false),
                        Stroke = c.String(nullable: false),
                        Round = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SwimmingMeets", t => t.SwimmingMeetId, cascadeDelete: true)
                .Index(t => t.SwimmingMeetId);
            
            CreateTable(
                "dbo.Lanes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(),
                        SwimmingEventId = c.Int(nullable: false),
                        Result = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.SwimmingEvents", t => t.SwimmingEventId, cascadeDelete: true)
                .Index(t => t.SwimmingEventId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SwimmingEvents", "SwimmingMeetId", "dbo.SwimmingMeets");
            DropForeignKey("dbo.Lanes", "SwimmingEventId", "dbo.SwimmingEvents");
            DropForeignKey("dbo.Lanes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Lanes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Lanes", new[] { "SwimmingEventId" });
            DropIndex("dbo.SwimmingEvents", new[] { "SwimmingMeetId" });
            DropTable("dbo.Lanes");
            DropTable("dbo.SwimmingEvents");
        }
    }
}
