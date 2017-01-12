using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace ParkingDashboardSample.Models
{
    public class EventsData
    {
        public string Site { get; set; }
        public int EventsDataId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Severity { get; set; }
    }
}