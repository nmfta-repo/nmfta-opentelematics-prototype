using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class CurrentServiceStatusFactor
    {
        public Guid Id { get; set; }
        public Guid eventId { get; set; }
        public string Factor { get; set; }
    }
}