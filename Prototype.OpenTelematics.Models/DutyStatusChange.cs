using System;

namespace Prototype.OpenTelematics.Models
{
    public class DutyStatusChangeModel
    {
        public DateTime dateTime { get; set; }
        public string location { get; set; }
        public string status { get; set; }
    }
}
