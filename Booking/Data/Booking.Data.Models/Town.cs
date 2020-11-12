namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Town : BaseDeletableModel<string>
    {
        public Town()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        [Required]
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
