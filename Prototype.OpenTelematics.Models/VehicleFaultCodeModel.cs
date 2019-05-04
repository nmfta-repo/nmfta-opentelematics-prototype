using Prototype.OpenTelematics.DataAccess;
using System;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleFaultCodeModel
    {
        public Guid Id { get; set; }
        public string providerId { get; set; }
        public Guid vehicleId { get; set; }
        public string location { get; set; }
        public string eventComment { get; set; }
        public DateTimeOffset triggerDate { get; set; }
        public DateTimeOffset clearedDate { get; set; }
        public int occurences { get; set; }
        public int messageIdentifier { get; set; }
        public string parameterOrSubsystemIdType { get; set; }
        public int faultCodeParameterOrSubsystemId { get; set; }
        public int sourceAddress { get; set; }
        public int suspectParameterNumber { get; set; }
        public int failureModeIdentifier { get; set; }
        public bool urgentFlag { get; set; }
        public int odometer { get; set; }
        public int engineRpm { get; set; }
        public int ecmSpeed { get; set; }
        public CruiseStatusModel cruiseStatus { get; set; }
        public IgnitionStatusModel ignitionStatus { get; set; }
        public string gpsQuality { get; set; }
        public string clearType { get; set; }

        public VehicleFaultCodeModel(VehicleFaultCodeEvent item, string provider)
        {
            Id = item.Id;
            providerId = provider;
            vehicleId = item.vehicleId;
            location = string.Format("{0} {1}", item.latitude, item.longitude);
            eventComment = item.eventComment;
            triggerDate = item.triggerDate;
            clearedDate = item.clearedDate;
            occurences = item.occurences;
            messageIdentifier = item.messageIdentifier;
            parameterOrSubsystemIdType = item.parameterOrSubsystemIdType;
            faultCodeParameterOrSubsystemId = item.faultCodeParameterOrSubsystemId;
            sourceAddress = item.sourceAddress;
            suspectParameterNumber = item.suspectParameterNumber;
            failureModeIdentifier = item.failureModeIdentifier;
            urgentFlag = item.urgentFlag;
            odometer = item.odometer;
            engineRpm = item.engineRpm;
            ecmSpeed = item.ecmSpeed;
            cruiseStatus = new CruiseStatusModel(item);
            ignitionStatus = new IgnitionStatusModel(item);
            gpsQuality = item.gpsQuality;
            clearType = item.clearType;            
        }
    }
}
