namespace Booking.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Models;
    using Booking.Web.ViewModels.ViewComponents.Countries;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class CountriesServiceTests : BaseServiceTests
    {
        private Country country = new Country
        {
            Name = "Country1",
            Currency = new Currency { CurrencyCode = "CN1" },
            Towns =
                {
                    new Town
                    {
                        Name = "Town1",
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
                    },
                },
        };

        private ICountriesService Service => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public void CheckGetAllByKeyValuePairsMethod()
        {
            this.AddToDb();

            var expectedResult = new List<KeyValuePair<string, string>>();
            expectedResult.Add(new KeyValuePair<string, string>(this.country.Id.ToString(), this.country.Name));

            var actualResult = this.Service.GetAllByKeyValuePairs().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetMostPopularByKeyValuePairsMethod()
        {
            this.AddToDb();

            var expectedResult = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(this.country.Id.ToString(), this.country.Name),
            };
            var actualResult = this.Service.GetMostPopularByKeyValuePairs().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetTheSixMostVisitedMethod()
        {
            this.AddToDb();

            var expectedResult = new List<CountryInListViewModel>();
            expectedResult.Add(new CountryInListViewModel
            {
                PropertiesCount = this.country.Towns.Sum(t => t.Properties.Count),
                Name = this.country.Name,
                Image = "assets/images/defaults/default.png",
            });

            var actualResult = this.Service.GetTheSixMostVisited().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult[0].Image, actualResult[0].Image);
            Assert.Equal(expectedResult[0].Name, actualResult[0].Name);
            Assert.Equal(expectedResult[0].PropertiesCount, actualResult[0].PropertiesCount);
        }

        [Fact]
        public void CheckGetTheSixMostVisitedMethodWhenCountriesAreMoreThan6()
        {
            for (int i = 0; i < 7; i++)
            {
                this.DbContext.Countries.Add(new Country
                {
                    Name = $"Country{i}",
                    Currency = new Currency { CurrencyCode = $"CN{i}" },
                    Towns =
                    {
                        new Town
                        {
                            Name = "Town1",
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
                        },
                    },
                });
            }
            this.DbContext.SaveChanges();

            var expectedResult = new List<CountryInListViewModel>();
            for (int i = 0; i < 6; i++)
            {
                expectedResult.Add(new CountryInListViewModel
                {
                    PropertiesCount = this.country.Towns.Sum(t => t.Properties.Count),
                    Name = this.country.Name,
                    Image = "assets/images/defaults/default.png",
                });
            }

            var actualResult = this.Service.GetTheSixMostVisited().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
        }

        [Fact]
        public void CheckGetTheSixMostVisitedNamesMethod()
        {
            this.AddToDb();

            var expectedResult = new List<string>();
            expectedResult.Add(this.country.Name);

            var actualResult = this.Service.GetTheSixMostVisitedNames().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        private void AddToDb()
        {
            this.DbContext.Countries.Add(this.country);

            this.DbContext.SaveChanges();
        }
    }
}
