using System;
using System.Collections.Generic;

namespace Prototype.OpenTelematics.Models
{
    public class ProviderStatusModel : ApiModelBase
    {
        public ServiceStatus serviceStatus { get; set; }
        public DateTime dateTime { get; set; }
        public string[] factors { get; set; }
    }

    public class IncidentsModel : ApiModelBase
    {
        public List<ProviderStatusModel> data { get; set; }
    }
}