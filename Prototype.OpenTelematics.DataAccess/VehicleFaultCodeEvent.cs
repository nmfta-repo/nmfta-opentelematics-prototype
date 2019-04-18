using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehicleFaultCodeEvent
    {
        public Guid Id { get; set; }
        public Guid vehicleId { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string eventComment { get; set; }
        public DateTimeOffset triggerDate { get; set; }
        public DateTimeOffset clearedDate { get; set; }
        public int occurences { get; set; }
        public int messageIdentifier { get; set; }
        public string parameterOrSubsystemIdType { get; set; }
        public int faultCodeParameterOfSubsystemId { get; set; }
        public int sourceAddress { get; set; }
        public int suspectParameterNumber { get; set; }
        public int failureModeIdentifier { get; set; }
        public bool urgentFlag { get; set; }
        public int odometer { get; set; }
        public int engineRpm { get; set; }
        public int ecmSpeed { get; set; }
        public bool ccSwitch { get; set; }
        public bool ccSetSwitch { get; set; }
        public bool ccCoastSwitch { get; set; }
        public bool ccClutchSwitch { get; set; }
        public bool ccCruiseSwitch { get; set; }
        public bool ccResumeSwitch { get; set; }
        public bool ccBrakeSwitch { get; set; }
        public int ccSpeed { get; set; }
        public bool ignitionAccessory { get; set; }
        public bool ignitionRunContact { get; set; }
        public bool ignitionCrankContact { get; set; }
        public bool ignitionAidContact { get; set; }
        public string gpsQuality { get; set; }
        public string clearType { get; set; }
    }
}
