using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace HousePrice.Postcodes.Services
{
    public static class PostcodeLookup
    {
        [Serializable]
        public class PostcodeData
        {
            private string _postcode;

            public long Id { get; set; }

            public string Postcode
            {
                get => _postcode;
                set => _postcode = value.Replace(" ", string.Empty);
            }

            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
        }

        private static Dictionary<string, PostcodeData> _postcodeLookup = new Dictionary<string, PostcodeData>();

        public static bool IsReady => PopulateTask.IsCompleted;
        private static readonly Task PopulateTask;
        private static string postcodeFile;
        static PostcodeLookup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();


            postcodeFile = configuration["POSTCODE_DATA"];

            if (postcodeFile != null)
            {
                PopulateTask = new Task(() =>
                {

                    Log.Information("Loading postcodes");
                    using (var streamReader =
                        new StreamReader(new FileStream(postcodeFile, FileMode.Open, FileAccess.Read)))
                    {

                        using (var csvReader = new CsvHelper.CsvReader(streamReader))
                        {
                            csvReader.Configuration.BufferSize = 131072;
                            var recs = csvReader.GetRecords<PostcodeData>();

                            var timer = Stopwatch.StartNew();
                            _postcodeLookup = recs.ToDictionary(p => p.Postcode);
                            timer.Stop();
                            Console.WriteLine(timer.ElapsedMilliseconds);

                        }
                    }
                    Log.Information("Postcodes loaded");
                });
                PopulateTask.Start();
            }
        }

        public static PostcodeData GetByPostcode(string postcode)
        {
            PopulateTask?.Wait();

            var found = _postcodeLookup.TryGetValue(postcode.ToUpper().Replace(" ", string.Empty), out PostcodeData postcodeData);
            return found ? postcodeData:null;
        }
    }
}
