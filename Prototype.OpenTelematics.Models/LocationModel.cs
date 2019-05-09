using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Models
{
    public class LocationModel
    {
        public string location { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string identifiedPlace { get; set; }
        public string identifiedState { get; set; }
        public decimal? distanceFrom { get; set; }
        public string directionFrom { get; set; }

        public LocationModel(Location loc)
        {
            this.location = string.Format("{0} {1}", loc.latitude, loc.longitude);
            this.latitude = loc.latitude;
            this.longitude = loc.longitude;
            this.identifiedPlace = loc.identifiedPlace;
            this.identifiedState = loc.identifiedState;
            this.directionFrom = loc.directionFrom;
            this.distanceFrom = loc.distanceFrom;
        }
    }
}
