﻿using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class ServiceStatusEvent
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTimeOffset dateTime { get; set; }
    }
}