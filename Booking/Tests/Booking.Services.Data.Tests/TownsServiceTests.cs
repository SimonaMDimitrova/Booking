namespace Booking.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class TownsServiceTests : BaseServiceTests
    {
        private ITownsService Service => this.ServiceProvider.GetRequiredService<ITownsService>();

        [Fact]
        public void CheckGetAllByKeyValuePairBasedOnCountryIdMethod()
        {
            var town = this.GetAddedToDbTowns().First();
            var expectedResult = new List<KeyValuePair<string, string>>();
            expectedResult.Add(new KeyValuePair<string, string>(town.Id.ToString(), town.Name));

            var actualResult = this.Service.GetAllByKeyValuePairBasedOnCountryId(town.CountryId).ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetMostPopularByKeyValuePairBasedOnCountryIdMethod()
        {
            var town = this.GetAddedToDbTowns().First();
            var expectedResult = new List<KeyValuePair<string, string>>();
            expectedResult.Add(new KeyValuePair<string, string>(town.Id.ToString(), town.Name));

            var actualResult = this.Service.GetMostPopularByKeyValuePairBasedOnCountryId(town.CountryId).ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        private IEnumerable<Town> GetAddedToDbTowns()
        {
            var towns = new List<Town>();
            for (int i = 0; i < 7; i++)
            {
                var town = new Town
                {
                    Name = "Town1",
                    Country = new Country
                    {
                        Name = $"Country{i}",
                        Currency = new Currency { CurrencyCode = $"CN{i}" },
                    },
                    Properties =
                        {
                            new Property
                            {
                                Name = "Property1",
                                Offers =
                                {
                                    new Offer
                                    {
                                        Bookings = { new Booking { }, },
                                    },
                                },
                            },
                        },
                };

                towns.Add(town);
            }

            this.DbContext.Towns.AddRange(towns);
            this.DbContext.SaveChanges();

            return towns;
        }
    }
}
