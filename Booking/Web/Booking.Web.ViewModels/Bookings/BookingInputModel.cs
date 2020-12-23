﻿namespace Booking.Web.ViewModels.Bookings
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
        [Range(1, 30)]
        public byte Members { get; set; }
    }
}
