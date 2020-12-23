namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class OfferFacility : BaseDeletableModel<int>
    {
        [Required]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}
