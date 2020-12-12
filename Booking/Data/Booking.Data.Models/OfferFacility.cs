namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class OfferFacility : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Property))]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        [ForeignKey(nameof(Facility))]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}
