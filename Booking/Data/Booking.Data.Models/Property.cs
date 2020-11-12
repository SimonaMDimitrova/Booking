namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();

            this.PropertyRules = new HashSet<PropertyRule>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        public byte? Floors { get; set; }

        [ForeignKey(nameof(Town))]
        [Required]
        public string TownId { get; set; }

        public virtual Town Town { get; set; }

        [ForeignKey(nameof(PropertyCategory))]
        [Required]
        public string PropertyCategoryId { get; set; }

        public virtual PropertyCategory PropertyCategory { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }
    }
}
