using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class DriverPerformanceSummaryModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset eventStart { get; set; }
        public DateTimeOffset eventEnd { get; set; }
        public decimal? distance { get; set; }
        public decimal? fuel { get; set; }
        public decimal? cruiseTime { get; set; }
        public decimal? engineLoadPercent { get; set; }
        public decimal? overRpmTime { get; set; }
        public int? brakeEvents { get; set; }

        public DriverPerformanceSummaryModel(DriverPerformanceSummary item, string provider)
        {
            id = item.Id;
            providerId = provider;
            serverTime = DateTimeOffset.UtcNow;
            driverId = item.driverId;
            eventEnd = item.eventEnd;
            eventStart = item.eventStart;
            distance = item.distance;
            fuel = item.fuel;
            cruiseTime = item.cruiseTime;
            engineLoadPercent = item.engineLoadPercent;
            overRpmTime = item.overRpmTime;
            brakeEvents = item.brakeEvents;
        }
    }
}
