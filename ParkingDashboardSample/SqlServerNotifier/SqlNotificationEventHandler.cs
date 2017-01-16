using System.Data.SqlClient;

namespace ParkingDashboardSample.SqlServerNotifier
{
    public delegate void SqlNotificationEventHandler(object sender, SqlNotificationEventArgs e);
}