namespace ParkingDashboardSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventsDatas",
                c => new
                    {
                        EventsDataId = c.Int(nullable: false, identity: true),
                        Site = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Severity = c.String(),
                    })
                .PrimaryKey(t => t.EventsDataId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventsDatas");
        }
    }
}
