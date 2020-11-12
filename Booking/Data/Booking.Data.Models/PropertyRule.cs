namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class PropertyRule : BaseDeletableModel<string>
    {
        public PropertyRule()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Property))]
        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [ForeignKey(nameof(Rule))]
        [Required]
        public string RuleId { get; set; }

        public virtual Rule Rule { get; set; }

        public bool IsAllowed { get; set; }
    }
}
