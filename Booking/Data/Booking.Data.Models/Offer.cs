namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();

            this.OfferFacilities = new HashSet<OfferFacility>();
            this.Rooms = new HashSet<Room>();
            this.ApplicationUserOffers = new HashSet<ApplicationUserOffer>();
        }

        public decimal Price { get; set; }

        public bool IsCreditCardAllowed { get; set; }

        public byte CancellationDays { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        public virtual ICollection<ApplicationUserOffer> ApplicationUserOffers { get; set; }

        public virtual ICollection<OfferFacility> OfferFacilities { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        // TODO: Photos entity
    }
}
