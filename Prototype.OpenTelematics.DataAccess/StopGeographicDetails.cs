using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class StopGeographicDetails
    {
        public Guid Id { get; set; }
        public string providerId { get; set; }
        public string stopName { get; set; }
        public string address { get; set; }
        public string comment { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set; }
        public string entryArea { get; set; }
    }
}
