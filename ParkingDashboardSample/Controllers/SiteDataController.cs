using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using ParkingDashboardSample.App_Start;
using ParkingDashboardSample.Hubs;
using ParkingDashboardSample.Models;
using ParkingDashboardSample.SqlServerNotifier;

namespace ParkingDashboardSample.Controllers
{
    public class SiteDataController : Controller, IEventProcessor
    {
        private ParkingDashboardSampleContext db = new ParkingDashboardSampleContext();
        private bool isTableNeedToUpdated = false;
        private int SiteId { set; get; }
        //private ParkingDashboardSampleContext db = new ParkingDashboardSampleContext();
        SiteDataRepository objRepo = new SiteDataRepository();
        /* public SiteDataController()
         {
             Task.Factory.StartNew(() =>
             {
                 string eventHubConnectionString =
                     "Endpoint=sb://parkingdashboardeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wlcP65lCLO3gzJRfmwMR+UfgqpQ7KLS5IEzdvOs975k=";
                 string eventHubName = "siteevent";
                 string storageAccountName = "parkingstorageaccount";
                 string storageAccountKey =
                     "4J2QCIVa6GKv4OFQj8DhU95fzGeewHKIpoHPgdS/w9kUhr5JwF/jzE11sYdpBcnzpSlgM21mn5V3J8+mJignqQ==";
                 string storageConnectionString =
                     string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName,
                         storageAccountKey);
                 ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Https;
                 string eventProcessorHostName = Guid.NewGuid().ToString();
                 EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName,
                     EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
                 //Console.WriteLine("Registering EventProcessor...");
                 var options = new EventProcessorOptions
                 {
                     InitialOffsetProvider = (partitionId) => DateTime.UtcNow
                 };
                 options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
                  eventProcessorHost.RegisterEventProcessorAsync<SiteDataController>(options).Wait();
             });
         }*/

        public async Task<ActionResult> PartialTable()
        {
            /*if (isTableNeedToUpdated)
            {*/
            var t = db.SiteDatas.ToList();
            isTableNeedToUpdated = false;
            /*return PartialView("PartialViewSites", t);*/
            /*var s = objRepo.GetData();*/
            var collection = db.SiteDatas;
            ViewBag.NotifierEntity = db.GetNotifierEntity<SiteData>(collection).ToJson();
            
            
            return PartialView("PartialViewSites", await collection.ToListAsync());

            /* }
             return PartialView("PartialViewSites",*/
        }

        // GET: SiteData
        public async Task<ActionResult> Index()
        {
            var collection = db.SiteDatas;
            ViewBag.NotifierEntity = db.GetNotifierEntity<SiteData>(collection).ToJson();
            //Console.WriteLine("Receiving. Press enter key to stop worker.");
            //Console.ReadLine();
            // eventProcessorHost.UnregisterEventProcessorAsync().Wait();

            //return View(db.SiteDatas.ToList());
            return View(await collection.ToListAsync());
        }

        // GET: SiteData/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                ViewBag.id = id;
                SiteId = (int)id;
            }
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             SiteData siteData = db.SiteDatas.Find(id);
             if (siteData == null)
             {
                 return HttpNotFound();
             }
             return View(siteData);*/
            return View();
        }

        //API:/SiteData/GetSitesDetails/5
        public JsonResult GetSitesDetails(int? id)
        {
            SiteData siteData = db.SiteDatas.Find(id);
            var json = Json(siteData, JsonRequestBehavior.AllowGet);
            //return json;
            return json;
        }

        //API:/SiteData/GetSitesEvents/id
        public JsonResult GetSitesEvents(String id)
        {
            List<EventsData> eventData = db.EventsDatas.Where(s => s.Site == id).ToList();
            var json = Json(eventData, JsonRequestBehavior.AllowGet);
            //return json;
            return json;
        }

        // GET: SiteData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteDataId,SiteName,Status,Faults,Warnings,Self")] SiteData siteData)
        {
            if (ModelState.IsValid)
            {
                db.SiteDatas.Add(siteData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteData);
        }

        // GET: SiteData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteData siteData = db.SiteDatas.Find(id);
            if (siteData == null)
            {
                return HttpNotFound();
            }
            return View(siteData);
        }

        // POST: SiteData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteDataId,SiteName,Status,Faults,Warnings,Self")] SiteData siteData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteData);
        }

        // GET: SiteData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteData siteData = db.SiteDatas.Find(id);
            if (siteData == null)
            {
                return HttpNotFound();
            }
            return View(siteData);
        }


        // POST: SiteData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteData siteData = db.SiteDatas.Find(id);
            db.SiteDatas.Remove(siteData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Test()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            hubContext.Clients.All.NewMessage("all", "update");
            return Content("Hi there!");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Task OpenAsync(PartitionContext context)
        {

            /* Console.WriteLine("SimpleEventProcessor initialized.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset);
             this.checkpointStopWatch = new Stopwatch();
             this.checkpointStopWatch.Start();*/
            return Task.FromResult<object>(null);
        }

        async Task IEventProcessor.ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (EventData eventData in messages)
            {
                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                var siteEventData = JsonConvert.DeserializeObject<EventsData>(data);
                /* Console.WriteLine(string.Format("Message received.  Partition: '{0}', Data: '{1}'",
                     context.Lease.PartitionId, data));*/
                var siteData = db.SiteDatas.First(s => s.SiteName == siteEventData.Site);
                UpdateSiteEvent(siteData, siteEventData);
                //Index();
            }
            if (messages.Count() > 0)
            {
                await context.CheckpointAsync();
            }

            //Call checkpoint every 5 minutes, so that worker can resume processing from 5 minutes back if it restarts.
            /* if (this.checkpointStopWatch.Elapsed > TimeSpan.FromMinutes(5))
             {
                 await context.CheckpointAsync();
                 this.checkpointStopWatch.Restart();
             }*/
        }

        private void UpdateSiteEvent(SiteData siteData, EventsData siteEventData)
        {
            if (siteData != null)
            {
                var ischanged = false;
                if (siteEventData.Severity == "Fault")
                {
                    siteData.Faults++;
                    ischanged = true;
                }
                else if (siteEventData.Severity == "Warning")
                {
                    siteData.Warnings++;
                    ischanged = true;
                }
                if (ischanged)
                    db.Entry(siteData).State = EntityState.Modified;
            }

            db.EventsDatas.Add(siteEventData);
            db.SaveChanges();
            isTableNeedToUpdated = true;
        }

        async Task IEventProcessor.CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("Processor Shutting Down. Partition '{0}', Reason: '{1}'.", context.Lease.PartitionId, reason);
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }
    }
}
