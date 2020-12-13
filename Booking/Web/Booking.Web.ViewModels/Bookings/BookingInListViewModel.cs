namespace Booking.Web.ViewModels.Bookings
{
    using System.Collections.Generic;

    public class BookingInListViewModel
    {
        public IEnumerable<BookingViewModel> Bookings { get; set; }
    }
}
