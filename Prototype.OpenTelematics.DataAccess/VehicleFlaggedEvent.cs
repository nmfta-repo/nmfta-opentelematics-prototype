using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehicleFlaggedEvent
    {
        public Guid Id { get; set; }
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
    }
}