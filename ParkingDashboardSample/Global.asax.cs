using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ParkingDashboardSample.Models;

namespace ParkingDashboardSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected String SqlConnectionString { get; set; }
        protected void Application_Start()
        {
            using (var context = new ParkingDeshboardContext())
                SqlConnectionString = context.Database.Connection.ConnectionString;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           

           /* if (!String.IsNullOrEmpty(SqlConnectionString))
                SqlDependency.Start(SqlConnectionString);*/
            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["ParkingDashboardSampleContext"].ConnectionString);
        }

        protected void Application_End()
        {
           /* if (!String.IsNullOrEmpty(SqlConnectionString))
                SqlDependency.Stop(SqlConnectionString);*/
            //SqlDependency.Stop(ConfigurationManager.ConnectionStrings["ParkingDashboardSampleContext"].ConnectionString);
        }
    }
}
