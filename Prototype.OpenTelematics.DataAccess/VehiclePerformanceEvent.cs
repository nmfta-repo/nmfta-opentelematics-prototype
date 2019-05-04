using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehiclePerformanceEvent
    {
        public Guid Id { get; set; }
        public DateTimeOffset eventStart { get; set; }
        public DateTimeOffset eventEnd { get; set; }
        public Guid vehicleId { get; set; }
        public string eventComment { get; set; }
        public int hours { get; set; }
        public Guid performanceThresholdId { get; set; }
        public int odometerStart { get; set; }
        public int odometerEnd { get; set; }
        public int engineTime { get; set; }
        public int movingTime { get; set; }
        public int startFuel { get; set; }
        public int endFuel { get; set; }
        public int brakeApplications { get; set; }
        public decimal engineLoadStopped { get; set; }
        public decimal engineLoadMoving { get; set; }
        public int headlightTime { get; set; }
        public int speedGovernorValue { get; set; }
        public int? overRpmTime { get; set; }
        public int? overSpeedTime { get; set; }
        public int? excessSpeedTime { get; set; }
        public int? longIdleTime { get; set; }
        public int? shortIdleTime { get; set; }
        public int? shortIdleCount { get; set; }
        public int? longIdleFuel { get; set; }
        public int? shortIdleFuel { get; set; }
        public int? cruiseEvents { get; set; }
        public int? cruiseTime { get; set; }
        public int? cruiseFuel { get; set; }
        public int? cruiseDistance { get; set; }
        public int? topGearValue { get; set; }
        public int? topGearTime { get; set; }
        public int? topGearFuel { get; set; }
        public int? topGearDistance { get; set; }
        public int? ptoFuel { get; set; }
        public int? ptoTime { get; set; }
        public int? seatBeltTime { get; set; }
        public string particulateFilterStatus { get; set; }
        public int? exhaustFluidLevel { get; set; }
        public bool? overspeedLowThrottle { get; set; }
        public bool? overspeedHiThrottle { get; set; }
        public bool? overrpmLowThrottle { get; set; }
        public bool? overrpmHiThrottle { get; set; }
        public bool? lkaDisable { get; set; }
        public bool? ldwActive { get; set; }
        public bool? ldwDisable { get; set; }

        [ForeignKey("performanceThresholdId")]
        public VehiclePerformanceThreshold thresholds { get; set; }
    }
}
