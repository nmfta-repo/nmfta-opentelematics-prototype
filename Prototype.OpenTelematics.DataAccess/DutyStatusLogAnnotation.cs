using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DutyStatusLogAnnotation
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public string comment { get; set; }
        public DateTimeOffset dateTime { get; set; }
    }
}