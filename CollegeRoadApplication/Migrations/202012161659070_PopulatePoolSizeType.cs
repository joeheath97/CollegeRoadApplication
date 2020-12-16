namespace CollegeRoadApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePoolSizeType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PoolSizeTypes (Id, PoolSize) VALUES (1, '25 Meters')");
            Sql("INSERT INTO PoolSizeTypes (Id, PoolSize) VALUES (2, '33.3 Meters')");
            Sql("INSERT INTO PoolSizeTypes (Id, PoolSize) VALUES (3, '50 Meters')");
        }
        
        public override void Down()
        {
        }
    }
}
