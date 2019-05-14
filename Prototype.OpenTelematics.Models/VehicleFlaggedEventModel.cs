using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleFlaggedEventModel 
    {
        public Guid Id { get; set; }
        public string providerId { get; set;  }
        public DateTimeOffset serverTime { get; set; }
        public DateTimeOffset eventStart { get; set; }
        public DateTimeOffset eventEnd { get; set; }
        public Guid vehicleId { get; set; }
        public Guid? driverId { get; set; }
        public string eventComment { get; set; }
        public string trigger { get; set; }
        public decimal? gpsSpeed { get; set; }
        public decimal? gpsHeading { get; set; }
        public string gpsQuality { get; set; }
        public decimal ecmSpeed { get; set; }
        public decimal engineRPM { get; set; }
        public decimal accelerationPercent { get; set; }
        public bool? seatBelts { get; set; }
        public string cruiseStatus { get; set; }
        public bool parkingBreak { get; set; }
        public string ignitionStatus { get; set; }
        public decimal forwardVehicleSpeed { get; set; }
        public decimal forwardVehicleDistance { get; set; }
        public decimal forwardVehicleElapsed { get; set; }
        public int odometer { get; set; }

        public VehicleFlaggedEventModel(VehicleFlaggedEvent item, string provider)
        {
            Id = item.Id;
            providerId = provider;
            serverTime = DateTimeOffset.UtcNow;
            vehicleId = item.vehicleId;
            driverId = item.driverId;
            eventStart = item.eventStart;
            eventEnd = item.eventEnd;
            eventComment = item.eventComment;
            trigger = item.trigger;
            gpsSpeed = item.gpsSpeed;
            gpsHeading = item.gpsHeading;
            gpsQuality = item.gpsQuality;
            ecmSpeed = item.ecmSpeed;
            engineRPM = item.engineRPM;
            accelerationPercent = item.accelerationPercent;
            seatBelts = item.seatBelts;
            cruiseStatus = item.cruiseStatus;
            parkingBreak = item.parkingBreak;
            ignitionStatus = item.ignitionStatus;
            forwardVehicleDistance = item.forwardVehicleDistance;
            forwardVehicleSpeed = item.forwardVehicleSpeed;
            forwardVehicleElapsed = item.forwardVehicleElapsed;
            odometer = item.odometer;
        }
    }
}
