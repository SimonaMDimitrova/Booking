namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class Rule : BaseDeletableModel<string>
    {
        public Rule()
        {
            this.Id = Guid.NewGuid().ToString();

            this.PropertyRules = new HashSet<PropertyRule>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }
    }
}
