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
}