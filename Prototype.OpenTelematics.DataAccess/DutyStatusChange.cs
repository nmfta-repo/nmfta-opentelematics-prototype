using System;

namespace Prototype.OpenTelematics.DataAccess
{
    public class DutyStatusChange
    {
        public Guid Id { get; set; }
        public Guid driverId { get; set; }
        public DateTime dateTime { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string status { get; set; }
    }
}