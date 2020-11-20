namespace Booking.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    using Newtonsoft.Json;

    internal class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            using (var reader = new StreamReader("../../Data/Booking.Data/Seeding/Datasets/countries-towns.json"))
            {
                var json = reader.ReadToEnd();
                var countriesTowns = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);

                var towns = new HashSet<Town>();
                foreach (var countryTowns in countriesTowns)
                {
                    var isCountryInDb = dbContext
                        .Countries
                        .Any(c => c.Name == countryTowns.Key);
                    if (!isCountryInDb)
                    {
                        continue;
                    }

                    var countryId = dbContext
                        .Countries
                        .First(c => c.Name == countryTowns.Key)
                        .Id;
                    var length = countryTowns.Value.Length;
                    if (countryTowns.Value.Length > 30)
                    {
                        length = 30;
                    }

                    for (int i = 0; i < length; i++)
                    {
                        var townName = countryTowns.Value[i];
                        var isTownAdded = towns.Any(t => t.Name == townName);
                        if (!isTownAdded)
                        {
                            var town = new Town { Name = countryTowns.Value[i], CountryId = countryId };
                            towns.Add(town);
                        }
                    }
                }

                await dbContext.Towns.AddRangeAsync(towns);
            }
        }
    }
}
