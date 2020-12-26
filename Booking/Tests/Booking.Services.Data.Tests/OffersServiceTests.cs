namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Offers.Add;
    using Booking.Web.ViewModels.Offers.Edit;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class OffersServiceTests : BaseServiceTests
    {
        private IOffersService Service => this.ServiceProvider.GetRequiredService<IOffersService>();

        private IDeletableEntityRepository<Offer> OffersRepository => this.ServiceProvider.GetRequiredService<IDeletableEntityRepository<Offer>>();

        [Fact]
        public async Task CheckDeleteAllByPropertyId()
        {
            var property = await this.GetAddedToDbProperty();

            await this.Service
                .DeleteAllByPropertyIdAsync(
                    property.Id,
                    property.ApplicationUserId,
                    $"../../{GlobalConstants.OfferImagesPath}");

            Assert.Equal(0, this.OffersRepository.All().Count());
        }

        [Fact]
        public async Task CheckDeleteAsyncMethod()
        {
            var property = await this.GetAddedToDbProperty();

            await this.Service
                .DeleteAsync(
                    property.Offers.FirstOrDefault().Id,
                    property.ApplicationUserId,
                    $"../../{GlobalConstants.OfferImagesPath}");

            Assert.Equal(1, this.OffersRepository.All().Count());
        }

        [Fact]
        public async Task CheckUpdateAsyncMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var offer = property.Offers.FirstOrDefault();
            var input = new EditOfferViewModel
            {
                Count = 20,
                CurrencyCode = offer.Property.Town.Country.Currency.CurrencyCode,
                OfferId = offer.Id,
                PricePerPerson = 150,
                PropertyId = property.Id,
                PropertyName = property.Name,
                ValidFrom = offer.ValidFrom.AddDays(10),
                ValidTo = offer.ValidTo.AddDays(1),
            };

            await this.Service
                .UpdateAsync(input);

            Assert.Equal(input.Count, offer.Count);
            Assert.Equal(input.ValidTo, offer.ValidTo);
            Assert.Equal(input.ValidFrom, offer.ValidFrom);
            Assert.Equal(input.PricePerPerson, offer.PricePerPerson);
        }

        [Fact]
        public async Task CheckCreateAsyncMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var input = new AddOfferInputModel
            {
                BedTypesCounts = new List<int>
                {
                    1, 0, 0, 0,
                },
                Count = 10,
                CurrencyCode = property.Town.Country.Currency.CurrencyCode,
                PricePerPerson = 50,
                ValidFrom = DateTime.UtcNow.AddDays(1),
                ValidTo = DateTime.UtcNow.AddDays(10),
                PropertyId = property.Id,
                PropertyName = property.Name,
            };

            await this.Service
                .CreateAsync(
                    input,
                    $"../../{GlobalConstants.OfferImagesPath}");

            Assert.Equal(3, this.OffersRepository.All().Count());
        }

        [Fact]
        public async Task CheckGetByIdMethod()
        {
            var property = await this.GetAddedToDbProperty();
            var offer = property.Offers.FirstOrDefault();

            var expectedResult = new EditOfferViewModel
            {
                Count = offer.Count,
                CurrencyCode = property.Town.Country.Currency.CurrencyCode,
                OfferId = offer.Id,
                PricePerPerson = offer.PricePerPerson,
                PropertyId = property.Id,
                PropertyName = property.Name,
                ValidFrom = offer.ValidFrom,
                ValidTo = offer.ValidTo,
            };

            var actualResult = this.Service.GetById(offer.Id, property.ApplicationUserId);

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult.CurrencyCode, actualResult.CurrencyCode);
            Assert.Equal(expectedResult.OfferId, actualResult.OfferId);
            Assert.Equal(expectedResult.PricePerPerson, actualResult.PricePerPerson);
            Assert.Equal(expectedResult.PropertyId, actualResult.PropertyId);
            Assert.Equal(expectedResult.PropertyName, actualResult.PropertyName);
            Assert.Equal(expectedResult.ValidFrom, actualResult.ValidFrom);
            Assert.Equal(expectedResult.ValidTo, actualResult.ValidTo);
        }

        private async Task<Property> GetAddedToDbProperty()
        {
            var property = new Property
            {
                ApplicationUserId = Guid.NewGuid().ToString(),
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
                },
            };

            await this.DbContext.Properties.AddAsync(property);
            await this.DbContext.SaveChangesAsync();

            return property;
        }
    }
}
