using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace ParkingDashboardSample.SqlServerNotifier
{
    public class NotifierEntity
    {
        ICollection<SqlParameter> m_SqlParameters = new List<SqlParameter>();

        public String SqlQuery { get; set; }

        public String SqlConnectionString { get; set; }

        public ICollection<SqlParameter> SqlParameters
        {
            get
            {
                return m_SqlParameters;
            }
            set
            {
                m_SqlParameters = value;
            }
        }

        public static NotifierEntity FromJson(String value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException($"NotifierEntity Value can not be null!");

            return new JavaScriptSerializer().Deserialize<NotifierEntity>(value);
        }
    }
}