using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class DutyStatusLogModel
    {
        public DutyStatusLogModel(DutyStatusLog log, string providerId)
        {
            this.id = log.Id;
            this.providerId = providerId;
            this.coDrivers = new List<Guid>();
            this.dateTime = log.dateTime;
            this.distanceLastValid = log.distanceLastValid;
            this.editDateTime = log.editDateTime;
            this.eventDataChecksum = log.eventDataChecksum;
            this.eventRecordStatus = log.eventRecordStatus;
            this.eventType = log.eventType;
            this.malfunction = log.malfunction;
            this.multidayBasis = log.multidayBasis;
            this.origin = log.origin;
            this.outputFileComment = log.outputFileComment;
            this.parentId = log.parentId;
            this.sequence = log.sequence;
            this.state = log.state;
            this.status = log.status;
            this.vehicleId = log.vehicleId;
            this.verifyDateTime = log.verifyDateTime;
            this.annotations = new List<DutyStatusLogAnnotationModel>();
            foreach (DutyStatusLogAnnotation annotation in log.annotations)
                this.annotations.Add(new DutyStatusLogAnnotationModel(annotation, providerId));
            if (log.location != null)
                this.location = new LocationModel(log.location);
        }

        public Guid id { get; set; }
        public string providerId { get; set; }
        public List<DutyStatusLogAnnotationModel> annotations { get; set; }
        public List<Guid> coDrivers { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public decimal distanceLastValid { get; set; }
        public DateTimeOffset editDateTime { get; set; }
        public string eventRecordStatus { get; set; }
        public string eventType { get; set; }
        public LocationModel location { get; set;  }
        public string malfunction { get; set; }
        public string origin { get; set; }
        public Guid? parentId { get; set; }
        public int sequence { get; set; }
        public string state { get; set; }
        public string status { get; set; }
        public DateTimeOffset? verifyDateTime { get; set; }
        public int multidayBasis { get; set; }
        public string outputFileComment { get; set; }
        public string eventDataChecksum { get; set; }
    }

    public class DutyStatusLogAnnotationModel
    {
        public string providerId { get; set; }
        public Guid driverId { get; set; }
        public string comment { get; set; }
        public DateTimeOffset dateTime { get; set; }

        public DutyStatusLogAnnotationModel(DutyStatusLogAnnotation annotation, string providerId)
        {
            this.providerId = providerId;
            this.driverId = annotation.driverId;
            this.comment = annotation.comment;
            this.dateTime = annotation.dateTime;
        }
    }
}
