namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Bookings;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class BookingsServiceTests : BaseServiceTests
    {
        private IBookingsService Service => this.ServiceProvider.GetRequiredService<IBookingsService>();

        [Fact]
        public async Task CheckAddAsyncMethod()
        {
            var userId = Guid.NewGuid().ToString();
            var property = await this.GetAddedToDbProperty();
            var booking = new Booking
            {
                ApplicationUserId = userId,
                CheckIn = DateTime.UtcNow.AddDays(1),
                CheckOut = DateTime.UtcNow.AddDays(4),
                OfferId = property.Offers.FirstOrDefault().Id,
            };

            await this.DbContext.Bookings.AddAsync(booking);
            await this.DbContext.SaveChangesAsync();

            var input = new BookingInputModel
            {
                CheckIn = DateTime.UtcNow.AddDays(1),
                CheckOut = DateTime.UtcNow.AddDays(4),
                Members = 2,
                PropertyId = property.Id,
                OfferId = property.Offers.FirstOrDefault().Id,
            };

            await this.Service.AddAsync(input, userId);

            Assert.Equal(2, this.DbContext.Bookings.Count());
            Assert.Equal(2, property.Offers.FirstOrDefault().Count);
        }

        [Fact]
        public async Task CheckDeleteAsyncMethod()
        {
            var userId = Guid.NewGuid().ToString();
            var property = await this.GetAddedToDbProperty();
            var booking = new Booking
            {
                ApplicationUserId = userId,
                CheckIn = DateTime.UtcNow.AddDays(1),
                CheckOut = DateTime.UtcNow.AddDays(4),
                OfferId = property.Offers.FirstOrDefault().Id,
            };

            await this.DbContext.Bookings.AddAsync(booking);
            await this.DbContext.SaveChangesAsync();

            await this.Service.DeleteAsync(booking.Id, userId);

            Assert.Equal(0, this.DbContext.Bookings.Count());
        }

        [Fact]
        public async Task CheckGetAllByUserIdMethod()
        {
            var userId = Guid.NewGuid().ToString();
            var property = await this.GetAddedToDbProperty();
            var booking = new Booking
            {
                ApplicationUserId = userId,
                CheckIn = DateTime.UtcNow.AddDays(1),
                CheckOut = DateTime.UtcNow.AddDays(4),
                OfferId = property.Offers.FirstOrDefault().Id,
            };

            await this.DbContext.Bookings.AddAsync(booking);
            await this.DbContext.SaveChangesAsync();

            var expectedResult = new List<BookingInListViewModel>
            {
                new BookingInListViewModel
                {
                    Address = booking.Offer.Property.Address,
                    CheckIn = booking.CheckIn.ToString(GlobalConstants.DateFormat),
                    CheckOut = booking.CheckOut.ToString(GlobalConstants.DateFormat),
                    Country = booking.Offer.Property.Town.Country.Name,
                    CurrencyCode = booking.Offer.Property.Town.Country.Currency.CurrencyCode,
                    Id = booking.Id,
                    Members = booking.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                    Price = booking.Offer.PricePerPerson * booking.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                    PropertyName = booking.Offer.Property.Name,
                    Town = booking.Offer.Property.Town.Name,
                },
            };

            var actualResult = this.Service.GetAllByUserId(userId).ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult[0].Address, actualResult[0].Address);
            Assert.Equal(expectedResult[0].CheckIn, actualResult[0].CheckIn);
            Assert.Equal(expectedResult[0].CheckOut, actualResult[0].CheckOut);
            Assert.Equal(expectedResult[0].Country, actualResult[0].Country);
            Assert.Equal(expectedResult[0].CurrencyCode, actualResult[0].CurrencyCode);
            Assert.Equal(expectedResult[0].Id, actualResult[0].Id);
            Assert.Equal(expectedResult[0].Members, actualResult[0].Members);
            Assert.Equal(expectedResult[0].Price, actualResult[0].Price);
            Assert.Equal(expectedResult[0].PropertyName, actualResult[0].PropertyName);
            Assert.Equal(expectedResult[0].Town, actualResult[0].Town);
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
                },
            };

            await this.DbContext.Properties.AddAsync(property);
            await this.DbContext.SaveChangesAsync();

            return property;
        }
    }
}
