namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class OfferBedType : BaseDeletableModel<int>
    {
        [Required]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        public int BedTypeId { get; set; }

        public virtual BedType BedType { get; set; }
    }
}
