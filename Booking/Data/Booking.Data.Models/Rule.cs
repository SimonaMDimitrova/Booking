namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class Rule : BaseModel<int>
    {
        public Rule()
        {
            this.PropertyRules = new HashSet<PropertyRule>();
        }

        [Required]
        [MaxLength(GlobalConstants.RuleNameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<PropertyRule> PropertyRules { get; set; }
    }
}
