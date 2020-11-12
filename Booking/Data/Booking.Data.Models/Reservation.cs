namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Offer))]
        [Required]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
