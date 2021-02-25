namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactToFamilyGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FamilyGroups", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FamilyGroups", "Contact");
        }
    }
}
