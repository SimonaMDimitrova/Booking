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
        private ICountriesService Service => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public void CheckGetAllByKeyValuePairsMethod()
        {
            var country = this.AddToDb();

            var expectedResult = new List<KeyValuePair<string, string>>();
            expectedResult.Add(new KeyValuePair<string, string>(country.Id.ToString(), country.Name));

            var actualResult = this.Service.GetAllByKeyValuePairs().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetMostPopularByKeyValuePairsMethod()
        {
            var country = this.AddToDb();

            var expectedResult = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(country.Id.ToString(), country.Name),
            };
            var actualResult = this.Service.GetMostPopularByKeyValuePairs().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckGetTheSixMostVisitedMethod()
        {
            var country = this.AddToDb();

            var expectedResult = new List<CountryInListViewModel>();
            expectedResult.Add(new CountryInListViewModel
            {
                PropertiesCount = country.Towns.Sum(t => t.Properties.Count),
                Name = country.Name,
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
            var countries = new List<Country>();
            for (int i = 0; i < 7; i++)
            {
                var country = new Country
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
                };

                this.DbContext.Countries.Add(country);
                countries.Add(country);
            }

            this.DbContext.SaveChanges();

            var expectedResult = new List<CountryInListViewModel>();
            for (int i = 0; i < 6; i++)
            {
                var currentCountry = countries[i];
                expectedResult.Add(new CountryInListViewModel
                {
                    PropertiesCount = currentCountry.Towns.Sum(t => t.Properties.Count),
                    Name = currentCountry.Name,
                    Image = "assets/images/defaults/default.png",
                });
            }

            var actualResult = this.Service.GetTheSixMostVisited().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            for (int i = 0; i < 6; i++)
            {
                Assert.Equal(expectedResult[i].Image, actualResult[i].Image);
                Assert.Equal(expectedResult[i].Name, actualResult[i].Name);
                Assert.Equal(expectedResult[i].PropertiesCount, actualResult[i].PropertiesCount);
            }
        }

        [Fact]
        public void CheckGetTheSixMostVisitedNamesMethod()
        {
            var country = this.AddToDb();

            var expectedResult = new List<string>();
            expectedResult.Add(country.Name);

            var actualResult = this.Service.GetTheSixMostVisitedNames().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        private Country AddToDb()
        {
            var country = new Country
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

            this.DbContext.Countries.Add(country);
            this.DbContext.SaveChanges();

            return country;
        }
    }
}
