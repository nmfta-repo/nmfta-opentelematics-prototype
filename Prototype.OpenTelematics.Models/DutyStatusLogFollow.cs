using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class DutyStatusLogFollow
    {
        public string token { get; set; }
        public List<DutyStatusLogModel> feed {get; set;}
    }
}
