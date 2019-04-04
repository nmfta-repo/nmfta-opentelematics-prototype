﻿using System;
using System.Collections.Generic;
using System.Text;

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Prototype.OpenTelematics.Models;
//
//    var breakRulesAndWaivers = BreakRulesAndWaivers.FromJson(jsonString);

namespace Prototype.OpenTelematics.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BreakRulesAndWaivers
    {
        /// <summary>
        /// the Region Specific Breaks Rules for this driver for the requested time period
        /// </summary>
        [JsonProperty("breakRules", Required = Required.Always)]
        public BreakRule[] BreakRules { get; set; }

        /// <summary>
        /// the Region Specific Waivers for this driver for the requested time period
        /// </summary>
        [JsonProperty("waivers", Required = Required.Always)]
        public Waiver[] Waivers { get; set; }
    }

    public partial class BreakRule
    {
        /// <summary>
        /// The date and time the break rules take effect
        /// </summary>
        [JsonProperty("activeFrom", Required = Required.Always)]
        public string ActiveFrom { get; set; }

        /// <summary>
        /// The date and time the break rules stop taking effect, if left blank then the rules apply
        /// in perpetuity
        /// </summary>
        [JsonProperty("activeTo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ActiveTo { get; set; }

        /// <summary>
        /// short code for the country of the region dictating the specific break rules
        /// </summary>
        [JsonProperty("country", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// The id of the driver with the region specific break rules.
        /// </summary>
        [JsonProperty("driverId", Required = Required.Always)]
        public string DriverId { get; set; }

        /// <summary>
        /// The unique identifier for the specific Entity object in the system.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// The unique 'Provider ID' of the TSP.
        /// </summary>
        [JsonProperty("providerId", Required = Required.Always)]
        public string ProviderId { get; set; }

        /// <summary>
        /// short code for the country's region/state/province/territory dictating the specific break
        /// rules
        /// </summary>
        [JsonProperty("region", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }
    }

    public partial class Waiver
    {
        /// <summary>
        /// short code for the country of the region dictating the specific waiver
        /// </summary>
        [JsonProperty("country", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// The id of the driver with the region specific waiver.
        /// </summary>
        [JsonProperty("driverId", Required = Required.Always)]
        public string DriverId { get; set; }

        /// <summary>
        /// The unique identifier for the specific Entity object in the system.
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// The unique 'Provider ID' of the TSP.
        /// </summary>
        [JsonProperty("providerId", Required = Required.Always)]
        public string ProviderId { get; set; }

        /// <summary>
        /// short code for the country's region/state/province/territory dictating the specific waiver
        /// </summary>
        [JsonProperty("region", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        /// <summary>
        /// The date of the effect of the waiver -- time is ignored
        /// </summary>
        [JsonProperty("waiverDay", Required = Required.Always)]
        public string WaiverDay { get; set; }
    }

    public partial class BreakRulesAndWaivers
    {
        public static BreakRulesAndWaivers FromJson(string json) => JsonConvert.DeserializeObject<BreakRulesAndWaivers>(json, Prototype.OpenTelematics.Models.Converter.Settings);
    }

    public static partial class Serialize
    {
        public static string ToJson(this BreakRulesAndWaivers self) => JsonConvert.SerializeObject(self, Prototype.OpenTelematics.Models.Converter.Settings);
    }

    internal static partial class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
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
