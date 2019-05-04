using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class Location
    {
        public Guid Id { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string identifiedPlace { get; set; }
        public string identifiedState { get; set; }
        public decimal? distanceFrom { get; set; }
        public string directionFrom { get; set; }
    }
}
