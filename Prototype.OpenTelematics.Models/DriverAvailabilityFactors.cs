using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Models
{
    using Newtonsoft.Json;

    public partial class DriverAvailability
    {
        public DriverAvailability()
        {
            this.CoarseVehicleLocationTimeHistory = new CoarseVehicleLocationTimeHistoryModel();
        }
        /// <summary>
        /// The Coarse Vehicle Location Time History associated with the requested `driverId` for the
        /// requested time period [`start`, `stop`)
        /// </summary>
        [JsonProperty("coarseVehicleLocationTimeHistory", Required = Required.Always)]
        public CoarseVehicleLocationTimeHistoryModel CoarseVehicleLocationTimeHistory { get; set; }

        /// <summary>
        /// The Duty Status Logs of the requested `driverId` for the requested time period [`start`,
        /// `stop`)
        /// </summary>
        [JsonProperty("dutyStatusLogs", Required = Required.Always)]
        public DutyStatusLog[] DutyStatusLogs { get; set; }

        /// <summary>
        /// All Vehicle Flagged Events which are associated with the requested `driverId` and which
        /// occur within the requested time period [`start`, `stop`)
        /// </summary>
        [JsonProperty("vehicleFlaggedEvents", Required = Required.Always)]
        public VehicleFlaggedEvent[] VehicleFlaggedEvents { get; set; }
    }

    public class CoarseVehicleLocationTimeHistoryModel
    {
        [JsonProperty("data", Required = Required.Always)]
        public CoarseVehicleLocationTimeHistory[] VehicleLocationTimeHistories { get; set; }

        [JsonProperty("timeResolution", Required = Required.Always)]
        public string TimeResolution { get; set; }
    }    
}
