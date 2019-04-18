using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class Vehicle
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string cmvVIN { get; set; }
        public string licensePlate { get; set; }

    }
}
