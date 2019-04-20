using Newtonsoft.Json;

namespace Prototype.OpenTelematics.Models
{
    public class DriverChangeLoginModel
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("enabled")]
        public bool enabled { get; set; }

    }
}
