using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Prototype.OpenTelematics.Models
{
    public class DriverChangeModel
    {
        [JsonProperty("username", Required = Required.Always)]
        public string username { get; set; }

        [JsonProperty("hoursWorked")]
        public decimal hoursWorked { get; set; }

        [JsonProperty("driverLicenseNumber")]
        public string driverLicenseNumber { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }

        [JsonProperty("driverHomeTerminal")]
        public string driverHomeTerminal { get; set; }
    }
}
