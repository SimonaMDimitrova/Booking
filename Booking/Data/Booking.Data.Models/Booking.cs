namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class Booking : BaseDeletableModel<string>
    {
        public Booking()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
