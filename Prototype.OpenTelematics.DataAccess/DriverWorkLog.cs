using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.DataAccess
{
    public class DriverWorkLog
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public DateTime workDate { get; set; }
        public decimal hoursWorked { get; set; }
    }
}
