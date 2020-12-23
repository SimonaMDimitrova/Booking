namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class PropertyType : BaseModel<int>
    {
        public PropertyType()
        {
            this.PropertyCategories = new HashSet<PropertyCategory>();
        }

        [Required]
        [MaxLength(GlobalConstants.PropertyTypeNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<PropertyCategory> PropertyCategories { get; set; }
    }
}
