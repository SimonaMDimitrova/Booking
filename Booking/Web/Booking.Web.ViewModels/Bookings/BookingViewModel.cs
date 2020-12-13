namespace Booking.Web.ViewModels.Bookings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BookingViewModel
    {
        public string Id { get; set; }

        public string PropertyName { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }

        public int Members { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }
    }
}
