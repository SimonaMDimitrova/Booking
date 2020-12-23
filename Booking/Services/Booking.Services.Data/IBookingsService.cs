namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Bookings;

    public interface IBookingsService
    {
        Task AddAsync(BookingInputModel input, string userId);

        IEnumerable<BookingInListViewModel> GetAllByUserId(string userId);

        Task DeleteAsync(string bookingId, string userId);
    }
}
