namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class PropertyCategory : BaseModel<int>
    {
        public PropertyCategory()
        {
            this.Properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(GlobalConstants.PropertyCategoryNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
