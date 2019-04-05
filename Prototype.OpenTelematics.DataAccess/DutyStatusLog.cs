using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DutyStatusLog
    {
        public Guid Id { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public decimal distanceLastValid { get; set; }
        public DateTimeOffset editDateTime { get; set; }
        public string eventRecordStatus { get; set; }
        public string eventType { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string malfunction { get; set; }
        public string origin { get; set; }
        public Guid parentId { get; set; }
        public int sequence { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public DateTimeOffset? verifyDateTime { get; set; }
        public int multidayBasis { get; set; }
        public string outputFileComment { get; set; }
        public string eventDataChecksum { get; set; }
    }
}