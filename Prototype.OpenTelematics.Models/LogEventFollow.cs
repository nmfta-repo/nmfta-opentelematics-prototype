using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class LogEventFollow
    {
        public string token { get; set; }
        public List<LogEventModel> feed { get; set; }
    }
}
