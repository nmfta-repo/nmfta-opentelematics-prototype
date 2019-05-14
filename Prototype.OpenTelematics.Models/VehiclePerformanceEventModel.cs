using System;
using System.Collections.Generic;
using System.Text;
using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Models
{
    public class VehiclePerformanceEventModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
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
        public bool? lkaActive { get; set; }
        public bool? ldwActive { get; set; }
        public bool? ldwDisable { get; set; }
        public VehiclePerformanceThresholdModel thresholds { get; set; }

        public VehiclePerformanceEventModel(VehiclePerformanceEvent item, string provider)
        {
            id = item.Id;
            providerId = provider;
            serverTime = DateTimeOffset.UtcNow;
            eventStart = item.eventStart;
            eventEnd = item.eventEnd;
            vehicleId = item.vehicleId;
            eventComment = item.eventComment;
            hours = item.hours;
            odometerEnd = item.odometerEnd;
            odometerStart = item.odometerStart;
            engineTime = item.engineTime;
            movingTime = item.movingTime;
            startFuel = item.startFuel;
            endFuel = item.endFuel;
            brakeApplications = item.brakeApplications;
            engineLoadStopped = item.engineLoadStopped;
            engineLoadMoving = item.engineLoadMoving;
            headlightTime = item.headlightTime;
            speedGovernorValue = item.speedGovernorValue;
            overRpmTime = item.overRpmTime;
            overSpeedTime = item.overSpeedTime;
            excessSpeedTime = item.excessSpeedTime;
            longIdleTime = item.longIdleTime;
            shortIdleCount = item.shortIdleCount;
            shortIdleTime = item.shortIdleTime;
            longIdleFuel = item.longIdleFuel;
            shortIdleFuel = item.shortIdleFuel;
            cruiseEvents = item.cruiseEvents;
            cruiseTime = item.cruiseTime;
            cruiseFuel = item.cruiseFuel;
            cruiseDistance = item.cruiseDistance;
            topGearValue = item.topGearValue;
            topGearTime = item.topGearTime;
            topGearValue = item.topGearValue;
            topGearDistance = item.topGearDistance;
            ptoFuel = item.ptoFuel;
            ptoTime = item.ptoTime;
            seatBeltTime = item.seatBeltTime;
            particulateFilterStatus = item.particulateFilterStatus;
            exhaustFluidLevel = item.exhaustFluidLevel;
            overspeedLowThrottle = item.overspeedLowThrottle;
            overspeedHiThrottle = item.overspeedHiThrottle;
            overrpmLowThrottle = item.overrpmLowThrottle;
            overrpmHiThrottle = item.overrpmHiThrottle;
            lkaDisable = item.lkaDisable;
            lkaActive = item.lkaActive;
            ldwActive = item.ldwActive;
            ldwDisable = item.ldwDisable;
            if (item.thresholds != null)
                thresholds = new VehiclePerformanceThresholdModel(item.thresholds, provider);
        }
    }
}
