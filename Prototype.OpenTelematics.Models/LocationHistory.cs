using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;


namespace Prototype.OpenTelematics.Models
{
    public class LocationHistory
    {
        public List<VehicleLocation> data { get; set; }
        public TimeResolution timeResolution { get; set; }

        public LocationHistory(List<CoarseVehicleLocationTimeHistory> locationList, string ProviderId)
        {
            data = new List<VehicleLocation>();
            foreach(CoarseVehicleLocationTimeHistory location in locationList)
            {
                data.Add(new VehicleLocation(location, ProviderId));
            }
        }
    }

    public class VehicleLocation
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public string location { get; set; }

        public VehicleLocation(CoarseVehicleLocationTimeHistory model, string ProviderId)
        {            
            id = model.Id;
            providerId = ProviderId;
            vehicleId = model.vehicleId;
            driverId = model.driverId;
            dateTime = model.dateTime;
            location = string.Format("{0} {1}", model.latitude.ToString(), model.longitude.ToString());
        }
    }

}
