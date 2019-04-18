using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class DutyStatusLogAnnotation
    {
        public Guid Id { get; set; }
        public Guid dutyStatusLogId { get; set; }
        public Guid driverId { get; set; }
        public string comment { get; set; }
        public DateTimeOffset dateTime { get; set; }


    }
}