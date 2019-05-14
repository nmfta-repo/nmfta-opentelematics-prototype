using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class DriverWaiverModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public Guid driverId { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public DateTimeOffset waiverDay { get; set; }

        public DriverWaiverModel(DriverWaiver item, string ProviderId)
        {
            id = item.Id;
            providerId = ProviderId;
            serverTime = DateTime.UtcNow;
            driverId = item.driverId;
            country = item.country;
            region = item.region;
            waiverDay = item.waiverDay;
        }
    }
}
