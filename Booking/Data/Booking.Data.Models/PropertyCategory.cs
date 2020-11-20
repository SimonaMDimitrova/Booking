namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class PropertyCategory : BaseDeletableModel<int>
    {
        public PropertyCategory()
        {
            this.Properties = new HashSet<Property>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(PropertyType))]
        public int PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
