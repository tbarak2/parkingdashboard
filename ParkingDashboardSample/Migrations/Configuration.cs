namespace ParkingDashboardSample.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ParkingDashboardSample.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ParkingDashboardSample.Models.ParkingDashboardSampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParkingDashboardSample.Models.ParkingDashboardSampleContext context)
        {
            context.SiteDatas.AddOrUpdate(p => p.SiteName,
       new SiteData
       {
           Faults = 2,
           SiteDataId = 1,
           SiteName = "Mexico",
           Status = "Faults",
           Warnings = 0
       },

       new SiteData
       {
           Faults = 0,
           SiteDataId = 1,
           SiteName = "Hoboken",
           Status = "Warning",
           Warnings = 1
       }
        );
        }
    }
}
