using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace ParkingDashboardSample.Models
{
    public class SiteData
    {
        public int SiteDataId { get; set; }
        public string SiteName{ get; set; }
        public string Status { get; set; }
        public int Faults { get; set; }
        public int Warnings { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
               "api/sitedata/{0}", this.SiteDataId);
            }
            set { }
        }
    }
}