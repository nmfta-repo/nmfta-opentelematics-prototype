using Prototype.OpenTelematics.DataAccess;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleFaultCodeEventFollow
    {
        public string token { get; set; }
        public List<VehicleFaultCodeModel> feed { get; set; }
    }
}
