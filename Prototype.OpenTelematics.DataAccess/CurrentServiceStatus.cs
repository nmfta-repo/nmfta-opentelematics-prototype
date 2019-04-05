using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class CurrentServiceStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTimeOffset dateTime { get; set; }
    }
}