namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class FacilityCategory : BaseModel<int>
    {
        public FacilityCategory()
        {
            this.Facilities = new HashSet<Facility>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
