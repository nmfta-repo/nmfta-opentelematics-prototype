using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Models
{
    public class IgnitionStatusModel
    {
        public bool ignitionAccessory { get; set; }
        public bool ignitionRunContact { get; set; }
        public bool ignitionCrankContact { get; set; }
        public bool ignitionAidContact { get; set; }

        public IgnitionStatusModel(VehicleFaultCodeEvent item)
        {
            ignitionAccessory = item.ignitionAccessory;
            ignitionRunContact = item.ignitionRunContact;
            ignitionCrankContact = item.ignitionCrankContact;
            ignitionAidContact = item.ignitionAidContact;
        }
    }
}
