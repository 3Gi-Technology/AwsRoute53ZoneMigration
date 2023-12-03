using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Route53Migration.Console;

namespace Route53Migration
{
    public class Tests
    {

        [Test]
        public async Task Test1()
        {

            var readAllTextAsync = await File.ReadAllTextAsync(@"C:\\Temp\\zone_migration\\export");
            var route53RecordSet = Route53RecordSet.FromJson(readAllTextAsync);
            var migrationRecordSet = Migrator.Migrate(route53RecordSet);
            var serializeObject = JsonConvert.SerializeObject(migrationRecordSet, Formatting.Indented);
            await File.WriteAllTextAsync(@"C:\\Temp\\zone_migration\\import", serializeObject);


        }
    }
}