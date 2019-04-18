using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class Driver
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string driverLicenseNumber { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string driverHomeTerminal { get; set; }
    }
}