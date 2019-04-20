using System.Collections.Generic;

namespace Prototype.OpenTelematics.Models
{
    public class DutyStatusLogFollow
    {
        public string token { get; set; }
        public List<DutyStatusLogModel> feed {get; set;}
    }
}
