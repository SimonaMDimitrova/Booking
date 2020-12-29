namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Bookings;

    public class BookingsService : IBookingsService
    {
        private readonly IDeletableEntityRepository<Booking> bookingsRepository;
        private readonly IDeletableEntityRepository<Offer> offersRepository;

        public BookingsService(
            IDeletableEntityRepository<Booking> bookingsRepository,
            IDeletableEntityRepository<Offer> offersRepository)
        {
            this.bookingsRepository = bookingsRepository;
            this.offersRepository = offersRepository;
        }

        public IEnumerable<BookingInListViewModel> GetAllByUserId(string userId)
        {
            var bookings = this.bookingsRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .Select(a => new BookingInListViewModel
                {
                    Id = a.Id,
                    Address = a.Offer.Property.Address,
                    Country = a.Offer.Property.Town.Country.Name,
                    Town = a.Offer.Property.Town.Name,
                    CurrencyCode = a.Offer.Property.Town.Country.Currency.CurrencyCode,
                    PropertyName = a.Offer.Property.Name,
                    Members = a.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                    Price = a.Offer.PricePerPerson * a.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                    CheckIn = a.CheckIn.ToString(GlobalConstants.DateFormat),
                    CheckOut = a.CheckOut.ToString(GlobalConstants.DateFormat),
                })
                .ToList();

            return bookings;
        }

        public async Task DeleteAsync(string bookingId, string userId)
        {
            var booking = this.bookingsRepository
                .All()
                .Where(a => a.ApplicationUserId == userId && a.Id == bookingId)
                .FirstOrDefault();

            if (booking == null)
            {
                throw new Exception(GlobalConstants.ErrorMessages.BookingErrorValue);
            }

            this.bookingsRepository.Delete(booking);
            await this.bookingsRepository.SaveChangesAsync();
        }

        public async Task AddAsync(BookingInputModel input, string userId)
        {
            var offer = this.offersRepository.All()
                .FirstOrDefault(o =>
                    o.Id == input.OfferId
                    && o.Property.ApplicationUserId != userId
                    && o.Count > 0);
            if (offer == null
                || offer.ValidFrom.AddDays(2) > input.CheckIn
                || offer.ValidTo < input.CheckOut)
            {
                throw new Exception(GlobalConstants.ErrorMessages.BookingErrorValue);
            }

            var applicationUserOffer = new Booking
            {
                ApplicationUserId = userId,
                OfferId = input.OfferId,
                CheckIn = input.CheckIn,
                CheckOut = input.CheckOut,
            };

            offer.Count--;

            offer.Bookings.Add(applicationUserOffer);
            await this.offersRepository.SaveChangesAsync();
        }
    }
}
