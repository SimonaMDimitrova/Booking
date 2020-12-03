namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class PropertyFacility : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        [ForeignKey(nameof(Facility))]
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
    }
}
