using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Prototype.OpenTelematics.DataAccess
{
    public partial class LogEvent
    {
        public LogEvent()
        {
            annotations = new List<LogEventAnnotation>();
        }

        public Guid Id { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public string coDrivers { get; set; }
        public decimal distanceLastValid { get; set; }
        public DateTimeOffset? editDateTime { get; set; }
        public Guid locationId { get; set; }
        public string origin { get; set; }
        public Guid? parentId { get; set; }
        public int sequence { get; set; }
        public string state { get; set; }
        public string eventType { get; set; }
        public int? certificationCount { get; set; }
        public DateTimeOffset? verifyDateTime { get; set; }
        public int multidayBasis { get; set; }
        public string comment { get; set; }
        public string eventDataChecksum { get; set; }

        [ForeignKey("locationId")]
        public virtual Location location { get; set; }

        public virtual List<LogEventAnnotation> annotations { get; set; }
    }
}