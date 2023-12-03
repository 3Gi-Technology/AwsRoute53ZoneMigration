using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Route53Migration
{
    public partial class Route53RecordSet
    {
        [JsonProperty("ResourceRecordSets", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceRecordSet[] ResourceRecordSets { get; set; }
    }

    public partial class ResourceRecordSet
    {
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("TTL", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ttl { get; set; }

        [JsonProperty("ResourceRecords", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceRecord[] ResourceRecords { get; set; }
        
        [JsonProperty("AliasTarget", NullValueHandling = NullValueHandling.Ignore)]
        public AliasTarget AliasTarget { get; set; }
    }

    public class AliasTarget
    {
        public string HostedZoneId { get; set; }
        public string DNSName { get; set; }
        public bool EvaluateTargetHealth { get; set; }
    }

    public partial class ResourceRecord
    {
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public partial class Route53RecordSet
    {
        public static Route53RecordSet FromJson(string json) => JsonConvert.DeserializeObject<Route53RecordSet>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Route53RecordSet self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
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
