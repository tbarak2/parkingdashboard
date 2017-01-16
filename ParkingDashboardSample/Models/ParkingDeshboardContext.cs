using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParkingDashboardSample.Models
{
    public class ParkingDeshboardContext:DbContext
    {
        const string DEFAULT_CONNECTION_NAME = "ParkingDashboardSampleContext";

        #region Ctor
        public ParkingDeshboardContext() : this(DEFAULT_CONNECTION_NAME)
        {
        }

        public ParkingDeshboardContext(string sqlConnectionName) : base($"Name={sqlConnectionName}")
        {
            
        }

        #endregion

        #region Collections Definitions

        public DbSet<SiteData> SiteData { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteData>()
                        .ToTable("SiteData", "dbo")
                        .HasKey(t => t.SiteDataId);
        }

    }
}