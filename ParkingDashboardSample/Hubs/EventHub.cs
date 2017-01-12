using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ParkingDashboardSample.Hubs
{
    public class EventHub:Hub
    {
        public void GetEvents(string client, string message)
        {
            Clients.All.NewMessage(client, message);
        }

        public void Heartbeat()
        {
            Clients.All.heartbeat();
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }
    }
}