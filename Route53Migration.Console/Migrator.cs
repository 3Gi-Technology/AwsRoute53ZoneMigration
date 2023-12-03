using System;
using System.Collections.Generic;
using System.Linq;
using Route53Migration.Migration;

namespace Route53Migration.Console
{
    public static class Migrator
    {
        public static MigrationRecordSet Migrate(Route53RecordSet route53RecordSet)
        {
            
            var migChanges = new List<MigChange>();

            foreach (var resourceRecordSet in route53RecordSet.ResourceRecordSets)
            {
                var migChange = new MigChange()
                {
                    Action = "CREATE",
                    ResourceRecordSet = new MigResourceRecordSet()
                    {
                        Name = resourceRecordSet.Name,
                        Ttl = resourceRecordSet.Ttl,
                        Type = resourceRecordSet.Type,
                    }
                };
                
                if (resourceRecordSet.ResourceRecords != null && resourceRecordSet.ResourceRecords.Any())
                {
                    migChange.ResourceRecordSet.ResourceRecords = resourceRecordSet.ResourceRecords.Select(record => new MigResourceRecord() { Value = record.Value }).ToArray();

                }else if (resourceRecordSet.AliasTarget != null)
                {
                    migChange.ResourceRecordSet.AliasTarget = new MigAliasTarget()
                    {
                        DnsName = resourceRecordSet.AliasTarget.DNSName,
                        EvaluateTargetHealth = resourceRecordSet.AliasTarget.EvaluateTargetHealth,
                        HostedZoneId = resourceRecordSet.AliasTarget.HostedZoneId
                    };
                }else
                {
                    throw new Exception("Unknown record State.");
                }


                migChanges.Add(migChange);
            }


            return new MigrationRecordSet {Changes = migChanges.ToArray()};
        }
    }
}