using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class Route
    {
        public Guid Id { get; set; }
        public string routeName { get; set; }
        public string startName { get; set; }
        public string startAddress { get; set; }
        public decimal startLatitude { get; set; }
        public decimal startLongitude { get; set; }
        public string comment { get; set; }
    }
}