using System.Data;
using System.Data.SqlClient;

namespace ParkingDashboardSample.SqlServerNotifier
{
    public class SqlDependencyRegister
    {
        public event SqlNotificationEventHandler SqlNotification;

        readonly NotifierEntity m_NotificationEntity;

        internal SqlDependencyRegister(NotifierEntity notificationEntity)
        {
            m_NotificationEntity = notificationEntity;
            RegisterForNotifications();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security",
             "CA2100:Review SQL queries for security vulnerabilities")]
        void RegisterForNotifications()
        {
            using (var sqlConnection = new SqlConnection(m_NotificationEntity.SqlConnectionString))
            using (var sqlCommand = new SqlCommand(m_NotificationEntity.SqlQuery, sqlConnection))
            {
                foreach (var sqlParameter in m_NotificationEntity.SqlParameters)
                    sqlCommand.Parameters.Add(sqlParameter);

                sqlCommand.Notification = null;
                var sqlDependency = new SqlDependency(sqlCommand);
                sqlDependency.OnChange += OnSqlDependencyChange;
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        private void OnSqlDependencyChange(object sender, SqlNotificationEventArgs e)
        {
            if (SqlNotification != null)
                SqlNotification(sender, e);
            RegisterForNotifications();
        }
    }
}