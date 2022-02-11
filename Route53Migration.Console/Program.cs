using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Route53Migration.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {

            if (!args.Any())
            {
                System.Console.WriteLine("At least one argument is required.");
                System.Console.WriteLine("Route53Migration.Console.exe ExportedRoute53Records.txt");
                Environment.Exit(1);
            }

            var readAllTextAsync = await File.ReadAllTextAsync(args[0]);
            var route53RecordSet = Route53RecordSet.FromJson(readAllTextAsync);

            var migrationRecordSet = Migrator.Migrate(route53RecordSet);

            // ReSharper disable once MethodHasAsyncOverload
            // Async Method is Obslete
            var serializeObject = JsonConvert.SerializeObject(migrationRecordSet, Formatting.Indented);
            System.Console.WriteLine(serializeObject);
            //await File.WriteAllTextAsync("output.json", serializeObject);
            
        }
    }
}
