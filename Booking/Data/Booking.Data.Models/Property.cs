﻿namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();

            this.PropertyRules = new HashSet<PropertyRule>();
            this.Offers = new HashSet<Offer>();
            this.PropertyFacilities = new HashSet<PropertyFacility>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        public byte Floors { get; set; }

        public byte Stars { get; set; }

        [Required]
        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        [Required]
        [ForeignKey(nameof(PropertyCategory))]
        public int PropertyCategoryId { get; set; }

        public virtual PropertyCategory PropertyCategory { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<PropertyFacility> PropertyFacilities { get; set; }
    }
}
