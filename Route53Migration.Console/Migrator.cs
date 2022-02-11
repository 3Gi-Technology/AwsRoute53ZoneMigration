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
                migChanges.Add(new MigChange()
                {
                    Action = "CREATE",
                    ResourceRecordSet = new MigResourceRecordSet()
                    {
                        Name = resourceRecordSet.Name,
                        Ttl = resourceRecordSet.Ttl,
                        Type = resourceRecordSet.Type,
                        ResourceRecords = resourceRecordSet.ResourceRecords.Select(record => new MigResourceRecord() {Value = record.Value}).ToArray()
                    }
                });
            }


            return new MigrationRecordSet {Changes = migChanges.ToArray()};
        }
    }
}