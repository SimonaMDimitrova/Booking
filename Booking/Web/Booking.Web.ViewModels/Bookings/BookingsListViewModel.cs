namespace Booking.Web.ViewModels.Bookings
{
    using System.Collections.Generic;

    public class BookingsListViewModel
    {
        public IEnumerable<BookingInListViewModel> Bookings { get; set; }
    }
}
