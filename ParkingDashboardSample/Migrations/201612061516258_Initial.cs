namespace ParkingDashboardSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteDatas",
                c => new
                    {
                        SiteDataId = c.Int(nullable: false, identity: true),
                        SiteName = c.String(),
                        Status = c.String(),
                        Faults = c.Int(nullable: false),
                        Warnings = c.Int(nullable: false),
                        Self = c.String(),
                    })
                .PrimaryKey(t => t.SiteDataId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SiteDatas");
        }
    }
}
