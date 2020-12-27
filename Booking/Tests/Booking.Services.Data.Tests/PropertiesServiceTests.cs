namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.PropertiesViewModels.All;
    using Booking.Web.ViewModels.ViewComponents.SearchResults;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class PropertiesServiceTests : BaseServiceTests
    {
        private IPropertiesService Service => this.ServiceProvider.GetRequiredService<IPropertiesService>();

        private IDeletableEntityRepository<Property> OffersRepository => this.ServiceProvider.GetRequiredService<IDeletableEntityRepository<Property>>();

        [Fact]
        public async Task CheckIsUserHasAccessToPropertyMethodWhenUserDoNotHaveAccess()
        {
            var property = await this.GetAddedToDbProperty();
            var actualResult = this.Service.IsUserHasAccessToProperty(property.Id, Guid.NewGuid().ToString());

            Assert.False(actualResult);
        }

        [Fact]
        public async Task CheckIsUserHasAccessToPropertyMethodWhenUserHasAccess()
        {
            var property = await this.GetAddedToDbProperty();
            var actualResult = this.Service.IsUserHasAccessToProperty(property.Id, property.ApplicationUserId);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task CheckIfNameIsAvailableMethodWhenItIs()
        {
            await this.GetAddedToDbProperty();
            var actualResult = this.Service.CheckIfNameIsAvailable("new name");

            Assert.False(actualResult);
        }

        [Fact]
        public async Task CheckIfNameIsAvailableMethodWhenItIsNot()
        {
            await this.GetAddedToDbProperty();
            var actualResult = this.Service.CheckIfNameIsAvailable("some name");

            Assert.True(actualResult);
        }

        [Fact]
        public async Task CheckGetNameByIdMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var actualResult = this.Service.GetNameById(property.Id);

            Assert.Equal(property.Name, actualResult);
        }

        [Fact]
        public async Task TaskGetIdByNameMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var actualResult = this.Service.GetIdByName(property.Name);

            Assert.Equal(property.Id, actualResult);
        }

        [Fact]
        public async Task CheckGetIdByOfferIdMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var offer = property.Offers.FirstOrDefault();
            var actualResult = this.Service.GetIdByOfferId(offer.Id, property.ApplicationUserId);

            Assert.Equal(property.Id, actualResult);
        }

        [Fact]
        public async Task CheckGetAllByUserId()
        {
            var property = await this.GetAddedToDbProperty();
            var expectedResult = new PropertiesListViewModel
            {
                Properties = new List<PropertyInListViewModel>
                {
                    new PropertyInListViewModel
                    {
                        Country = property.Town.Country.Name,
                        Id = property.Id,
                        Name = property.Name,
                        PropertyCategory = property.PropertyCategory.Name,
                        Stars = property.Stars,
                        Town = property.Town.Name,
                        Image = property.PropertyImages.FirstOrDefault(pi => pi.PropertyId == property.Id) != null ?
                            $"../..{GlobalConstants.PropertyImagesPath}{property.PropertyImages.FirstOrDefault(pi => pi.PropertyId == property.Id).Id}.{property.PropertyImages.FirstOrDefault(pi => pi.PropertyId == property.Id).Extension}"
                            : property.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault() != null ?
                            $"../..{GlobalConstants.OfferImagesPath}{property.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault().Id}.{property.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault().Extension}"
                            : $"../..{GlobalConstants.DefaultImagePath}",
                    },
                },
            };

            var actualResult = this.Service.GetAllByUserId(property.ApplicationUserId).Properties.ToList();

            Assert.Equal(expectedResult.Properties.ToList().Count, actualResult.Count);
            Assert.Equal(expectedResult.Properties.ToList()[0].Country, actualResult[0].Country);
            Assert.Equal(expectedResult.Properties.ToList()[0].Image, actualResult[0].Image);
            Assert.Equal(expectedResult.Properties.ToList()[0].Id, actualResult[0].Id);
            Assert.Equal(expectedResult.Properties.ToList()[0].Name, actualResult[0].Name);
            Assert.Equal(expectedResult.Properties.ToList()[0].PropertyCategory, actualResult[0].PropertyCategory);
            Assert.Equal(expectedResult.Properties.ToList()[0].Stars, actualResult[0].Stars);
            Assert.Equal(expectedResult.Properties.ToList()[0].Town, actualResult[0].Town);
        }

        [Fact]
        public async Task CheckIfEditInputNameIsAvailable()
        {
            var property = await this.GetAddedToDbProperty();

            var actualResult = this.Service.CheckIfEditInputNameIsAvailable("name", property.Id);

            Assert.False(actualResult);
        }

        [Fact]
        public async Task CheckGetBySearchRequirementsWhenInputIsWrong()
        {
            var property = await this.GetAddedToDbProperty();
            var input = new IndexInputModel
            {
                CheckIn = DateTime.UtcNow.AddDays(8),
                CheckOut = DateTime.UtcNow.AddDays(6),
                CountryId = property.Town.Country.Id.ToString(),
                TownId = property.Town.Id.ToString(),
                MaxBudget = 0,
                MinBudget = 0,
                Members = 0,
            };

            var actualResult = this.Service.GetBySearchRequirements(input, property.ApplicationUser.Email);

            Assert.Null(actualResult);
        }

        private async Task<Property> GetAddedToDbProperty()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "moni@abv.bg",
            };

            var property = new Property
            {
                ApplicationUserId = user.Id,
                Address = "some address",
                Floors = 5,
                Name = "some name",
                Stars = 3,
                Town = new Town
                {
                    Country = new Country { Name = "Country", Currency = new Currency { CurrencyCode = "CRN" } },
                    Name = "Town",
                },
                PropertyCategory = new PropertyCategory
                {
                    Name = "Hotel",
                    PropertyType = new PropertyType { Name = "Apartment" },
                },
                Offers =
                {
                    new Offer
                    {
                        Count = 3,
                        ValidFrom = DateTime.UtcNow,
                        ValidTo = DateTime.UtcNow.AddDays(5),
                        PricePerPerson = 50,
                        OfferBedTypes =
                        {
                            new OfferBedType
                            {
                                BedType = new BedType { Type = "Bedroom", Capacity = 2, },
                            },
                        },
                    },
                    new Offer
                    {
                        Count = 3,
                        ValidFrom = DateTime.UtcNow,
                        ValidTo = DateTime.UtcNow.AddDays(10),
                        PricePerPerson = 50,
                        OfferBedTypes =
                        {
                            new OfferBedType
                            {
                                BedType = new BedType { Type = "Bedroom", Capacity = 2, },
                            },
                        },
                    },
                },
            };

            await this.DbContext.Properties.AddAsync(property);
            await this.DbContext.Users.AddAsync(user);
            await this.DbContext.SaveChangesAsync();

            return property;
        }
    }
}
