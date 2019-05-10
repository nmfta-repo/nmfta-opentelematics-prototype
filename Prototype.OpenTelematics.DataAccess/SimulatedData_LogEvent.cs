using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class SimulatedData_LogEvent
    {
        public int Id { get; set; }
        public int driverId { get; set; }
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
        public int createdOrder { get; set; }
    }
}
