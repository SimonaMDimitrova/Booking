namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class PropertyFacility : BaseDeletableModel<int>
    {
        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}
