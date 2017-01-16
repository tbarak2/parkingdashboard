using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ParkingDashboardSample.Hubs;

namespace ParkingDashboardSample.Models
{
    public class SiteDataRepository
    {
        public IEnumerable<SiteData> GetData()
        {
            using (
                var connection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["ParkingDashboardSampleContext"].ConnectionString))
            {
                connection.Open();

                using (
                    SqlCommand command =
                        new SqlCommand(
                            @"SELECt [SiteDataId],[SiteName],[Status],[Faults],[Warnings],[Self] FROM [dbo].[SiteData]",
                            connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new SiteData()
                            {
                                SiteName = x.GetString(1),
                                SiteDataId = x.GetInt32(0),
                                Status = x.GetString(2),
                                Faults = x.GetInt32(3),
                                Warnings = x.GetInt32(4),
                                Self = x.GetString(5)

                            }).ToList();

                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            EventHub.Show();
        }
    }
}