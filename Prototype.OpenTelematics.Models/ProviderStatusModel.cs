using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.Models
{
    public class ProviderStatusModel
    {
        public string serviceStatus { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public string[] factors { get; set; }
    }

    public class IncidentsModel
    {
        public List<ProviderStatusModel> data { get; set; }
    }
}