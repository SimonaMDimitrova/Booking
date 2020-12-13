namespace Booking.Web.ViewModels.Bookings
{
    using System;

    public class BookingInputModel
    {
        public string OfferId { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}
