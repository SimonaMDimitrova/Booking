﻿namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class PropertyCategory : BaseDeletableModel<string>
    {
        public PropertyCategory()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey(nameof(PropertyType))]
        [Required]
        public string PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
