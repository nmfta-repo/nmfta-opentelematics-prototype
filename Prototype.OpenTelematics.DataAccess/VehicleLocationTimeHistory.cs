using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehicleLocationTimeHistory
    {
        public Guid Id { get; set; }
        public Guid vehicleId { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public long sequence { get; set; }

        public virtual Vehicle vehicle { get; set; }
    }
}