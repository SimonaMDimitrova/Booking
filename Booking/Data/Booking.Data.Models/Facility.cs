namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class Facility : BaseModel<int>
    {
        public Facility()
        {
            this.PropertyFacilities = new HashSet<PropertyFacility>();
            this.OfferFacilities = new HashSet<OfferFacility>();
        }

        [Required]
        [MaxLength(GlobalConstants.FacilityNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int FacilityCategoryId { get; set; }

        public virtual FacilityCategory FacilityCategory { get; set; }

        public virtual ICollection<OfferFacility> OfferFacilities { get; set; }

        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }
    }
}
