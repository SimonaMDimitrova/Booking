namespace Booking.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Booking.Data;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Xunit;

    public class CurrenciesServiceTests : BaseServiceTests
    {
        private ICurrenciesService Service => this.ServiceProvider.GetRequiredService<ICurrenciesService>();

        private IRepository<Currency> CurrenciesRepository => this.ServiceProvider.GetRequiredService<IRepository<Currency>>();

        private IRepository<Country> CountriesRepository => this.ServiceProvider.GetRequiredService<IRepository<Country>>();

        [Fact]
        public void CheckGetByCountryIdMethod()
        {
            var currency = new Currency
            {
                CurrencyCode = "BYR",
            };

            var country = new Country
            {
                Name = "Belarus",
            };

            currency.Countries.Add(country);

            this.DbContext.Currencies.Add(currency);
            this.DbContext.Countries.Add(country);
            this.DbContext.SaveChanges();

            var expectedResult = "BYR";
            var actualResult = this.Service.GetByCountryId(country.Id);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetByPropertyIdMethod()
        {
            var property = new Property
            {
                Town = new Town
                {
                    Name = "Some town",
                    Country = new Country
                    {
                        Name = "Belarus",
                        Currency = new Currency
                        {
                            CurrencyCode = "BYR",
                        },
                    },
                },
            };

            this.DbContext.Properties.Add(property);
            this.DbContext.SaveChanges();

            var expectedResult = "BYR";
            var actualResult = this.Service.GetByPropertyId(property.Id);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
