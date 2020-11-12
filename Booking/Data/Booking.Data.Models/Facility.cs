namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Facility : BaseDeletableModel<string>
    {
        public Facility()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey(nameof(FacilityCategory))]
        [Required]
        public string FacilityCategoryId { get; set; }

        public virtual FacilityCategory FacilityCategory { get; set; }

        [ForeignKey(nameof(Offer))]
        [Required]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
