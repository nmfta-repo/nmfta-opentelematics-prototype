using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class DriverBreakRuleModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime {get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset activeFrom { get; set; }
        public DateTimeOffset activeTo { get; set; }
        public string country { get; set; }
        public string region { get; set; }

        public DriverBreakRuleModel(DriverBreakRule item, string ProviderId)
        {
            id = item.Id;
            providerId = ProviderId;
            serverTime = DateTime.UtcNow;
            driverId = item.driverId;
            activeFrom = item.activeFrom;
            activeTo = item.activeTo;
            country = item.country;
            region = item.region;
        }
    }
}
