using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehicleStopXRef
    {
        public Guid Id { get; set; }
        public Guid vehicleId { get; set; }
        public Guid stopId { get; set; }
        public DateTimeOffset createdOn { get; set; }
    }
}