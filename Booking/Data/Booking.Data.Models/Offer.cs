namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();

            this.OfferFacilities = new HashSet<OfferFacility>();
            this.OfferBedTypes = new HashSet<OfferBedType>();
            this.ApplicationUserOffers = new HashSet<Booking>();
            this.OfferImages = new HashSet<OfferImage>();
        }

        public byte Count { get; set; }

        public decimal PricePerPerson { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        public virtual ICollection<Booking> ApplicationUserOffers { get; set; }

        public virtual ICollection<OfferFacility> OfferFacilities { get; set; }

        public virtual ICollection<OfferBedType> OfferBedTypes { get; set; }

        public virtual ICollection<OfferImage> OfferImages { get; set; }
    }
}
