using Prototype.OpenTelematics.DataAccess;
using System;

namespace Prototype.OpenTelematics.Models
{
    public class DriverModel
    {
        public Guid id { get; set; }
        public string providerId { get; set; }
        public DateTimeOffset serverTime { get; set; }
        public string username { get; set; }
        public string driverLicenseNumber { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string driverHomeTerminal { get; set; }
        public string password { get; set; }
        public bool enabled { get; set; }

        public DriverModel(Driver item, string provider)
        {
            id = item.Id;
            providerId = provider;
            serverTime = DateTime.UtcNow;
            username = item.username;
            driverLicenseNumber = item.driverLicenseNumber;
            country = item.country;
            region = item.region;
            driverHomeTerminal = item.driverHomeTerminal;
            password = item.password;
            enabled = item.enabled;
        }
    }
}
