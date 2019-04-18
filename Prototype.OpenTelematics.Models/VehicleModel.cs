using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public string providerId { get; set; }
        public string name { get; set; }
        public string cmvVIN { get; set; }
        public string licensePlate { get; set; }

        public VehicleModel(Vehicle item, string providerId)
        {
            this.Id = item.Id;
            this.providerId = providerId;
            this.name = item.name;
            this.cmvVIN = item.cmvVIN;
            this.licensePlate = item.licensePlate;
        }

    }
}
