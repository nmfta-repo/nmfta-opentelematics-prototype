using System;

namespace Prototype.OpenTelematics.Models
{
    public class PutStopModel
    {
        public string stopName { get; set; }
        public string stopAddress { get; set; }
        public string stopLocation { get; set; }
        public DateTimeOffset stopDeadline { get; set; }
    }

    public class PatchStopModel
    {
        public string comment { get; set; }
        public string location { get; set; }
        public string[] entryArea { get; set; }
    }

    public class PostRouteModel
    {
        public string stopName { get; set; }
        public string stopAddress { get; set; }
        public string stopLocation { get; set; }
        public DateTimeOffset stopDeadline { get; set; }
        public string startName { get; set; }
        public string startAddress { get; set; }
        public string startLocation { get; set; }
        public string routeAddInstructions { get; set; }
    }

}