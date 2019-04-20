using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleInfoHistory
    {
        public LocationHistory coarseVehicleLocationTimeHistories { get; set; }
        public List<VehicleFlaggedEvent> flaggedVehiclePerformanceEvents { get; set; }
        public List<VehiclePerformanceEvent> vehiclePerformanceEvents { get; set; }
        public List<VehicleFaultCodeModel> vehicleFaultCodeEvents { get; set; }
    }
}
