using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using  Microsoft.ServiceBus.Messaging.Configuration;

namespace WebJob1
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var eventHubConfig = new EventHubConfiguration();
           /* string eventHubName = "MyHubName";
            
            eventHubConfig.AddReceiver(eventHubName, "Endpoint=sb://test.servicebus.windows.net/;SharedAccessKeyName=ReceiveRule;SharedAccessKey=yyyyyyy");
*/            var config = new JobHostConfiguration();
            config.useEv
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
