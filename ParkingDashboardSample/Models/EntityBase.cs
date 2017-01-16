using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ParkingDashboardSample.Models
{
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class EntityBase
    {
    }
}