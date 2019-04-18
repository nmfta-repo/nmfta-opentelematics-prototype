using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class VehiclePerformanceThreshold
    {
        public Guid Id { get; set; }
        public DateTimeOffset activeFrom { get; set; }
        public DateTimeOffset activeTo { get; set; }
        public int rpmOverValue { get; set; }
        public int overSpeedValue { get; set; }
        public int excessSpeedValue { get; set; }
        public int longIdleValue { get; set; }
        public int hiThrottleValue { get; set; }
    }
}
