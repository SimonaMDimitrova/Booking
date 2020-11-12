namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class FacilityCategory : BaseDeletableModel<string>
    {
        public FacilityCategory()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Facilities = new HashSet<Facility>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
