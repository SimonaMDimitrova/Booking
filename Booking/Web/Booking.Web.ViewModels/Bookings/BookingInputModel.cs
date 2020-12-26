namespace Booking.Web.ViewModels.Bookings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;

    public class BookingInputModel
    {
        [Required]
        public string OfferId { get; set; }

        [Required]
        public string PropertyId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        [Range(GlobalConstants.BookingMinMembers, GlobalConstants.BookingMaxMembers)]
        public byte Members { get; set; }
    }
}
