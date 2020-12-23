namespace Booking.Web.ViewModels.SearchProperties
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
        [Range(1, 30)]
        public byte Members { get; set; }
    }
}
