namespace Booking.Web.ViewModels.SearchProperties
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;
    using Booking.Web.Infrastructure.ValidationAttributes;

    public class SearchedInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DateMinValue]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        [Range(GlobalConstants.BookingMinMembers, GlobalConstants.BookingMaxMembers)]
        public byte Members { get; set; }
    }
}
