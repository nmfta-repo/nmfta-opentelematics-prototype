using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class VehiclePerformanceThresholdModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public DateTimeOffset activeFrom { get; set; }
        public DateTimeOffset? activeTo { get; set; }
        public int? rpmOverValue { get; set; }
        public int? overSpeedValue { get; set; }
        public int? excessSpeedValue { get; set; }
        public int? longIdleValue { get; set; }
        public int? hiThrottleValue { get; set; }

        public VehiclePerformanceThresholdModel(VehiclePerformanceThreshold item, string provider)
        {
            id = item.Id;
            providerId = provider;
            serverTime = DateTimeOffset.UtcNow;
            activeFrom = item.activeFrom;
            activeTo = item.activeTo;
            rpmOverValue = item.rpmOverValue;
            overSpeedValue = item.overSpeedValue;
            excessSpeedValue = item.excessSpeedValue;
            longIdleValue = item.longIdleValue;
            hiThrottleValue = item.hiThrottleValue;
        }
    }
}
