using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DriverBreakRule
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public DateTimeOffset activeFrom { get; set; }
        public DateTimeOffset activeTo { get; set; }
        public string country { get; set; }
        public string region { get; set; }
    }
}