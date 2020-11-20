namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();

            this.OfferFacilities = new HashSet<OfferFacility>();
            this.Rooms = new HashSet<Room>();
        }

        public decimal Price { get; set; }

        public bool IsCreditCardAllowed { get; set; }

        public byte CancellationDays { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Reservation))]
        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<OfferFacility> OfferFacilities { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        // TODO: Photos entity
    }
}
