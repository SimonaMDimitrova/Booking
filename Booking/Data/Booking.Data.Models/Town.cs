namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class Town : BaseModel<int>
    {
        public Town()
        {
            this.Properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(GlobalConstants.TownNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
