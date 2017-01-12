using ParkingDashboardSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingDashboardSample.Controllers
{
    public class GisSitesDataController : Controller
    {
        private ParkingDashboardSampleContext db = new ParkingDashboardSampleContext();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSites()
        {
            var rseult = db.SiteDatas.ToList();
            var json = Json(rseult, JsonRequestBehavior.AllowGet);
            //return json;
            return json;
        }
    }
}