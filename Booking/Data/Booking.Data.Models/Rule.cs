namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class Rule : BaseDeletableModel<int>
    {
        public Rule()
        {
            this.PropertyRules = new HashSet<PropertyRule>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }
    }
}
