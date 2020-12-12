namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class OfferBedType : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Offer))]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        [ForeignKey(nameof(BedType))]
        public int BedTypeId { get; set; }

        public virtual BedType BedType { get; set; }
    }
}
