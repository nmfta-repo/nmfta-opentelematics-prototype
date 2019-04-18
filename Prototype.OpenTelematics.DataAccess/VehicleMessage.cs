using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehicleMessage
    {
        public Guid Id { get; set; }
        public Guid vehicleId { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public DateTimeOffset displayAt { get; set; }
        public DateTimeOffset createdOn { get; set; }
    }
}