using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DriverWaiver
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public DateTimeOffset waiverDay { get; set; }
    }
}