﻿using Prototype.OpenTelematics.DataAccess;

namespace Prototype.OpenTelematics.Models
{
    public class CruiseStatusModel
    {
        public bool ccSwitch { get; set; }
        public bool ccSetSwitch { get; set; }
        public bool ccCoastSwitch { get; set; }
        public bool ccClutchSwitch { get; set; }
        public bool ccCruiseSwitch { get; set; }
        public bool ccResumeSwitch { get; set; }
        public bool ccBrakeSwitch { get; set; }
        public int ccSpeed { get; set; }

        public CruiseStatusModel(VehicleFaultCodeEvent item)
        {
            ccBrakeSwitch = item.ccBrakeSwitch;
            ccClutchSwitch = item.ccClutchSwitch;
            ccCoastSwitch = item.ccCoastSwitch;
            ccCruiseSwitch = item.ccCruiseSwitch;
            ccResumeSwitch = item.ccResumeSwitch;
            ccSetSwitch = item.ccSetSwitch;
            ccSpeed = item.ccSpeed;
            ccSwitch = item.ccSwitch;
        }

    }

}