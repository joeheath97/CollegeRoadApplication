namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPoolSizeType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PoolSizeTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        PoolSize = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PoolSizeTypes");
        }
    }
}
