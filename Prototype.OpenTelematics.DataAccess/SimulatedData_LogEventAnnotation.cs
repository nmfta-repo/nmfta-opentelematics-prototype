using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class SimulatedData_LogEventAnnotation
    {
        public int Id { get; set; }
        public int logEventId { get; set; }
        public Guid? driverId { get; set; }
        public string comment { get; set; }
        public DateTimeOffset? dateTime { get; set; }
    }
}
