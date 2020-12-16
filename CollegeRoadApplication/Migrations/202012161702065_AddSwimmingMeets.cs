namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSwimmingMeets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SwimmingMeets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PoolSizeTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                        Vanue = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PoolSizeTypes", t => t.PoolSizeTypeId, cascadeDelete: true)
                .Index(t => t.PoolSizeTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SwimmingMeets", "PoolSizeTypeId", "dbo.PoolSizeTypes");
            DropIndex("dbo.SwimmingMeets", new[] { "PoolSizeTypeId" });
            DropTable("dbo.SwimmingMeets");
        }
    }
}
