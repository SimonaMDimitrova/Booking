namespace Booking.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Booking.Data.Seeding.ImportDTOs;

    using Newtonsoft.Json;

    internal class CountriesCurrenciesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            using (var reader = new StreamReader("../../Data/Booking.Data/Seeding/Datasets/country-by-currency-code.json"))
            {
                var json = reader.ReadToEnd();
                var countriesCurrenciesDto = JsonConvert.DeserializeObject<CountryModel[]>(json);
                var countries = new List<Country>();
                var currencies = new List<Currency>();

                foreach (var countryCurrencyDto in countriesCurrenciesDto)
                {
                    if (!IsValid(countryCurrencyDto))
                    {
                        continue;
                    }

                    var ifCountryExists = countries
                        .Any(c => c.Name == countryCurrencyDto.Country);
                    if (ifCountryExists)
                    {
                        continue;
                    }

                    var ifCurrencyExists = currencies
                        .Any(c => c.CurrencyCode == countryCurrencyDto.CurrencyCode);
                    if (!ifCurrencyExists)
                    {
                        var currency = new Currency { CurrencyCode = countryCurrencyDto.CurrencyCode };
                        currencies.Add(currency);
                    }

                    var country = new Country { Name = countryCurrencyDto.Country, Currency = currencies.Where(c => c.CurrencyCode == countryCurrencyDto.CurrencyCode).First() };
                    countries.Add(country);
                }

                await dbContext.AddRangeAsync(currencies);
                await dbContext.AddRangeAsync(countries);
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}
