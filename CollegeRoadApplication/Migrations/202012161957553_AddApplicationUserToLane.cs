namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserToLane : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lanes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Lanes", "ApplicationUserId");
            RenameColumn(table: "dbo.Lanes", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Lanes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lanes", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Lanes", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Lanes", "ApplicationUserId", c => c.Int());
            RenameColumn(table: "dbo.Lanes", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Lanes", "ApplicationUserId", c => c.Int());
            CreateIndex("dbo.Lanes", "ApplicationUser_Id");
        }
    }
}
