﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class VehicleFeedInfosFollow
    {
        public string token { get; set; }
        public VehicleInfoHistory feed { get; set; }
    }
}
