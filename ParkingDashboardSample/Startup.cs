using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ParkingDashboardSample.Startup))]

namespace ParkingDashboardSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            /*string connectionString = "Endpoint=sb://tablestoragenotification.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=D9IEq56i+7XVZoxs8lpZu3f+wE/q0N4s32aMBoRJXeY=";
            GlobalHost.DependencyResolver.UseServiceBus(connectionString, "YourAppName");*/
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
