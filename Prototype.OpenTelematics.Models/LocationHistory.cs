using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;


namespace Prototype.OpenTelematics.Models
{
    public class LocationHistory
    {
        public List<VehicleLocation> data { get; set; }
        public TimeResolution timeResolution { get; set; }

        public LocationHistory(List<VehicleLocationTimeHistory> locationList, string ProviderId)
        {
            data = new List<VehicleLocation>();
            foreach(VehicleLocationTimeHistory location in locationList)
            {
                data.Add(new VehicleLocation(location, ProviderId));
            }
        }

        public LocationHistory()
        {
            data = new List<VehicleLocation>();
        }
    }

    public class CoarseVehicleLocationTimeHistoryModel : LocationHistory
    {
        public CoarseVehicleLocationTimeHistoryModel(List<VehicleLocationTimeHistory> locationList, string ProviderId) : base(locationList, ProviderId)
        {
            this.timeResolution = TimeResolution.TIMERESOLUTION_NOT_MAX;            
        }
        public CoarseVehicleLocationTimeHistoryModel() : base() { }
    }

    public class VehicleLocationTimeHistoryModel : LocationHistory
    {
        public VehicleLocationTimeHistoryModel(List<VehicleLocationTimeHistory> locationList, string ProviderId) : base(locationList, ProviderId)
        {
            this.timeResolution = TimeResolution.TIMERESOLUTION_MAX;
        }
        public VehicleLocationTimeHistoryModel() : base() {}
    }

    public class VehicleLocation
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public string location { get; set; }

        public VehicleLocation(VehicleLocationTimeHistory model, string ProviderId)
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
