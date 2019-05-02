using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class Vehicle
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string cmvVIN { get; set; }
        public string licensePlate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sequence { get; set; }
    }
}
