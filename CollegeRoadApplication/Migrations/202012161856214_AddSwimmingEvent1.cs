namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSwimmingEvent1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SwimmingEvents", "Round", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SwimmingEvents", "Round", c => c.String(nullable: false));
        }
    }
}
