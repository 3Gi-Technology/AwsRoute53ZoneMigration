using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Route53Migration.Migration
{

    public partial class MigrationRecordSet
    {
        [JsonProperty("Comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("Changes", NullValueHandling = NullValueHandling.Ignore)]
        public MigChange[] Changes { get; set; }
    }

    public partial class MigChange
    {
        [JsonProperty("Action", NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        [JsonProperty("ResourceRecordSet", NullValueHandling = NullValueHandling.Ignore)]
        public MigResourceRecordSet ResourceRecordSet { get; set; }
    }

    public partial class MigResourceRecordSet
    {
        [JsonProperty("ResourceRecords", NullValueHandling = NullValueHandling.Ignore)]
        public MigResourceRecord[] ResourceRecords { get; set; }

        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("TTL", NullValueHandling = NullValueHandling.Ignore)]
        public long? Ttl { get; set; }
        
        [JsonProperty("AliasTarget", NullValueHandling = NullValueHandling.Ignore)]
        public MigAliasTarget AliasTarget { get; set; }
    }

    public partial class MigResourceRecord
    {
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }

    public partial class MigAliasTarget
    {
        [JsonProperty("HostedZoneId", NullValueHandling = NullValueHandling.Ignore)]
        public string HostedZoneId { get; set; }

        [JsonProperty("EvaluateTargetHealth", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EvaluateTargetHealth { get; set; }

        [JsonProperty("DNSName", NullValueHandling = NullValueHandling.Ignore)]
        public string DnsName { get; set; }
    }
    
    public partial class MigrationRecordSet
    {
        public static MigrationRecordSet FromJson(string json) => JsonConvert.DeserializeObject<MigrationRecordSet>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MigrationRecordSet self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

