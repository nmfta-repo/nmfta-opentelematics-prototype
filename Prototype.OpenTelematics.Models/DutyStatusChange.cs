using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Prototype.OpenTelematics.Models
{
    public class DutyStatusChangeModel
    {
        [JsonProperty("dateTime", Required = Required.Always)]
        public DateTime DateTime { get; set; }

        [JsonProperty("location", Required = Required.Always)]
        public string Location { get; set; }

        [JsonProperty("status", Required = Required.Always)]
        public DriverStatus Status { get; set; }

        public static DutyStatusChangeModel FromJson(string json) => JsonConvert.DeserializeObject<DutyStatusChangeModel>(json, Prototype.OpenTelematics.Models.Converter.Settings3);
    }

    public enum DriverStatus
    {
        /// <summary>
        /// The driver has changed status to on-duty
        /// </summary>
        EXTTRIG_STATUS_ON,
        /// <summary>
        /// The driver has changed status to off-duty
        /// </summary>
        EXTTRIG_STATUS_OFF
    }

    public static partial class Serialize
    {
        public static string ToJson(this DutyStatusChangeModel self) => JsonConvert.SerializeObject(self, Prototype.OpenTelematics.Models.Converter.Settings3);
    }

    internal static partial class Converter
    {
        public static readonly JsonSerializerSettings Settings3 = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
