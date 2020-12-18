namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Bookings;
    using Booking.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task AddToProperty(string propertyId, AddOfferInputModel input, string imagePath);

        Task DeleteAsync(string id);

        Task UpdateAsync(string offerId, EditOfferViewModel input);

        EditOfferViewModel GetById(string id);

        Task AddToUserBookingList(BookingInputModel input, string userId);

        IEnumerable<BookingViewModel> GetBookingsByUserId(string userId);

        Task DeleteBookingAsync(string bookingId, string userId);
    }
}
