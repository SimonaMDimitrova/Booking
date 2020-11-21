﻿namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class PropertyType : BaseDeletableModel<int>
    {
        public PropertyType()
        {
            this.PropertyCategories = new HashSet<PropertyCategory>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public virtual ICollection<PropertyCategory> PropertyCategories { get; set; }
    }
}