using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.OpenTelematics.Models
{
    public class LogEventModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public DateTimeOffset? dateTime { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public List<LogEventAnnotationModel> annotations { get; set; }
        public LocationModel location { get; set; }
        public List<Guid> coDrivers { get; set; }
        public decimal distanceLastValid { get; set; }
        public DateTimeOffset? editDateTime { get; set; }
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

        public LogEventModel(LogEvent log, string providerId)
        {
            this.id = log.Id;
            this.providerId = providerId;
            this.driverId = log.driverId;
            this.coDrivers = new List<Guid>();
            if (!string.IsNullOrEmpty(log.coDrivers))
            {
                List<string> otherDrivers = log.coDrivers.Split(',').ToList();
                foreach (string driver in otherDrivers)
                {
                    if (Guid.TryParse(driver, out var drvId))
                        coDrivers.Add(drvId);
                }
            }

            this.dateTime = log.dateTime;
            this.serverTime = DateTimeOffset.UtcNow;
            this.editDateTime = log.editDateTime;
            this.distanceLastValid = log.distanceLastValid;
            this.editDateTime = log.editDateTime;
            this.eventDataChecksum = log.eventDataChecksum;
            this.eventType = log.eventType;
            this.multidayBasis = log.multidayBasis;
            this.origin = log.origin;
            this.comment = log.comment;
            this.parentId = log.parentId;
            this.sequence = log.sequence;
            this.state = log.state;
            this.certificationCount = log.certificationCount;
            this.vehicleId = log.vehicleId;
            this.verifyDateTime = log.verifyDateTime;
            this.annotations = new List<LogEventAnnotationModel>();
            foreach (LogEventAnnotation annotation in log.annotations)
                this.annotations.Add(new LogEventAnnotationModel(annotation, providerId));
            if (log.location != null)
                this.location = new LocationModel(log.location);
        }
    }

    public class LogEventAnnotationModel
    {
        public string providerId { get; set; }
        public Guid driverId { get; set; }
        public string comment { get; set; }
        public DateTimeOffset dateTime { get; set; }

        public LogEventAnnotationModel(LogEventAnnotation annotation, string providerId)
        {
            this.providerId = providerId;
            this.driverId = annotation.driverId;
            this.comment = annotation.comment;
            this.dateTime = annotation.dateTime;
        }
    }
}
