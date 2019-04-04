using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DriverPerformanceSummary
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset eventStart { get; set; }
        public DateTimeOffset eventEnd { get; set; }
        public decimal? distance { get; set; }
        public decimal? fuel { get; set; }
        public decimal? cruiseTime { get; set; }
        public decimal? engineLoadPercent { get; set; }
        public decimal? overRpmTime { get; set; }
        public int? brakeEvents { get; set; }
    }
}