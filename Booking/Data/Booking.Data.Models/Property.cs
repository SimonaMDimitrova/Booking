namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();

            this.PropertyRules = new HashSet<PropertyRule>();
            this.Offers = new HashSet<Offer>();
            this.PropertyFacilities = new HashSet<PropertyFacility>();
            this.PropertyImages = new HashSet<PropertyImage>();
        }

        [Required]
        [MaxLength(GlobalConstants.PropertyNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GlobalConstants.PropertyAddressMaxLength)]
        public string Address { get; set; }

        public byte Floors { get; set; }

        public byte Stars { get; set; }

        [MaxLength(GlobalConstants.PropertyDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        [Required]
        public int PropertyCategoryId { get; set; }

        public virtual PropertyCategory PropertyCategory { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }

        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
    }
}
