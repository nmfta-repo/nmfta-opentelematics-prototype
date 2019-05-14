using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.OpenTelematics.Models
{
    public class StopGeographicDetailsModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public string stopName { get; set; }
        public string address { get; set; }
        public string comment { get; set; }
        public string location { get; set; }
        public string[] entryArea { get; set; }

        public StopGeographicDetailsModel(StopGeographicDetails item, string ProviderId)
        {
            id = item.Id;
            providerId = ProviderId;
            serverTime = DateTime.UtcNow;
            stopName = item.stopName;
            address = item.address;
            comment = item.comment;
            location = string.Format("{0} {1}", item.latitude, item.longitude);            
            entryArea = item.entryArea.Split(";");
        }
    }
}
