namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFamilyGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FamilyGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "FamilyGroupId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "FamilyGroupId");
            AddForeignKey("dbo.AspNetUsers", "FamilyGroupId", "dbo.FamilyGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "FamilyGroupId", "dbo.FamilyGroups");
            DropIndex("dbo.AspNetUsers", new[] { "FamilyGroupId" });
            DropColumn("dbo.AspNetUsers", "FamilyGroupId");
            DropTable("dbo.FamilyGroups");
        }
    }
}
