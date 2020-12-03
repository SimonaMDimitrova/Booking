namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class PropertyRule : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        [ForeignKey(nameof(Rule))]
        public int RuleId { get; set; }

        public virtual Rule Rule { get; set; }

        public bool IsAllowed { get; set; }
    }
}
