namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class ApplicationUserOffer : BaseDeletableModel<string>
    {
        public ApplicationUserOffer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey(nameof(Offer))]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
