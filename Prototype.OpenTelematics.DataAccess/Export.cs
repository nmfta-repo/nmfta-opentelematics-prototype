using System;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class Export
    {
        public Guid Id { get; set; }
        public DateTimeOffset export_date { get; set; }
        public string export_type { get; set; }
        public string location { get; set; }
    }
}