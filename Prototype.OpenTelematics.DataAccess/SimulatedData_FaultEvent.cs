using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class SimulatedData_FaultEvent
    {
        public int Id { get; set; }
        public Guid vehicleId { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string eventComment { get; set; }
        public int occurencesCount { get; set; }
        public int messageIdentifier { get; set; }
        public string parameterOrSubsystemIdType { get; set; }
        public int faultCodeParameterOrSubsystemId { get; set; }
        public int sourceAddress { get; set; }
        public int suspectParameterNumber { get; set; }
        public int failureModeIdentifier { get; set; }
        public bool urgentFlag { get; set; }
        public decimal odometer { get; set; }
        public int engineRpm { get; set; }
        public decimal ecmSpeed { get; set; }
        public string gpsQuality { get; set; }
        public string clearType { get; set; }
        public int createOrder { get; set; }
    }
}
